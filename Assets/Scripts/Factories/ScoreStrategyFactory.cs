using Interfaces;

namespace Controllers.ScoreStrategies
{
    public static class ScoreStrategyFactory
    {
        public static IScoreStrategy CreateScoreStrategy(string scoreStrategyType)
        {
            return scoreStrategyType switch
            {
                "PointScore" => new PointScoreStrategy(),
                _ => null
            };
        }
    }
}