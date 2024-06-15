using Interfaces;

namespace Controllers.ScoreStrategies
{
    public class PointScoreStrategy : IScoreStrategy
    {
        public int CalculateScore(int score, int points)
        {
            return score + points;
        }
    }
}