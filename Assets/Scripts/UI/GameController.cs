using Interfaces;
using Controllers;
using System.Web;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Specialized;
using UnityEngine.SocialPlatforms.Impl;
using Controllers.LineControllers;
using Controllers.ScoreStrategies;
using System.Collections.Generic;

namespace UI
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        [Tooltip("The amount of points to award when a line hits a target of the same color")]
        [SerializeField] int _pointsPerHit = 2;
        
        [Tooltip("When the player reaches a multiple of this number a game action depending of the mode will be started")]
        [SerializeField] int _pointsTrigger = 10;

        [Tooltip("The mode in which this game will run")]
        [SerializeField] GameMode _gameMode = GameMode.Basic;

        [Tooltip("The score strategy used for the game in string")]
        [SerializeField] string scoreStrategyType = "PointScore";

        private ScoreController _scoreController;
        private IScoreStrategy _scoreStrategy;

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
            // Load configuration and save it to settings
            SetConfiguration();

            // Create a score strategy and score controller
            _scoreStrategy = ScoreStrategyFactory.CreateScoreStrategy(scoreStrategyType);
            if (_scoreStrategy == null)
            {
                Debug.LogError($"Unable to find score strategy with type '{scoreStrategyType}'");
            }
            _scoreController = new ScoreController(_scoreStrategy);
        }

        private void SetConfiguration()
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

                    if (Enum.TryParse(parameters["gameMode"], true, out GameMode gameMode))
                    {
                        Settings.gameMode = gameMode;
                    }

                    if (int.TryParse(parameters["pointsPerHit"], out int pointsPerHit) && pointsPerHit > 0)
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
        }

        // Is called by a button press on MainMenu
        public void StartGame()
        {
            _scoreController = new ScoreController(_scoreStrategy);
            SceneManager.LoadScene("TimeMachine");
        }

        // Is called when the camera hits an object with GameOver tag
        public void GameOver()
        {
            SceneManager.LoadScene("ScoreScreen");
        }

        // Is called by a button press on ScoreScreen
        public void RestartGame()
        {
            StartGame();
        }

        // Is called by a button press on ScoreScreen
        public void QuitGame()
        {
            Application.Quit();
        }
        
        public int GetScore()
        {
            return _scoreController.Score;
        }
        
        public void AddPoints()
        {
            AddPoints(Settings.pointsPerHit);
        }

        public void AddPoints(int points)
        {
            // Calculate score
            _scoreController.AddPoints(points);

            // Update the UI
            UpdateScoreUI();

            // If the player has a multiple of the trigger, execute action based on game mode
            if (_scoreController.Score % Settings.pointsTrigger == 0)
            {
                switch (Settings.gameMode)
                {
                    case GameMode.SwitchButtons:
                        // Switch buttons
                        ButtonManager buttonPositionUI = FindFirstObjectByType<ButtonManager>();
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

        public void UpdateScoreUI ()
        {
            ScoreUI scoreUI = FindFirstObjectByType<ScoreUI>();
            if (scoreUI != null)
            {
                scoreUI.UpdateScore(GetScore());
            }
        }
        
    }
}