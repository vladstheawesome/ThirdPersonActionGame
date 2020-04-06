using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.Control;
using UnityEngine;

namespace ThirdPersonGame.Interact
{
    public class UngroundedConstraints
    {
        public bool IsPlayerOnLedge(CharacterControl control)
        {
            var isOnLedge = control.ledgeChecker.IsGrabbingLedge;
            return isOnLedge;
        }
    }
}
