using Controllers;
using Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool buttonPressed { get; private set; } // indicates whether the buttons is currently pressed
    public float direction = 0f;                    // -1f means left, 0f means right
    public string colorTag;                         // color tag of the line to control

    // When the button is pressed we store this state
    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
        Debug.Log("Button "+colorTag+" pressed");
    }

    // And similarly when the button is released 
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
        Debug.Log("Button "+colorTag+" released");
    }

    // At start the button is not pressed
    void Start()
    {
        buttonPressed = false;
    }
}
