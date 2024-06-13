using Interfaces;
using System.Web;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Specialized;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }
        
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private ButtonPositionManager buttonPositionManager;
        private int _score;

        [Tooltip("The amount of points to award when a line hits a target of the same color")]
        [SerializeField] int _pointsPerHit = 2;
        
        [Tooltip("When the player reaches a multiple of this number a game action depending of the mode will be started")]
        [SerializeField] int _pointsTrigger = 10;

        [Tooltip("The mode in which this game will run")]
        [SerializeField] GameMode _gameMode = GameMode.Basic;

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
            // Store the configuration provided to controller in the settings
            Settings.pointsPerHit = _pointsPerHit;
            Settings.pointsTrigger = _pointsTrigger;
            Settings.gameMode = _gameMode;
            
            // Depending on the platform try to get the configuration from parameters
            Debug.Log("Platform: " + Application.platform);
            switch (Application.platform)
            {
                case RuntimePlatform.WebGLPlayer:
                    Debug.Log("URL: " + Application.absoluteURL);

                    Uri uri = new Uri(Application.absoluteURL);
                    NameValueCollection parameters = HttpUtility.ParseQueryString(uri.Query);

                    if(Enum.TryParse(parameters["gameMode"], out GameMode gameMode))
                    {
                        Settings.gameMode = gameMode;
                    }

                    if(int.TryParse(parameters["pointsPerHit"], out int pointsPerHit) && pointsPerHit > 0)
                    {
                        Settings.pointsPerHit = pointsPerHit;
                    }

                    if (int.TryParse(parameters["pointsTrigger"], out int pointsTrigger) && pointsTrigger > 0)
                    {
                        Settings.pointsTrigger = pointsTrigger;
                    }
                    break;
                case RuntimePlatform.WindowsPlayer:
                default:
                    // Do nothing (variables provided to the controller will be used)
                    break;
            }
            Debug.Log($"Configuration used: GameMode: {Settings.gameMode}, Points per hit: {Settings.pointsPerHit}, Points trigger: {Settings.pointsTrigger}.");

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

            // If the player has a multiple of the trigger, execute action based on game mode
            if (_score % Settings.pointsTrigger == 0)
            {
                switch (Settings.gameMode)
                {
                    case GameMode.SwitchButtons:
                        // Switch buttons
                        buttonPositionManager = FindFirstObjectByType<ButtonPositionManager>();
                        buttonPositionManager.ChangeButtonConfiguration();
                        Debug.LogWarning("Switch!");
                        break;
                    case GameMode.Basic:
                    default:
                        // Do nothing
                        break;
                }
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