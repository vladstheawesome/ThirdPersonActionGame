using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.Control;
using UnityEngine;

namespace ThirdPersonGame.States
{
    [CreateAssetMenu(fileName = "New State", menuName = "ThirdPersonGame/AbilityData/CheckCrouchAndMovement")]
    public class CheckCrouchAndMovement : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (control.MoveForward && control.Crouch)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), true);
                animator.SetBool(TransitionParameter.Crouch.ToString(), true);
            }
            else if (control.MoveForward && !control.Crouch)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), true);
                animator.SetBool(TransitionParameter.Crouch.ToString(), false);
            }      
            else if (!control.MoveForward && control.Crouch)
            {
                control.Crouch = false;
                ThirdPersonGame.Core.VirtualInputManager.Instance.Crouch = false;
            }
            else
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
                animator.SetBool(TransitionParameter.Crouch.ToString(), false);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
    }
}
