namespace Controllers
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class ButtonPositionUI : MonoBehaviour
    {
        [Tooltip("List of all the buttons that should be managed")]
        [SerializeField] private List<RectTransform> _buttons;
        
        private ButtonPositionController _positionController;

        void Start()
        {
            List<Vector2> positions = new List<Vector2>();
            foreach (var button in _buttons)
            {
                positions.Add(button.anchoredPosition);
            }

            _positionController = new ButtonPositionController(positions);
        }

        public void ChangeButtonConfiguration()
        {
            List<Vector2> positions = _positionController.ShufflePositions();
            UpdateButtonPositions(positions);
        }

        private void UpdateButtonPositions(List<Vector2> positions)
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                RectTransform buttonTransform = _buttons[i];
                buttonTransform.anchorMin = new Vector2(0.5f, 0.5f); // Set anchorMin to center
                buttonTransform.anchorMax = new Vector2(0.5f, 0.5f); // Set anchorMax to center
                buttonTransform.anchoredPosition = positions[i];
            }
        }
    }

}