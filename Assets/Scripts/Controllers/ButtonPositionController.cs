using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class ButtonPositionController
    {
        private List<Vector2> _positions = new List<Vector2>();

        public ButtonPositionController(List<Vector2> positions)
        {
            // Copy all values to a new list in order to leave the original list intact
            foreach (var position in positions)
            {
                _positions.Add(position);
            }
        }

        public List<Vector2> ShufflePositions()
        {
            // Fisher-Yates shuffle algorithm to randomly shuffle positions
            for (int i = _positions.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (_positions[i], _positions[j]) = (_positions[j], _positions[i]);
            }
            return _positions;
        }
    }
}