using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.PooledObjects;
using ThirdPersonGame.Control;
using ThirdPersonGame.Core;
using UnityEngine;

namespace ThirdPersonGame.States
{
    [CreateAssetMenu(fileName = "New State", menuName = "ThirdPersonGame/AbilityData/Attack")]
    public class Attack : StateData
    {
        public float StartAttackTime;
        public float EndAttackTime;
        public List<string> ColliderNames = new List<string>(); // List of body parts the attack is using
        public bool MustCollide;
        public bool MustFaceAttacker;
        public float LethalRange;
        public int MaxHits;
        //public List<RuntimeAnimatorController> DeathAnimators = new List<RuntimeAnimatorController>();

        private List<AttackInfo> FinishedAttacks = new List<AttackInfo>();

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameter.Attack.ToString(), false);

            // Instantiate AttackInfo Prefab
            // Prefab needs to be in a folder called RESOURCES
            // GameObject obj = Instantiate(Resources.Load("AttackInfo", typeof(GameObject))) as GameObject;
            GameObject obj = PoolManager.Instance.GetObject(PoolObjectType.ATTACKINFO); 
            AttackInfo info = obj.GetComponent<AttackInfo>();

            obj.SetActive(true);
            info.ResetInfo(this, characterState.GetCharacterControl(animator)); // Clean slate before an attack is registered 

            // if info is not already added in the AttackManager
            // we need to add it to the current attacks
            if (!AttackManager.Instance.CurrentAttacks.Contains(info))
            {
                AttackManager.Instance.CurrentAttacks.Add(info);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            RegisterAttack(characterState, animator, stateInfo);
            DeregisterAttack(characterState, animator, stateInfo);
        }

        // As we update the ability, we register the attack
        public void RegisterAttack(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            // Attack should be registered after the StartTime but before EndTime
            if(StartAttackTime <= stateInfo.normalizedTime && EndAttackTime > stateInfo.normalizedTime)
            {
                foreach(AttackInfo info in AttackManager.Instance.CurrentAttacks)
                {
                    if (info == null)
                    {
                        continue;
                    }

                    if (!info.isRegistered && info.AttackAbility == this)
                    {
                        info.Register(this);
                    }
                }
            }
        }

        public void DeregisterAttack(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= EndAttackTime)
            {
                foreach(AttackInfo info in AttackManager.Instance.CurrentAttacks)
                {
                    if (info == null)
                    {
                        continue;
                    }

                    if (info.AttackAbility == this && !info.isFinished)
                    {
                        info.isFinished = true;
                        //Destroy(info.gameObject);
                        info.GetComponent<PoolObject>().TurnOff();
                    }
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            ClearAttack();
        }

        public void ClearAttack()
        {
            FinishedAttacks.Clear();

            foreach(AttackInfo info in AttackManager.Instance.CurrentAttacks)
            {
                if (info == null || info.isFinished)
                {
                    FinishedAttacks.Add(info);
                }
            }

            foreach(AttackInfo info in FinishedAttacks)
            {
                if(AttackManager.Instance.CurrentAttacks.Contains(info))
                {
                    AttackManager.Instance.CurrentAttacks.Remove(info);
                }
            }
        }

        /*public RuntimeAnimatorController GetDeathAnimator()
        {
            int index = Random.Range(0, DeathAnimators.Count);
            return DeathAnimators[index];
        }*/
    }
}
