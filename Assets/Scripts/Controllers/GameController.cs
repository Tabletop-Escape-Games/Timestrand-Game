using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }
        
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private ButtonPositionManager buttonPositionManager;
        private int _score;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                Debug.Log("GameController instance created");
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void OnEnable()
        {
            // Registreer de callback voor als een scène geladen is
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            // Verwijder de registratie van de callback voor als een scène geladen is
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // Zoek de UI-elementen in de nieuwe scène
            FindScoreText();
            UpdateScoreUI();
        }
        
        private void Start()
        {
            // Probeer de scoreText te vinden in de huidige scène
            FindScoreText();
            UpdateScoreUI();
        }

        public void StartGame()
        {
            _score = 0;
            UpdateScoreUI();
            SceneManager.LoadScene("TimeMachine");
        }

        public void GameOver()
        {
            SceneManager.LoadScene("ScoreScreen");
            FindScoreText();
            UpdateScoreUI();
        }

        public void RestartGame()
        {
            _score = 0;
            SceneManager.LoadScene("TimeMachine");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
        
        public int GetScore()
        {
            return _score;
        }
        
        public void UpdateScore(IScoreStrategy scoreStrategy, int points)
        {
            _score = scoreStrategy.CalculateScore(_score, points);

            if(_score % 10 == 0)
            {
                buttonPositionManager = FindFirstObjectByType<ButtonPositionManager>();
                buttonPositionManager.ChangeButtonConfiguration();
                Debug.LogWarning("Switch!");
            }
        }
        
        private void FindScoreText()
        {
            if (scoreText == null)
            {
                // Probeer een TextMeshProUGUI object te vinden met de tag "ScoreText"
                GameObject scoreTextObject = GameObject.FindWithTag("ScoreText");
                if (scoreTextObject != null)
                {
                    scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
                    Debug.Log("ScoreText found");
                } else Debug.LogWarning("ScoreText not found");
            }
        }
        
        private void UpdateScoreUI()
        {
            if (scoreText != null)
            {
                Debug.Log($"Score: {_score}");
                scoreText.text = $"Score: {_score}";
            } else Debug.LogWarning("ScoreText not assigned");
        }
    }
}