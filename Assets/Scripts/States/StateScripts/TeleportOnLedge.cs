using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.Control;
using ThirdPersonGame.Core;
using ThirdPersonGame.Interact;
using UnityEngine;

namespace ThirdPersonGame.States
{
    [CreateAssetMenu(fileName = "New State", menuName = "ThirdPersonGame/AbilityData/TeleportOnLedge")]
    public class TeleportOnLedge : StateData
    {

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = CharacterManager.Instance.GetCharacter(animator);

            var yPosition = control.ledgeChecker.GrabbedLedge.transform.position.y + control.ledgeChecker.GrabbedLedge.EndPosition.y;
            var zPosition = control.ledgeChecker.GrabbedLedge.transform.position.z + control.ledgeChecker.GrabbedLedge.EndPosition.z;

            control.transform.position = VectorPosition.ChangeY(control.transform.position, yPosition);
            control.transform.position = VectorPosition.ChangeZ(control.transform.position, zPosition);

            if (control.ledgeChecker.GrabbedLedge.EndPosition.x > 0)
            {
                var xPosition = control.ledgeChecker.GrabbedLedge.transform.position.x + control.ledgeChecker.GrabbedLedge.EndPosition.x;
                control.transform.position = VectorPosition.ChangeX(control.transform.position, xPosition);
            }

            control.SkinnedMeshAnimator.transform.position = control.transform.position;
            control.SkinnedMeshAnimator.transform.parent = control.transform;
        }
    }
}
