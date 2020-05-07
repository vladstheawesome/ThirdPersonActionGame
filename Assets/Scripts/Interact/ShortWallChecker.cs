using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonGame.Interact
{
    public class ShortWallChecker : MonoBehaviour
    {
        public bool IsCrouchingOnWall;
        public ShortWall CrouchedWall;
        ShortWall CheckCrouchWall = null;

        private void OnTriggerEnter(Collider other)
        {
            CheckCrouchWall = other.gameObject.GetComponent<ShortWall>();
            if (CheckCrouchWall != null)
            {
                Debug.Log("Crouch on this wall");
                IsCrouchingOnWall = true;
                CrouchedWall = CheckCrouchWall;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            CheckCrouchWall = other.gameObject.GetComponent<ShortWall>();
            if (CheckCrouchWall != null)
            {
                IsCrouchingOnWall = false;
                CrouchedWall = null;
            }
        }
    }
}
