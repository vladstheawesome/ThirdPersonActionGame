﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonGame.Interact
{
    public class LedgeChecker : MonoBehaviour
    {
        public bool IsGrabbingLedge;
        public Ledge GrabbedLedge;
        Ledge CheckLedge = null;

        private void OnTriggerEnter(Collider other)
        {
            CheckLedge = other.gameObject.GetComponent<Ledge>();
            if (CheckLedge != null)
            {
                IsGrabbingLedge = true;
                GrabbedLedge = CheckLedge;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            CheckLedge = other.gameObject.GetComponent<Ledge>();
            if (CheckLedge != null)
            {
                IsGrabbingLedge = false;
                GrabbedLedge = null;
            }
        }
    }
}
