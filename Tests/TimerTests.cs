using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TimerTests
{
    private GameObject gameObj;
    private Timer timer;

    [SetUp]
    public void SetUp() {
        // Create a new game object to attach the Timer script
        gameObj = new GameObject();
        timer = gameObj.AddComponent<Timer>();
        timer.Start();
    }

    [TearDown]
    public void TearDown() {
        // Clean up after each test
        UnityEngine.Object.Destroy(gameObj);
    }

    [Test]
    public void Timer_InitializesCorrectly() {
        // Test that the timer initializes correctly
        Assert.AreEqual(0, timer.GetCurrentTime(), "Timer should start at 0.");
    }

    [UnityTest]
    public IEnumerator Timer_IncrementsCorrectly() {
        float initialTime = timer.GetCurrentTime();
        yield return new WaitForSecondsRealtime(1);  // Wait for 1 second
        Assert.Greater(timer.GetCurrentTime(), initialTime, "Timer should increment over time.");
    }

    [UnityTest]
    public IEnumerator Timer_StopsDuringPause() {
        Time.timeScale = 0;  // Pause the game
        float pausedTime = timer.GetCurrentTime();
        yield return new WaitForSecondsRealtime(2);  // Wait for 2 seconds
        Assert.AreEqual(pausedTime, timer.GetCurrentTime(), "Timer should not increment while game is paused.");
        Time.timeScale = 1;  // Resume time scale to normal for other tests
    }

    [Test]
    public void Timer_AddsPenaltyCorrectly() {
        float initialTime = timer.GetCurrentTime();
        timer.AddTimePenalty(5.0f);  // Add a penalty of 5 seconds
        Assert.AreEqual(initialTime + 5, timer.GetCurrentTime(), "Timer should include time penalty.");
    }

    [Test]
    public void Timer_ResetsCorrectly() {
        timer.AddTimePenalty(10.0f);  // Add some time to the timer
        timer.ResetTimer();  // Reset the timer
        Assert.AreEqual(0, timer.GetCurrentTime(), "Timer should reset to 0.");
    }
}