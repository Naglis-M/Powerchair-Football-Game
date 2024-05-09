using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
public class ChairRotationInputTest
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
    public IEnumerator ChairRotatesWhenHorizontalInputGiven() {
        // Simulate horizontal input
        chairController.FixedUpdateWithInput(0, 1); // Assume this is a method that handles input in FixedUpdate()

        // Wait for physics update
        yield return new WaitForFixedUpdate();

        // Check if the chair has rotation
        Assert.AreNotEqual(Quaternion.identity, chairObject.transform.rotation, "Chair should rotate from its initial rotation.");
    }
}