using Interfaces;
using UI;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

namespace Controllers
{
    public class ButtonLineController : ILineController
    {
        private List<ButtonController> _buttons;

        // Controller to control the line with buttons placed in screen. The constructor takes a string corresponding with the color tag
        // of the line to control (can be used like this in factory).
        public ButtonLineController( List<ButtonController> buttons)
        {
            _buttons = buttons;
        }

        public Vector3 GetDirection()
        {
            Vector3 direction = Vector3.up;

            // Check all controllers and check if the button is currently pressed
            foreach (ButtonController button in _buttons)
            {
                // if so, set the direction matching to the direction defined in the controller
                if (button.IsPressed())
                {
                    direction.x += button.GetDirection();
                }
            }

            return direction;
        }

        public IReadOnlyCollection<ButtonController> GetButtonControllers() 
        {
            return _buttons.AsReadOnly();
        }
    }
}