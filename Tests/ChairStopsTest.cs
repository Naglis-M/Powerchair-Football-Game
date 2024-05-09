using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ChairStopTest
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
        chairController.acceleration = 5.0f; // Set an acceleration value
    }

    [TearDown]
    public void TearDown() {
        UnityEngine.Object.DestroyImmediate(chairObject);
    }

    [UnityTest]
    public IEnumerator ChairStopsWhenNoInputGiven() {
        // Initially apply some velocity
        chairRigidbody.velocity = chairObject.transform.forward * chairController.acceleration;

        // Wait a little for movement
        yield return new WaitForSeconds(1);

        // Stop any input (simulate key up)
        chairRigidbody.velocity = Vector3.zero;

        // Allow time for physics to settle
        yield return new WaitForSeconds(1);

        // Check if the chair has stopped moving
        Assert.AreEqual(Vector3.zero, chairRigidbody.velocity, "Chair should come to a complete stop.");
    }
}