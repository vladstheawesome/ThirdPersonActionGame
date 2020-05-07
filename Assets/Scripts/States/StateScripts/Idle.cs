using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.Control;
using ThirdPersonGame.Core;
using UnityEngine;

namespace ThirdPersonGame.States
{
    [CreateAssetMenu(fileName = "New State", menuName = "ThirdPersonGame/AbilityData/Idle")]
    public class Idle : StateData
    {      

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameter.Jump.ToString(), false);
            animator.SetBool(TransitionParameter.Attack.ToString(), false);
            animator.SetBool(TransitionParameter.Move.ToString(), false);
            //animator.SetBool(TransitionParameter.CrouchOnWall.ToString(), false);
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (control.animationProgress.AttackTriggered)
            {
                animator.SetBool(TransitionParameter.Attack.ToString(), true);
            }

            if (control.Jump)
            {
                animator.SetBool(TransitionParameter.Jump.ToString(), true);
            }

            if (control.MoveForward && control.MoveBackwards)
            {
                // do nothing
            }

            else if (control.MoveForward)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), true);
            }

            else if (control.MoveBackwards)
            {
                animator.SetBool(TransitionParameter.MoveBack.ToString(), true);
            }

            if (control.StrafeRight)
            {
                animator.SetBool(TransitionParameter.StrafeRight.ToString(), true);
            }

            if (control.StrafeLeft)
            {
                animator.SetBool(TransitionParameter.StrafeLeft.ToString(), true);
            }

            if (!control.MoveForward && control.Crouch)
            {
                VirtualInputManager.Instance.Crouch = false;
            }

            //if (control.CrouchOnWall)
            //{
            //    //VirtualInputManager.Instance.CrouchOnWall = true;
            //    animator.SetBool(TransitionParameter.CrouchOnWall.ToString(), true);
            //}
            if (!control.CrouchOnWall)
            {
                //VirtualInputManager.Instance.CrouchOnWall = false;
                animator.SetBool(TransitionParameter.CrouchOnWall.ToString(), false);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameter.Attack.ToString(), false);
        }
    }
}
