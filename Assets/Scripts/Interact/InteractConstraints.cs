using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.Control;
using UnityEngine;

namespace ThirdPersonGame.Interact
{
    public class InteractConstraints
    {
        public bool IsPlayerOnLedge(CharacterControl control)
        {
            var isOnLedge = control.ledgeChecker.IsGrabbingLedge;
            return isOnLedge;
        }

        public bool IsPlayerCrouching(CharacterControl control)
        {
            var isCrouching = control.shortWallChecker.IsCrouchingOnWall;
            return isCrouching;
        }
    }
}
