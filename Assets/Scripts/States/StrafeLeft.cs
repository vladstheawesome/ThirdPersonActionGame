using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.Control;
using UnityEngine;

namespace ThirdPersonGame.States
{
    [CreateAssetMenu(fileName = "New State", menuName = "ThirdPersonGame/AbilityData/StrafeLeft")]
    public class StrafeLeft : StateData
    {
        public float Speed;
        public AnimationCurve SpeedGraph;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (!control.StrafeLeft)
            {
                animator.SetBool(TransitionParameter.StrafeLeft.ToString(), false);
                return;
            }

            if (control.StrafeLeft)
            {
                control.PlayerStrafeLeft(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}
