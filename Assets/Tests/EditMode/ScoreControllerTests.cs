using System;
using Controllers;
using Interfaces;
using NUnit.Framework;

[TestFixture]
public class ScoreControllerTests
{
    private IScoreStrategy _strategy;

    [SetUp]
    public void SetUp()
    {
        _strategy = new MockScoreStrategy();
    }

    [Test]
    public void GivenNewControllerAssertThatScoreIsZero()
    {
        //Act
        ScoreController controller = new ScoreController(_strategy);

        //Assert
        Assert.Zero(controller.Score);
    }

    [Test]
    public void GivenTwoPointsAssertThatScoreIsTwo()
    {
        //Arrange
        ScoreController controller = new ScoreController(_strategy);
        int points = 2;

        //Act
        controller.AddPoints(points);

        //Assert
        Assert.AreEqual(points, controller.Score);
    }

}