using Controllers;
using Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ButtonUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    [Tooltip("The direction in which the line should move, -1f means left and 1f means right.")]
    public float direction = 0f;

    [SerializeField]
    [Tooltip("The color tag of the line to control.")]
    public string colorTag;
    
    public ButtonController Controller { get; private set; }

    // Add creation create a new controller
    void Start()
    {
        Controller = new ButtonController(colorTag);
    }

    // When the button is pressed we store this state
    public void OnPointerDown(PointerEventData eventData)
    {
        Controller.Press();
        Debug.Log("Button "+colorTag+" pressed");
    }

    // And similarly when the button is released 
    public void OnPointerUp(PointerEventData eventData)
    {
        Controller.Release();
        Debug.Log("Button "+colorTag+" released");
    }    
}
