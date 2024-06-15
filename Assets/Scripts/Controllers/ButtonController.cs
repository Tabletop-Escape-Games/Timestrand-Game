using UnityEngine.EventSystems;
using UnityEngine;

namespace Controllers
{
    public class ButtonController
    {
        private bool _isPressed;
        private string _colorTag;
        private float _direction;

        public ButtonController(string colorTag, float direction)
        {
            _isPressed = false;
            _colorTag = colorTag;

            if(direction < 0)
            {
                _direction = -1f;
            } else if (direction > 0)
            {
                _direction = 1f;
            } else
            {
                _direction = 0f;
            }
        }

        public void Press() { _isPressed = true; }

        public void Release() { _isPressed = false; }

        public bool IsPressed() { return _isPressed; }

        public float GetDirection() { return _direction; }

        public bool HasColorTag(string colorTag) { return _colorTag == colorTag; }
    }
}