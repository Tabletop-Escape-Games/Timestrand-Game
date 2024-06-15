using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class ScoreController
    {
        private int _score;
        private IScoreStrategy _strategy;

        public ScoreController(IScoreStrategy scoreStrategy, int score = 0)
        {
            _score = score;
            _strategy = scoreStrategy;
        }

        public int Score { get { return _score; } }

        public void AddPoints(int points)
        {
            _score = _strategy.CalculateScore(_score, points);
        }
    }
}