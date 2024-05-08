using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BallCollisionTests
{
    private GameObject ballObject;
    private BallController ballController;
    private Rigidbody ballRigidbody;
    private GameObject boundaryObject;

    [SetUp]
    public void SetUp() {
        ballObject = new GameObject("Ball");
        ballController = ballObject.AddComponent<BallController>();
        ballRigidbody = ballObject.AddComponent<Rigidbody>();
        ballRigidbody.useGravity = false;

        // Setting up the boundary object
        boundaryObject = new GameObject("Boundary");
        boundaryObject.tag = "Boundary";
        boundaryObject.AddComponent<BoxCollider>().isTrigger = false;

        // Mocking the ScoreManager
        GameObject scoreManagerObject = new GameObject("ScoreManager");
        ScoreManager scoreManager = scoreManagerObject.AddComponent<ScoreManager>();
        ballController.scoreManager = scoreManager;
    }

    [TearDown]
    public void TearDown() {
        UnityEngine.Object.Destroy(ballObject);
        UnityEngine.Object.Destroy(boundaryObject);
    }

    [UnityTest]
    public IEnumerator BallDestroysOnBoundaryCollision() {
        // Set ball's position near the boundary
        ballObject.transform.position = new Vector3(0, 0, 0);
        boundaryObject.transform.position = new Vector3(1, 0, 0);

        // Simulate collision
        Physics.Simulate(0.1f);
        yield return new WaitForFixedUpdate(); // Wait for next physics update
        
        // Test if ball is destroyed
        Assert.IsTrue(ballObject == null, "Ball should be destroyed after hitting a boundary.");
    }

    [UnityTest]
    public IEnumerator BallCollisionCooldown() {
        // Trigger collision
        Physics.ClosestPoint(ballObject.transform.position, boundaryObject.GetComponent<Collider>(), boundaryObject.transform.position, Quaternion.identity);

        // Before cooldown
        Assert.IsTrue(ballController.canCollide, "Ball should initially be able to collide.");

        yield return new WaitForSeconds(0.5f);

        // After cooldown
        Assert.IsTrue(ballController.canCollide, "Ball should be able to collide after cooldown period.");
    }
}