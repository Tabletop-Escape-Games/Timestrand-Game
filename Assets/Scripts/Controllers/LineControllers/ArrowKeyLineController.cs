using Interfaces;
using UnityEngine;

namespace Controllers.LineControllers
{
    public class ArrowKeyLineController : ILineController
    {
        public Vector3 GetDirection()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");

            if (moveHorizontal != 0)
            {
                return new Vector3(moveHorizontal, 0f, 0f).normalized;
            }
            return new Vector3(0f, 1f, 0f);
        }
    }
}