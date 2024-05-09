using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ChairRotationTest
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
        chairController.rotationTorque = 100.0f; // Set rotation torque value
    }

    [TearDown]
    public void TearDown() {
        UnityEngine.Object.DestroyImmediate(chairObject);
    }

    [UnityTest]
    public IEnumerator ChairRotatesWhenTorqueApplied() {
        // Apply torque to simulate rotation
        chairRigidbody.AddTorque(Vector3.up * chairController.rotationTorque);

        // Wait for physics update
        yield return new WaitForFixedUpdate();

        // Check if the chair has rotated from its initial rotation
        Assert.AreNotEqual(Quaternion.identity, chairRigidbody.rotation, "Chair should rotate from its initial orientation.");
    }
}