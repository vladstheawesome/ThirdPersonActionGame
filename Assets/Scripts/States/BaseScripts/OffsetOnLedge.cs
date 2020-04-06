using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.Control;
using ThirdPersonGame.Core;
using ThirdPersonGame.Interact;
using UnityEngine;

namespace ThirdPersonGame.States
{
    [CreateAssetMenu(fileName = "New State", menuName = "ThirdPersonGame/AbilityData/OffsetOnLedge")]
    public class OffsetOnLedge : StateData
    {

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            GameObject anim = control.SkinnedMeshAnimator.gameObject;

            anim.transform.parent = control.ledgeChecker.GrabbedLedge.transform;        

            var localY = control.ledgeChecker.GrabbedLedge.Offset_HangingBrace.y;
            var localZ = control.ledgeChecker.GrabbedLedge.Offset_HangingBrace.z;
            var xRotation = control.ledgeChecker.GrabbedLedge.characterXRotation;

            anim.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            anim.transform.localPosition = VectorPosition.ChangeY(anim.transform.localPosition, localY /*-3.68f*/);
            anim.transform.localPosition = VectorPosition.ChangeZ(anim.transform.localPosition, localZ /*-1.03f*/);

            control.RIGID_BODY.velocity = Vector3.zero;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            GameObject anim = control.SkinnedMeshAnimator.gameObject;
            anim.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
