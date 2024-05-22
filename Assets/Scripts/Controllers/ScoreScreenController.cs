using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreScreenController : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("TimeMachine");
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
