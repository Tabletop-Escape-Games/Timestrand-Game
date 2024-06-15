using Interfaces;

public class MockScoreStrategy : IScoreStrategy
{
    public int CalculateScore(int score, int points)
    {
        return score + points;
    }
}