using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.Control;
using UnityEngine;

namespace ThirdPersonGame.States
{
    [CreateAssetMenu(fileName = "New State", menuName = "ThirdPersonGame/AbilityData/CrouchOnWall")]
    public class CrouchOnWall : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

            CharacterControl control = characterState.GetCharacterControl(animator);

            if (control.CrouchOnWall)
            {
                animator.SetBool(TransitionParameter.CrouchOnWall.ToString(), true);
            }
            //if (!control.CrouchOnWall)
            else
            {
                animator.SetBool(TransitionParameter.CrouchOnWall.ToString(), false);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}
