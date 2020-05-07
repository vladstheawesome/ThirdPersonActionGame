using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonGame.Interact
{
    public class ShortWall : MonoBehaviour
    {
        public static bool IsShortWall(GameObject obj)
        {
            if (obj.GetComponent<ShortWall>() == null)
            {
                return false;
            }

            return true;
        }

        public static bool IsShortWallChecker(GameObject obj)
        {
            if (obj.GetComponent<ShortWallChecker>() == null)
            {
                return false;
            }

            return true;
        }
    }
}
