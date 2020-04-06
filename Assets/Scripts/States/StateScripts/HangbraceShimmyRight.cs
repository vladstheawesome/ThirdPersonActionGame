using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.Control;
using ThirdPersonGame.Interact;
using ThirdPersonGame.States;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "ThirdPersonGame/AbilityData/HangbraceShimmyRight")]
public class HangbraceShimmyRight : StateData
{
    public float Speed;
    public AnimationCurve SpeedGraph;

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl control = CharacterManager.Instance.GetCharacter(animator);
        GameObject anim = control.SkinnedMeshAnimator.gameObject;

        if (!control.ShimmyRight)
        {
            animator.SetBool(TransitionParameter.ShimmyRight.ToString(), false);
            return;
        }

        if (control.ShimmyRight)
        {
            var localZ = control.ledgeChecker.GrabbedLedge.Offset_HangingBrace.z;

            control.SkinnedMeshAnimator.transform.localPosition = VectorPosition.ChangeZ(control.SkinnedMeshAnimator.transform.localPosition, localZ);

            control.PlayerStrafeOrShimmyRight(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
            control.SkinnedMeshAnimator.transform.position = control.transform.position;
            animator.SetBool(TransitionParameter.ShimmyRight.ToString(), true);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        
    }
}
