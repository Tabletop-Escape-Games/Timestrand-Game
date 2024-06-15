using System;
using NUnit.Framework;

[TestFixture]
public class ButtonControllerTests
{
    private ButtonController _controller;
    private string _colorTag = "test";

    [SetUp]
    public void SetUp()
    {
        _controller = new ButtonController(_colorTag);
    }

    [Test]
    public void CreatedTest()
    {
        //Assert
        Assert.IsFalse(_controller.IsPressed());
    }

    [Test]
    public void HasColorTagTest()
    {
        //Assert
        Assert.True(_controller.HasColorTag(_colorTag));
    }

    [Test]
    public void PressTest()
    {
        //Act
        _controller.Press();

        //Assert
        Assert.IsTrue(_controller.IsPressed());
    }

    [Test]
    public void ReleaseTest()
    {
        //Arrange
        _controller.Press();

        //Act
        _controller.Release();

        //Assert
        Assert.IsFalse(_controller.IsPressed());
    }
}
