using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.Control;
using UnityEngine;

namespace ThirdPersonGame.Core
{
    public class ManualInput : MonoBehaviour
    {
        private CharacterControl characterControl;

        private void Awake()
        {
            characterControl = this.gameObject.GetComponent<CharacterControl>();
        }

        // Update is called once per frame
        void Update()
        {
            // Control
            if (VirtualInputManager.Instance.MoveForward)
            {
                characterControl.MoveForward = true;
            }
            else
            {
                characterControl.MoveForward = false;
            }

            if (VirtualInputManager.Instance.MoveBackwards)
            {
                characterControl.MoveBackwards = true;
            }
            else
            {
                characterControl.MoveBackwards = false;
            }

            if (VirtualInputManager.Instance.Jump)
            {
                characterControl.Jump = true;
            }
            else
            {
                characterControl.Jump = false;
            }

            if (VirtualInputManager.Instance.StrafeRight)
            {
                characterControl.StrafeRight = true;
            }
            else
            {
                characterControl.StrafeRight = false;
            }

            if (VirtualInputManager.Instance.StrafeLeft)
            {
                characterControl.StrafeLeft = true;
            }
            else
            {
                characterControl.StrafeLeft = false;
            }

            // Combat
            if (VirtualInputManager.Instance.Attack)
            {
                characterControl.Attack = true;
            }
            else
            {
                characterControl.Attack = false;
            }
        }
    }
}
