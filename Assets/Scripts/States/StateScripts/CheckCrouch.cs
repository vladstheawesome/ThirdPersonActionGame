using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.Control;
using UnityEngine;

namespace ThirdPersonGame.States
{
    [CreateAssetMenu(fileName = "New State", menuName = "ThirdPersonGame/AbilityData/CheckCrouch")]
    public class CheckCrouch : StateData
    {
        public bool MustRequireMovement;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (control.Crouch)
            {
                if (MustRequireMovement)
                {
                    if (control.MoveForward)
                    {
                        animator.SetBool(TransitionParameter.Crouch.ToString(), true);
                    }
                }
                else
                {
                    animator.SetBool(TransitionParameter.Crouch.ToString(), true);
                }
            }
            else
            {
                animator.SetBool(TransitionParameter.Crouch.ToString(), false);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}