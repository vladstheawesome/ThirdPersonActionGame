using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.Control;
using ThirdPersonGame.Core;
using ThirdPersonGame.Interact;
using UnityEngine;

namespace ThirdPersonGame.States
{
    [CreateAssetMenu(fileName = "New State", menuName = "ThirdPersonGame/AbilityData/MoveForward")]
    public class MoveForward : StateData
    {
        public bool AllowEarlyTurn;
        public bool LockDirection;
        public bool Constant;
        public AnimationCurve SpeedGraph;
        public float Speed;
        public float BlockDistance;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            #region
            //if (AllowEarlyTurn /*&& !control.animationProgress.disallowEarlyTurn*/)
            //{
            //    if (control.MoveForward)
            //    {
            //        control.FaceForward(true);
            //    }
            //}

            //control.animationProgress.disallowEarlyTurn = false;
            #endregion
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (control.Jump)
            {
                animator.SetBool(TransitionParameter.Jump.ToString(), true);
            }

            if (Constant)
            {
                ConstantMove(animator, stateInfo, control);
            }
            else 
            {
                ControlledMove(animator, stateInfo, control);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        private void ConstantMove(Animator animator, AnimatorStateInfo stateInfo, CharacterControl control)
        {
            if(!CheckFront(control))
            {
                control.PlayerMoveForward(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
            }
        }

        private void ControlledMove(Animator animator, AnimatorStateInfo stateInfo, CharacterControl control)
        {
            if (control.MoveForward && control.MoveBackwards)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
                animator.SetBool(TransitionParameter.MoveBack.ToString(), false);
                return;
            }

            if (!control.MoveForward && !control.MoveBackwards)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
                animator.SetBool(TransitionParameter.MoveBack.ToString(), false);
                return;
            }

            if (control.MoveForward)
            {
                if (!CheckFront(control))
                {
                    control.PlayerMoveForward(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
                }
            }

            //CheckTurn(control);
        }

        private void CheckTurn(CharacterControl control)
        {
            if (!LockDirection)
            {
                if (control.MoveForward)
                {
                    control.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }
            }
        }

        bool CheckFront(CharacterControl control)
        {
            foreach (GameObject o in control.FrontSpheres)
            {
                //Self = false;

                Debug.DrawRay(o.transform.position, control.transform.forward * 0.3f, Color.yellow);
                RaycastHit hit;
                if (Physics.Raycast(o.transform.position, control.transform.forward, out hit, BlockDistance))
                {
                    if (!control.RagdollParts.Contains(hit.collider) )
                    {
                        if(!IsBodyPart(hit.collider) 
                            && !Ledge.IsLedge(hit.collider.gameObject)
                            && !Ledge.IsLedgeChecker(hit.collider.gameObject))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        bool IsBodyPart(Collider col)
        {
            CharacterControl control = col.transform.root.GetComponent<CharacterControl>();

            if (control == null)
            {
                return false;
            }

            if (control.gameObject == col.gameObject)
            {
                return false;
            }

            if (control.RagdollParts.Contains(col))
            { 
                return true;
            }

            return false;
        }
    }
}
