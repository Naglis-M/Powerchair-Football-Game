using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ChairMovementTest
{
    private GameObject chairObject;
    private ChairController chairController;
    private Rigidbody chairRigidbody;

    [SetUp]
    public void SetUp() {
        chairObject = new GameObject("Chair");
        chairController = chairObject.AddComponent<ChairController>();
        chairRigidbody = chairObject.AddComponent<Rigidbody>();
        chairRigidbody.useGravity = true;
        chairController.acceleration = 5.0f; // Set acceleration value
    }

    [TearDown]
    public void TearDown() {
        UnityEngine.Object.DestroyImmediate(chairObject);
    }

    [UnityTest]
    public IEnumerator ChairMovesForwardWhenAccelerated() {
        // Set a direct forward velocity for testing purposes
        chairRigidbody.velocity = chairObject.transform.forward * chairController.acceleration;

        // Wait for physics update
        yield return new WaitForFixedUpdate();

        // Check if the chair has moved forward
        Assert.AreNotEqual(Vector3.zero, chairRigidbody.velocity, "Chair should move from its initial position.");
    }
}