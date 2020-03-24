using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonGame.Core
{
    public class VirtualInputManager : Singleton<VirtualInputManager>
    {
        //public static VirtualInputManager Instance = null;
        
        public bool MoveForward;
        public bool MoveBackwards;
        public bool Jump;
        public bool Attack;
        public bool StrafeRight;
        public bool StrafeLeft;
    }
}
