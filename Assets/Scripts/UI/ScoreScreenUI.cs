using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class ScoreScreenController : MonoBehaviour
    {
        private void Start()
        {
            UpdateScoreUI(GameController.Instance.GetScore());
        }
        public void UpdateScoreUI(int score)
        {
            FindFirstObjectByType<ScoreUI>().UpdateScore(score);
        }
    
        public void RestartGame()
        {
            GameController.Instance.RestartGame();
        }
    
        public void ExitGame()
        {
            GameController.Instance.QuitGame();
        }
    }
}
