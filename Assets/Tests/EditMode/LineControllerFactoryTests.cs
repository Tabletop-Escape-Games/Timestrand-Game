using System;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using NUnit.Framework;
using Controllers.LineControllers;
using Interfaces;

[TestFixture]
public class LineControllerFactoryTests
{
    [Test]
    public void GivenControlTypeArrowsExpectFactoryToReturnArrowKeyLineController()
    {
        //Arrange
        string controlType = "arrowKeys";

        //Act
        ILineController controller = LineControllerFactory.CreateLineController(controlType);

        //Assert
        Assert.IsInstanceOf<ArrowKeyLineController>(controller);
    }

    [Test]
    public void GivenControlTypeZCExpectFactoryToReturnZCLineController()
    {
        //Arrange
        string controlType = "ZC";

        //Act
        ILineController controller = LineControllerFactory.CreateLineController(controlType);

        //Assert
        Assert.IsInstanceOf<ZCLineController>(controller);
    }

    [Test]
    public void GivenEmptyStringAsControlTypeExpectFactoryToReturnNull()
    {
        //Arrange
        string controlType = string.Empty;

        //Act
        ILineController controller = LineControllerFactory.CreateLineController(controlType);

        //Assert
        Assert.IsNull(controller);
    }

    [Test]
    public void GivenNullAsControlTypeExpectFactoryToReturnNull()
    {
        //Arrange
        string controlType = null;

        //Act
        ILineController controller = LineControllerFactory.CreateLineController(controlType);

        //Assert
        Assert.IsNull(controller);
    }

    [Test]
    public void GivenCreateButtonLineControllerExpectReturnButtonLineController()
    {
        //Arrange
        string controlType = null;
        List<ButtonController> buttons = new List<ButtonController>();

        //Act
        ILineController result = LineControllerFactory.CreateButtonLineController(controlType, buttons);

        //Assert
        Assert.IsInstanceOf<ButtonLineController>(result);
    }

    [Test]
    public void GivenNoMatchingButtonControllersExpectFactoryToReturnControllerWithoutButtonController()
    {
        //Arrange
        string controlType = "green";
        string blueType = "blue";
        string redType = "red";
        ButtonController blue1 = new ButtonController(blueType, -1);
        ButtonController blue2 = new ButtonController(blueType, 1);
        ButtonController red1 = new ButtonController(redType, -1);
        ButtonController red2 = new ButtonController(redType, 1);
        List<ButtonController> buttonControllers = new List<ButtonController>();
        buttonControllers.Add(blue1);
        buttonControllers.Add(blue2);
        buttonControllers.Add(red1);
        buttonControllers.Add(red2);

        //Act
        ButtonLineController controller = (ButtonLineController)LineControllerFactory.CreateButtonLineController(controlType, buttonControllers);
        int result = controller.GetButtonControllers().Count;

        //Assert
        Assert.Zero(result);
    }

    [Test]
    public void GivenTwoBlueButtonsTwoRedAndRequestTypeBlueExpectTheFactoryToReturnOnlyBlueButtonsInController()
    {
        //Arrange
        string blueType = "blue";
        string redType = "red";
        ButtonController blue1 = new ButtonController(blueType, -1);
        ButtonController blue2 = new ButtonController(blueType, 1);
        ButtonController red1 = new ButtonController(redType, -1);
        ButtonController red2 = new ButtonController(redType, 1);
        List<ButtonController> buttonControllers = new List<ButtonController>();
        buttonControllers.Add(blue1);
        buttonControllers.Add(blue2);
        buttonControllers.Add(red1);
        buttonControllers.Add(red2);
        int expectation = 2;

        //Act
        ButtonLineController controller = (ButtonLineController) LineControllerFactory.CreateButtonLineController(blueType, buttonControllers);
        int result = controller.GetButtonControllers().Count;

        //Assert
        Assert.AreEqual(expectation, result);
    }

    
}