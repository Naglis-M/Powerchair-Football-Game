using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ChairCollisionTests
{
    private GameObject chairObject;
    private ChairController chairController;
    private GameObject ballObject;
    private Rigidbody ballRigidbody;

    [SetUp]
    public void SetUp() {
        chairObject = new GameObject("Chair");
        chairController = chairObject.AddComponent<ChairController>();
        chairObject.AddComponent<Rigidbody>();

        ballObject = new GameObject("Ball");
        ballRigidbody = ballObject.AddComponent<Rigidbody>();
        ballRigidbody.useGravity = false;

        // Assume chair has a collider setup as needed
        chairObject.AddComponent<BoxCollider>().isTrigger = false;
        ballObject.AddComponent<SphereCollider>().isTrigger = false;
    }

    [TearDown]
    public void TearDown() {
        UnityEngine.Object.Destroy(chairObject);
        UnityEngine.Object.Destroy(ballObject);
    }

    [Test]
    public void BallReceivesForceOnCollision() {
        float initialSpeed = ballRigidbody.velocity.magnitude;

        // Simulate collision by moving ball towards the chair
        Physics.ClosestPoint(chairObject.transform.position, ballObject.GetComponent<Collider>(), ballObject.transform.position, Quaternion.identity);

        // Assume a method that triggers on collision to apply force
        chairController.OnCollisionEnter(new Collision());

        float finalSpeed = ballRigidbody.velocity.magnitude;
        Assert.Greater(finalSpeed, initialSpeed, "Ball should accelerate after collision due to force applied by the chair.");
    }
}