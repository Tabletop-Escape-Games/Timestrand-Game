using Interfaces;

namespace Controllers.ScoreControllers
{
    public class PointScoreStrategy : IScoreStrategy
    {
        public int CalculateScore(int score, int points)
        {
            return score + points;
        }
    }
}