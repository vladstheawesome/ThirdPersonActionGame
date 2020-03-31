using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonGame.Interact
{
    public static class VectorPosition 
    {
        public static Vector3 ChangeX(this Vector3 v, float x)
        {
            v.x = x;
            return v;
        }

        public static Vector3 ChangeY(this Vector3 v, float y)
        {
            v.y = y;
            return v;
        }

        public static Vector3 ChangeZ(this Vector3 v, float z)
        {
            v.z = z;
            return v;
        }
    }
}
