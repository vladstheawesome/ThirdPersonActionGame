using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonGame.Interact
{
    public class Ledge : MonoBehaviour
    {
        public Vector3 Offset_HangingBrace;
        public float characterXRotation;
        public Vector3 EndPosition;

        public static bool IsLedge(GameObject obj)
        {
            if (obj.GetComponent<Ledge>() == null)
            {
                return false;
            }

            return true;
        }
    }
}
