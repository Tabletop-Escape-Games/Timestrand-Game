using Interfaces;
using UnityEngine;

namespace Controllers.LineControllers
{
    public class BMLineController : ILineController
    {
        public Vector3 GetDirection()
        {
            if (Input.GetKey(KeyCode.B))
            {
                return new Vector3(-1f, 0f, 0f).normalized;
            }
            else if (Input.GetKey(KeyCode.M))
            {
                return new Vector3(1f, 0f, 0f).normalized;
            }
            return new Vector3(0f, 1f, 0f);
        }
    }
}