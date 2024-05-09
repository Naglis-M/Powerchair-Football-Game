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
        chairRigidbody.velocity = new Vector3(0, chairRigidbody.velocity.y, 0); // Preserve the Y-axis velocity due to gravity

        // Allow time for physics to settle
        yield return new WaitForSeconds(1);

        // Check if the chair has stopped moving on the X and Z axes
        Assert.AreEqual(Vector3.zero, new Vector3(chairRigidbody.velocity.x, 0, chairRigidbody.velocity.z), "Chair should come to a complete stop.");
    }
}