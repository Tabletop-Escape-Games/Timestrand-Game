using System.Collections;
using NUnit.Framework;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class GamePlayThrough
{
    [UnityTest]
    public IEnumerator FullGamePlaythrough()
    {
        // Start de game vanaf het hoofdmenu
        SceneManager.LoadScene("MainMenu");
        yield return new WaitForSeconds(1);

        // Simuleer het drukken op de Start-knop om het spel te beginnen
        var playButton = GameObject.Find("PlayButton");
        Assert.IsNotNull(playButton, "PlayButton not found in MainMenu");
        playButton.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
        yield return new WaitForSeconds(1);

        // Controleer of de juiste scène is geladen
        yield return WaitForSceneToLoad("TimeMachine");

        // Simuleer de speler die punten verzamelt
        var gameController = GameObject.FindObjectOfType<UI.GameController>();
        Assert.IsNotNull(gameController, "GameController not found in TimeMachine scene");

        // Voeg punten toe en voer game logica uit
        for (int i = 0; i < 5; i++)
        {
            gameController.AddPoints(10);
            yield return new WaitForSeconds(0.5f);
        }

        // Simuleer een game-over door een trigger collision te veroorzaken
        var drawPointUI = GameObject.FindObjectOfType<DrawpointUI>();
        Assert.IsNotNull(drawPointUI, "DrawpointUI not found in TimeMachine scene");

        // Maak een gameOver collider
        GameObject gameOverObject = new GameObject("GameOverCollider");
        gameOverObject.tag = "gameOver";
        var gameOverCollider = gameOverObject.AddComponent<BoxCollider2D>();
        gameOverCollider.isTrigger = true;
        gameOverObject.transform.position = drawPointUI.transform.position;

        // Simuleer de trigger collision
        drawPointUI.OnTriggerEnter2D(gameOverCollider);
        yield return new WaitForSeconds(1);

        // Controleer of de ScoreScreen scène is geladen
        yield return WaitForSceneToLoad("ScoreScreen");

        // Simuleer het drukken op de Restart-knop om het spel opnieuw te starten
        var restartButton = GameObject.Find("RestartButton");
        Assert.IsNotNull(restartButton, "RestartButton not found in ScoreScreen");
        restartButton.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
        yield return new WaitForSeconds(1);

        // Controleer of de TimeMachine scène opnieuw is geladen
        yield return WaitForSceneToLoad("TimeMachine");

        // Simuleer de speler die punten verzamelt
        gameController = GameObject.FindObjectOfType<UI.GameController>();
        Assert.IsNotNull(gameController, "GameController not found in TimeMachine scene");
        for (int i = 0; i < 4; i++)
        {
            gameController.AddPoints(10);
            yield return new WaitForSeconds(0.5f);
        }

        // Simuleer een game-over door een nieuwe trigger collision te veroorzaken
        drawPointUI = GameObject.FindObjectOfType<DrawpointUI>();
        Assert.IsNotNull(drawPointUI, "DrawpointUI not found in TimeMachine scene");

        // Maak opnieuw een gameOver collider
        gameOverObject = new GameObject("GameOverCollider");
        gameOverObject.tag = "gameOver";
        gameOverCollider = gameOverObject.AddComponent<BoxCollider2D>();
        gameOverCollider.isTrigger = true;
        gameOverObject.transform.position = drawPointUI.transform.position;

        // Simuleer de trigger collision
        drawPointUI.OnTriggerEnter2D(gameOverCollider);
        yield return new WaitForSeconds(1);

        // Controleer of de ScoreScreen scène opnieuw is geladen
        yield return WaitForSceneToLoad("ScoreScreen");

        // Simuleer het drukken op de Quit-knop om het spel af te sluiten
        var quitButton = GameObject.Find("QuitButton");
        Assert.IsNotNull(quitButton, "QuitButton not found in ScoreScreen");
        quitButton.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
    }

    private IEnumerator WaitForSceneToLoad(string sceneName)
    {
        while (SceneManager.GetActiveScene().name != sceneName)
        {
            yield return null;
        }
    }
}
