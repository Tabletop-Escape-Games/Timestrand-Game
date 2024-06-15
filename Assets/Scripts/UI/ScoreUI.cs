using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScoreUI : MonoBehaviour
    {
        [Tooltip("The text field that contains the score in text")]
        [SerializeField] private TextMeshProUGUI _scoreText;

        [Tooltip("The slider displaying the score")]
        [SerializeField] private Slider _scoreBar;

        [Tooltip("The max achievable score")]
        [SerializeField] private float _maxScore;

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
                }
                else Debug.LogWarning("ScoreText not found");
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

        public void UpdateScore(int score)
        {
            _scoreText = FindScoreText();
            if (_scoreText != null)
            {
                _scoreText.text = $"Score: {score}";
            }
            else Debug.LogWarning($"ScoreText not assigned. Score: {score}");

            _scoreBar = FindScoreBar();
            if (_scoreBar != null)
            {
                _scoreBar.value = (float)score / _maxScore;
            }
            else Debug.LogWarning("ScoreBar not assigned");
        }
    }
}