﻿using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.Control;
using ThirdPersonGame.Interact;
using ThirdPersonGame.States;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "ThirdPersonGame/AbilityData/HangbraceShimmyLeft")]
public class HangbraceShimmyLeft : StateData
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

        if (!control.ShimmyLeft)
        {
            animator.SetBool(TransitionParameter.ShimmyLeft.ToString(), false);
            return;
        }

        if (control.ShimmyLeft)
        {
            animator.SetBool(TransitionParameter.ShimmyLeft.ToString(), true);

            var localY = control.ledgeChecker.GrabbedLedge.Offset_HangingBrace.y;
            control.SkinnedMeshAnimator.transform.localPosition = VectorPosition.ChangeY(control.SkinnedMeshAnimator.transform.localPosition, localY);

            control.PlayerStrafeOrShimmyLeft(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
            control.SkinnedMeshAnimator.transform.position = control.transform.position;
      }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
