namespace Controllers
{
    using System.Collections.Generic;
    using UnityEngine;

    public class ButtonPositionManager : MonoBehaviour
    {
        public List<RectTransform> buttons;
        private List<Vector2> initialPositions = new List<Vector2>();

        void Start()
        {
            // Sla de initiële posities van de knoppen op
            foreach (var button in buttons)
            {
                initialPositions.Add(button.anchoredPosition);
            }
        }

        public void ChangeButtonConfiguration()
        {
            ShufflePositions();
            UpdateButtonPositions();
        }

        private void ShufflePositions()
        {
            // Fisher-Yates shuffle algorithm to randomly shuffle positions
            for (int i = initialPositions.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (initialPositions[i], initialPositions[j]) = (initialPositions[j], initialPositions[i]);
            }
        }

        private void UpdateButtonPositions()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                RectTransform buttonTransform = buttons[i];
                buttonTransform.anchorMin = new Vector2(0.5f, 0.5f); // Set anchorMin to center
                buttonTransform.anchorMax = new Vector2(0.5f, 0.5f); // Set anchorMax to center
                buttonTransform.anchoredPosition = initialPositions[i];
            }
        }
    }

}