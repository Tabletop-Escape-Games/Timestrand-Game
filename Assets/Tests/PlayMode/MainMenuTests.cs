using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UI;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class MainMenuTests
{
    private bool sceneLoaded;
    private bool clicked;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    [SetUp]
    public void SetUp()
    {
        clicked = false;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneLoaded = true;
    }

    private void Clicked()
    {
        clicked = true;
    }

    [UnityTest]
    public IEnumerator GivenClickOnStartGameAssertNewSceneIsLoaded()
    {
        //Wait for the scene to be loaded
        yield return new WaitWhile(() => (sceneLoaded == false && SceneManager.GetActiveScene().name == "MainMenu"));

        //Click on the start button
        var playButtonObj = GameObject.Find("PlayButton");
        var playButton = playButtonObj.GetComponent<Button>();
        clicked = false;
        playButton.onClick.AddListener(Clicked);
        playButton.onClick.Invoke();
        Assert.True(clicked);
        yield return new FixedUpdate();

        // Assert that the TimeMachine scene is loaded
        string newScene = SceneManager.GetActiveScene().name;
        string expectation = "TimeMachine";
        Assert.AreEqual(expectation, newScene);
    }    
}
