using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.Control;
using UnityEngine;

namespace ThirdPersonGame.States
{
    [CreateAssetMenu(fileName = "New State", menuName = "ThirdPersonGame/AbilityData/CheckTurbo")]
    public class CheckTurbo : StateData
    {
        public bool MustRequireMovement;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (control.Turbo)
            {
                if (MustRequireMovement)
                {
                    if(control.MoveForward)
                    {
                        animator.SetBool(TransitionParameter.Turbo.ToString(), true);
                    }
                }
                else
                {
                    animator.SetBool(TransitionParameter.Turbo.ToString(), true);
                }
            }
            else
            {
                animator.SetBool(TransitionParameter.Turbo.ToString(), false);
            }

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}