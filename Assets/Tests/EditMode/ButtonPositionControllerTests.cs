using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

[TestFixture]
public class ButtonPositionControllerTests
{
    List<Vector2> _positions;
    ButtonPositionController _controller;

    [SetUp]
    public void SetUp()
    {
        _positions = new List<Vector2>
        {
            new Vector2(1, 2),
            new Vector2(3, 4),
            new Vector2(5, 6),
            new Vector2(7, 8),
            new Vector2(9, 10),
            new Vector2(11, 12)
        };
        _controller = new ButtonPositionController(_positions);
    }

    [Test]
    public void GivenVectorsAssertThatCountStaysEqualAfterShuffle()
    {
        //Act
        List<Vector2> newPositions = _controller.ShufflePositions();

        //Assert
        Assert.AreEqual(_positions.Count, newPositions.Count);  // The amount of positions should remain the same
    }


    [Test]
    public void GivenVectorsAssertThatSequenceIsChangedAfterShuffle()
    {
        //Act
        List<Vector2> newPositions = _controller.ShufflePositions();

        //Assert
        Assert.AreNotEqual(_positions, newPositions); // The order should have changed, so the list themselves should not be the same
    }
    
    [Test]
    public void Assert_That_Vectors_Stay_The_Same_After_Shuffle()
    {
        //Act
        List<Vector2> newPositions = _controller.ShufflePositions();
        
        foreach (Vector2 position in _positions)
        {
            newPositions.Remove(position);
        }

        //Assert
        Assert.IsEmpty(newPositions, "Found a second occurrence of position coordinates"); // All the items from the first list should occur only once in the second list
    }

    [Test]
    public void Assert_That_Shuffle_Is_Random()
    {
        List<Vector2> newPositions = _controller.ShufflePositions();
        List<Vector2> newPositions2 = _controller.ShufflePositions();
        List<Vector2> newPositions3 = _controller.ShufflePositions();
        
        bool areEqual = newPositions.SequenceEqual(newPositions2) && newPositions.SequenceEqual(newPositions3);
        Assert.AreNotEqual(areEqual, false, "The shuffle is not random"); // The shuffle should be random
    }
}
