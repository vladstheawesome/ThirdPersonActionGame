﻿using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.Control;
using ThirdPersonGame.Core;
using UnityEngine;

namespace ThirdPersonGame.States
{
    [CreateAssetMenu(fileName = "New State", menuName = "ThirdPersonGame/AbilityData/CheckTurboAndMovement")]
    public class CheckTurboAndMovement : StateData
    {

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (control.MoveForward && control.Turbo)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), true);
                animator.SetBool(TransitionParameter.Turbo.ToString(), true);
            }
            else
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
                animator.SetBool(TransitionParameter.Turbo.ToString(), false);
            }

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}
