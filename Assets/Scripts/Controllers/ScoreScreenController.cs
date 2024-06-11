using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers
{
    public class ScoreScreenController : MonoBehaviour
    {
        [SerializeField] private Text scoreText;

        private void Start()
        {
            UpdateScoreUI(GameController.Instance.GetScore());
        }
        public void UpdateScoreUI(int score)
        {
            if (scoreText != null)
            {
                scoreText.text = "Score: " + score;
            }
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
