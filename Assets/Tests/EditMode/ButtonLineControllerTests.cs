using System;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using NUnit.Framework;

[TestFixture]
public class ButtonLineControllerTests
{
    private ButtonLineController _controller;
    private List<ButtonController> _buttons;

    string colorTag = "Test";
    float left = -1;
    float right = 1;

    ButtonController left1;
    ButtonController left2;
    ButtonController right1;
    ButtonController right2;


    [SetUp]
    public void SetUp()
    {
        left1 = new ButtonController(colorTag, left);
        left2 = new ButtonController(colorTag, left);
        right1 = new ButtonController(colorTag, right);
        right2 = new ButtonController(colorTag, right);

        _buttons = new List<ButtonController>
        {
            left1,
            left2,
            right1,
            right2
        };

        _controller = new ButtonLineController(_buttons);
    }

    [Test]
    public void GivenButtonLeftIsPressedAssertThatDirectionIsUpAndLeft()
    {
        //Arrange
        left1.Press();
        Vector3 expectation = (Vector3.left + Vector3.up);

        //Act
        Vector3 direction = _controller.GetDirection();

        //Assert
        Assert.AreEqual(expectation, direction);
    }

    [Test]
    public void GivenButtonRightIsPressedAssertThatDirectionIsUpAndRight()
    {
        //Arrange
        right1.Press();
        Vector3 expectation = (Vector3.right + Vector3.up);

        //Act
        Vector3 direction = _controller.GetDirection();

        //Assert
        Assert.AreEqual(expectation, direction);
    }

    [Test]
    public void GivenBothButtonLeftAndButtonRightArePressedAssertThatDirectionIsOnlyUp()
    {
        //Arrange
        left1.Press();
        right1.Press();
        Vector3 expectation = Vector3.up;

        //Act
        Vector3 direction = _controller.GetDirection();

        //Assert
        Assert.AreEqual(expectation, direction);
    }

    [Test]
    public void GivenTwoButtonsLeftArePressedAssertThatDirectionIsTwiceLeftAndOnceUp()
    {
        //Arrange
        left1.Press();
        left2.Press();
        Vector3 expectation = (Vector3.left + Vector3.left + Vector3.up);

        //Act
        Vector3 direction = _controller.GetDirection();

        //Assert
        Assert.AreEqual(expectation, direction);
    }

    [Test]
    public void GivenButtonWithDirecton2fAndPressedAssertThatDirectionIs1f()
    {
        //Arrange
        ButtonController right3 = new ButtonController(colorTag, 2f);
        _buttons.Add(right3);
        _controller = new ButtonLineController(_buttons);
        right3.Press();
        Vector3 expectation = (Vector3.right + Vector3.up);

        //Act
        Vector3 direction = _controller.GetDirection();

        //Assert
        Assert.AreEqual(expectation, direction);
    }
}