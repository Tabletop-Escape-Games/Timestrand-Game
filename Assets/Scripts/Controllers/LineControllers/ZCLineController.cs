using Interfaces;
using UnityEngine;

namespace Controllers.LineControllers
{
    public class ZCLineController : ILineController
    {
        public Vector3 GetDirection()
        {
            if (Input.GetKey(KeyCode.Z))
            {
                return new Vector3(-1f, 0f, 0f).normalized;
            }
            else if (Input.GetKey(KeyCode.C))
            {
                return new Vector3(1f, 0f, 0f).normalized;
            }
            return new Vector3(0f, 1f, 0f);
        }
    }
}