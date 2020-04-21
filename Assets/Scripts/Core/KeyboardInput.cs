﻿using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.Control;
using ThirdPersonGame.Interact;
using ThirdPersonGame.States;
using UnityEngine;

namespace ThirdPersonGame.Core
{
    public class KeyboardInput : MonoBehaviour
    {
        //private CharacterControl characterControl;
        CharacterControl control;
        public Transform Player;

        private void Awake()
        {
            control = this.transform.GetComponent<CharacterControl>();
        }

        void Update()
        {
            control = Player.transform.root.GetComponent<CharacterControl>();
            GroundDetector checkGround = (GroundDetector) ScriptableObject.CreateInstance(typeof(GroundDetector));
            var ledge = control.ledgeChecker;

            // Control
            if (Input.GetKey(KeyCode.W) || (Input.GetMouseButton(0) && Input.GetMouseButton(1)))
            {
                if (checkGround.IsGrounded(control) || VirtualInputManager.Instance.Jump == true)
                {
                    VirtualInputManager.Instance.MoveForward = true;
                }
                else
                {
                    VirtualInputManager.Instance.MoveUp = true;
                }
            }
            else
            {
                if (checkGround.IsGrounded(control) || VirtualInputManager.Instance.Jump == true)
                {
                    VirtualInputManager.Instance.MoveForward = false;
                }
                else
                {
                    VirtualInputManager.Instance.MoveUp = false;
                }
            }

            if (Input.GetKey(KeyCode.S))
            {
                if (checkGround.IsGrounded(control))
                {
                    VirtualInputManager.Instance.MoveBackwards = true;
                }
                else
                {
                    VirtualInputManager.Instance.MoveDown = true;
                }
            }
            else
            {
                if (checkGround.IsGrounded(control))
                {
                    VirtualInputManager.Instance.MoveBackwards = false;
                }
                else
                {
                    VirtualInputManager.Instance.MoveDown = false;
                }
            }

            if (Input.GetKey(KeyCode.Space))
            {
                VirtualInputManager.Instance.Jump = true;
            }
            else
            {
                VirtualInputManager.Instance.Jump = false;
            }

            if (Input.GetKey(KeyCode.D))
            {
                VirtualInputManager.Instance.StrafeRight = true;
            }
            else
            {
                VirtualInputManager.Instance.StrafeRight = false;
            }

            if (Input.GetKey(KeyCode.A))
            {
                VirtualInputManager.Instance.StrafeLeft = true;
            }
            else
            {
                VirtualInputManager.Instance.StrafeLeft = false;
            }

            if (Input.GetKey(KeyCode.D) && ledge.IsGrabbingLedge)
            { 
                VirtualInputManager.Instance.ShimmyRight = true;
            }
            else
            {
                VirtualInputManager.Instance.ShimmyRight = false;
            }

            if (Input.GetKey(KeyCode.A) && ledge.IsGrabbingLedge)
            {
                VirtualInputManager.Instance.ShimmyLeft = true;
            }
            else
            {
                VirtualInputManager.Instance.ShimmyLeft = false;
            }

            // Combat
            if (Input.GetKey(KeyCode.F))
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
