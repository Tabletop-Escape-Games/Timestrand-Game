using Interfaces;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class ButtonLineController : ILineController
{
    private string colorTag;    // the color tag of the timestrand to move

    // Controller to control the line with buttons placed in screen. The constructor takes a string corresponding with the color tag
    // of the line to control (can be used like this in factory).
    public ButtonLineController(string colorTag)
    {
        this.colorTag = colorTag;
    } 

    public Vector3 GetDirection()
    {
        ButtonController[] controllers = GameObject.FindObjectsOfType<ButtonController>();
        Vector3 direction = new Vector3(0f, 1f, 0f);

        // Check all controllers and verify if they are set to control this controllers color, and if the button is currently pressed
        foreach (ButtonController controller in controllers)
        {
            // if so, set the direction matching to the direction defined in the controller
            if(controller.colorTag == colorTag && controller.buttonPressed)
            {
                direction.x += controller.direction;
            }
        }

        return direction;
    }
}
