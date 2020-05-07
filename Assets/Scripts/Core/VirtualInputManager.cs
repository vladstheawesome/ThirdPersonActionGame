using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonGame.Core
{
    public class VirtualInputManager : Singleton<VirtualInputManager>
    {
        //public static VirtualInputManager Instance = null;

        public bool Turbo;
        public bool MoveForward;
        public bool MoveBackwards;
        public bool Jump;
        public bool Attack;
        public bool StrafeRight;
        public bool StrafeLeft;
        public bool MoveUp;
        public bool MoveDown;
        public bool ShimmyRight;
        public bool ShimmyLeft;
        public bool Crouch;
        public bool CrouchOnWall;
    }
}
