using System;
using Controllers.ScoreStrategies;
using Interfaces;
using NUnit.Framework;

[TestFixture]
public class PointScoreStrategyTests
{
    [Test]
    public void GivenScoreOfTwoAndTwoPointsAssertThatScoreWillBeFour()
    {
        //Arrange
        IScoreStrategy strategy = new PointScoreStrategy();
        int score = 2;
        int points = 2;
        int expected = 4;

        //Act
        int result = strategy.CalculateScore(score, points);

        //Assert
        Assert.AreEqual(result, expected);
    }
}