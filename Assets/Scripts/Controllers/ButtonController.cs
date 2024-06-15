using UnityEngine.EventSystems;
using UnityEngine;

public class ButtonController
{
    private bool _isPressed;
    private string _colorTag;

    public ButtonController(string colorTag)
    {
        _isPressed = false;
        _colorTag = colorTag;
    }

    public void Press() { _isPressed = true; }

    public void Release() { _isPressed = false; }

    public bool IsPressed() { return _isPressed;}

    public bool HasColorTag(string colorTag) { return _colorTag == colorTag;}
}
