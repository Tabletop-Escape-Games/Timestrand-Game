using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class SpeedUpTests
{
    private bool sceneLoaded;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("TimeMachine", LoadSceneMode.Single);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneLoaded = true;

        //Set all settings
        Settings.gameMode = GameMode.SpeedUp;
        Settings.pointsPerHit = 1;
        Settings.pointsTrigger = 2;
    }

    [UnityTest]
    public IEnumerator GivenAddedPointsEqualToPointsTriggerAssertGameSpeedIncreases()
    {
        //Wait for the TimeStrand scene to be loaded
        yield return new WaitWhile(() => sceneLoaded == false);

        //Arrange
        float orgSpeed = Settings.scrollSpeed;

        //Act
        GameController.Instance.AddPoints(Settings.pointsTrigger);

        //Assert
        float newSpeed = Settings.scrollSpeed;
        Assert.Greater(newSpeed, orgSpeed);
    }

    [UnityTest]
    public IEnumerator GivenAddedPointsLessThanPointsTriggerAssertGameSpeedStaysTheSame()
    {
        //Wait for the TimeStrand scene to be loaded
        yield return new WaitWhile(() => sceneLoaded == false);

        //Arrange
        float orgSpeed = Settings.scrollSpeed;

        //Act
        GameController.Instance.AddPoints(Settings.pointsPerHit);

        //Assert
        float newSpeed = Settings.scrollSpeed;
        Assert.AreEqual(newSpeed, orgSpeed);
    }

}