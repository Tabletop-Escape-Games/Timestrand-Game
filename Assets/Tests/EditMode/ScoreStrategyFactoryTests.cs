using Controllers.LineControllers;
using Controllers.ScoreStrategies;
using Interfaces;
using NUnit.Framework;

[TestFixture]
public class ScoreStrategyFactoryTests
{
    [Test]
    public void GivenStrategyTypePointScoreExpectFactoryToReturnPointScoreStrategy()
    {
        //Arrange
        string controlType = "PointScore";

        //Act
        IScoreStrategy strategy = ScoreStrategyFactory.CreateScoreStrategy(controlType);

        //Assert
        Assert.IsInstanceOf<PointScoreStrategy>(strategy);
    }

    [Test]
    public void GivenEmptyStringAsControlTypeExpectFactoryToReturnNull()
    {
        //Arrange
        string controlType = string.Empty;

        //Act
        IScoreStrategy strategy = ScoreStrategyFactory.CreateScoreStrategy(controlType);

        //Assert
        Assert.IsNull(strategy);
    }

    [Test]
    public void GivenNullAsControlTypeExpectFactoryToReturnNull()
    {
        //Arrange
        string controlType = null;

        //Act
        IScoreStrategy strategy = ScoreStrategyFactory.CreateScoreStrategy(controlType);

        //Assert
        Assert.IsNull(strategy);
    }
}