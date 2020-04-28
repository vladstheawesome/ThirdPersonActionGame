using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.Control;
using ThirdPersonGame.PooledObjects;
using UnityEngine;

namespace ThirdPersonGame.Core
{
    public class AnimationProgress : MonoBehaviour
    {
        public bool Jumped;
        public List<PoolObjectType> PoolObjectList = new List<PoolObjectType>();
        public bool AttackTriggered;
        public float MaxPressTime;
        //public bool disallowEarlyTurn;

        private CharacterControl control;
        private float PressTime;

        private void Awake()
        {
            control = GetComponentInParent<CharacterControl>();
            PressTime = 0f;
        }

        private void Update()
        {
            if (control.Attack)
            {
                PressTime += Time.deltaTime;
            }
            else
            {
                // If you let go of the button
                PressTime = 0f;
            }

            if (PressTime == 0f)
            {
                AttackTriggered = false;
            }
            else if (PressTime > MaxPressTime)
            {
                AttackTriggered = false;
            }
            else
            {
                AttackTriggered = true;
            }
        }
    }
}

