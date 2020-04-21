using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.Control;
using UnityEngine;

namespace ThirdPersonGame.States
{
    [CreateAssetMenu(fileName = "New State", menuName = "ThirdPersonGame/AbilityData/Jump")]
    public class Jump : StateData
    {
        [Range(0f, 1f)]
        public float JumpTiming;
        public float JumpForce;
        //public AnimationCurve Gravity;
        public AnimationCurve Pull;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (JumpTiming == 0f)
            {
                CharacterControl control = characterState.GetCharacterControl(animator);

                characterState.GetCharacterControl(animator).RIGID_BODY.AddForce(Vector3.up * JumpForce);
                control.animationProgress.Jumped = true;
            }
            animator.SetBool(TransitionParameter.Grounded.ToString(), false);
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            control.PullMultiplier = Pull.Evaluate(stateInfo.normalizedTime);
            if (!control.animationProgress.Jumped && stateInfo.normalizedTime >= JumpTiming)
            {
                characterState.GetCharacterControl(animator).RIGID_BODY.AddForce(Vector3.up * JumpForce);
                control.animationProgress.Jumped = true;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            control.PullMultiplier = 0f;
            control.animationProgress.Jumped = false;
        }
    }
}
