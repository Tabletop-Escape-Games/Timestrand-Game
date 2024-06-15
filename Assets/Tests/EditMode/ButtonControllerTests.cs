using System;
using Controllers;
using NUnit.Framework;

[TestFixture]
public class ButtonControllerTests
{
    private ButtonController _controller;
    private string _colorTag = "test";
    private float _direction = 0f;

    [SetUp]
    public void SetUp()
    {
        _controller = new ButtonController(_colorTag, _direction);
    }

    [Test]
    public void GivenNewControllerAssertThatNotPressed()
    {
        //Assert
        Assert.IsFalse(_controller.IsPressed());
    }

    [Test]
    public void GivenNewControllerAssertThatHasColorTagIsTrue()
    {
        //Assert
        Assert.True(_controller.HasColorTag(_colorTag));
    }

    [Test]
    public void GivenPressAssertThatIsPressedIsTrue()
    {
        //Act
        _controller.Press();

        //Assert
        Assert.IsTrue(_controller.IsPressed());
    }

    [Test]
    public void GivenReleaseAssertThatIsPressedIsFalse()
    {
        //Arrange
        _controller.Press();

        //Act
        _controller.Release();

        //Assert
        Assert.IsFalse(_controller.IsPressed());
    }
}
