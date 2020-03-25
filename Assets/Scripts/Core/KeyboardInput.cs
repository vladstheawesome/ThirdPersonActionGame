using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonGame.Core
{
    public class KeyboardInput : MonoBehaviour
    {
        void Update()
        {
            // Control
            if (Input.GetKey(KeyCode.W) || (Input.GetMouseButton(0) && Input.GetMouseButton(1)))
            {
                VirtualInputManager.Instance.MoveForward = true;
            }
            else
            {
                VirtualInputManager.Instance.MoveForward = false;
            }

            if (Input.GetKey(KeyCode.S))
            {
                VirtualInputManager.Instance.MoveBackwards = true;
            }
            else
            {
                VirtualInputManager.Instance.MoveBackwards = false;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                VirtualInputManager.Instance.Jump = true;
            }
            else
            {
                VirtualInputManager.Instance.Jump = false;
            }

            if (Input.GetKey(KeyCode.E))
            {
                VirtualInputManager.Instance.StrafeRight = true;
            }
            else
            {
                VirtualInputManager.Instance.StrafeRight = false;
            }

            if (Input.GetKey(KeyCode.Q))
            {
                VirtualInputManager.Instance.StrafeLeft = true;
            }
            else
            {
                VirtualInputManager.Instance.StrafeLeft = false;
            }

            // Combat
            if (Input.GetKey(KeyCode.J))
            {
                VirtualInputManager.Instance.Attack = true;
            }
            else
            {
                VirtualInputManager.Instance.Attack = false;
            }
        }
    }
}
