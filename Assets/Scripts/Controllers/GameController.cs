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
        
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private ButtonPositionUI buttonPositionUI;
        [SerializeField] private Slider _scoreBar;

        [Tooltip("The amount of points to award when a line hits a target of the same color")]
        [SerializeField] int _pointsPerHit = 2;
        
        [Tooltip("When the player reaches a multiple of this number a game action depending of the mode will be started")]
        [SerializeField] int _pointsTrigger = 10;

        [Tooltip("The mode in which this game will run")]
        [SerializeField] GameMode _gameMode = GameMode.Basic;

        private int _score;
        private int _maxScore;

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

            // Calculate the max achievable score
            _maxScore = 40 * Settings.pointsPerHit; // There are currently 40 targets drawn on the canvas

            // Update de UI
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
            // Calculate score
            _score = scoreStrategy.CalculateScore(_score, points);

            // Update the UI
            UpdateScoreUI();

            // If the player has a multiple of the trigger, execute action based on game mode
            if (_score % Settings.pointsTrigger == 0)
            {
                switch (Settings.gameMode)
                {
                    case GameMode.SwitchButtons:
                        // Switch buttons
                        buttonPositionUI = FindFirstObjectByType<ButtonPositionUI>();
                        buttonPositionUI.ChangeButtonConfiguration();
                        Debug.LogWarning("Switch buttons!");
                        break;
                    case GameMode.SpeedUp:
                        Settings.scrollSpeed += 0.5f;
                        Debug.LogWarning("Speed up!");
                        break;
                    case GameMode.Basic:
                    default:
                        // Do nothing
                        break;
                }
            }
        }
        
        private TextMeshProUGUI FindScoreText()
        {
            if (_scoreText == null)
            {
                // Probeer een TextMeshProUGUI object te vinden met de tag "ScoreText"
                GameObject scoreTextObject = GameObject.FindWithTag("ScoreText");
                if (scoreTextObject != null)
                {
                    _scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
                    Debug.Log("ScoreText found");
                } else Debug.LogWarning("ScoreText not found");
            } 
            return _scoreText;
        }

        private Slider FindScoreBar()
        {
            if (_scoreBar == null)
            {
                // Probeer een Slider object te vinden met de tag "ScoreBar"
                GameObject scoreBarObject = GameObject.FindWithTag("ScoreBar");
                if (scoreBarObject != null)
                {
                    _scoreBar = scoreBarObject.GetComponent<Slider>();
                }
            } 
            return _scoreBar;
        }
        
        private void UpdateScoreUI()
        {
            _scoreText = FindScoreText();
            if (_scoreText != null)
            {
                _scoreText.text = $"Score: {_score}";
            } else Debug.LogWarning($"ScoreText not assigned. Score: {_score}");

            _scoreBar = FindScoreBar();
            if (_scoreBar != null)
            {
                _scoreBar.value = (float)_score / _maxScore;
            }
            else Debug.LogWarning("ScoreBar not assigned");
        }
    }
}