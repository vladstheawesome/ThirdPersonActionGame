using System;
using ThirdPersonGame.Control;
using ThirdPersonGame.Core;
using ThirdPersonGame.Interact;
using UnityEngine;

namespace ThirdPersonGame.PooledObjects
{
    public class DamageDetector : MonoBehaviour
    {
        CharacterControl control;
        GeneralBodyPart DamagedPart;

        public int DamageTaken;

        private void Awake()
        {
            DamageTaken = 0;
            control = GetComponent<CharacterControl>();
        }

        private void Update()
        {
            if (AttackManager.Instance.CurrentAttacks.Count > 0)
            {
                CheckAttack();
            }
        }

        private void CheckAttack()
        {
            foreach(AttackInfo info in AttackManager.Instance.CurrentAttacks)
            {
                if (info == null)
                {
                    continue;
                }

                if (!info.isRegistered)
                {
                    continue;
                }

                if (info.isFinished)
                {
                    continue;
                }

                if (info.CurrentHits >= info.MaxHits)
                {
                    continue;
                }

                // If attacker is me (player), dont take damage
                if (info.Attacker == control)
                {                    
                    continue;
                }

                if (info.MustFaceAttacker)
                {
                    // vector going from attacker to the target
                    Vector3 vec = this.transform.position - info.Attacker.transform.position;

                    if (vec.z * info.Attacker.transform.forward.z < 0f) // forward direction < 0 => target is behind attacker
                    {
                        continue; // do not register attack
                    }
                }

                if (info.MustCollide)
                {
                    if(IsCollided(info))
                    {
                        TakeDamage(info);
                    }
                }
                else // AOE attack
                {
                    // distance between target and attacker
                    float dist = Vector3.SqrMagnitude(this.gameObject.transform.position - info.Attacker.transform.position);
                    // Debug.Log(this.gameObject.name + " dist: " + dist.ToString());
                    if (dist <= info.LethalRange)
                    {
                        TakeDamage(info);
                    }
                }
            }
        }

        private bool IsCollided(AttackInfo info)
        {
            foreach(TriggerDetector trigger in control.GetAllTriggers())
            {
                foreach (Collider collider in trigger.CollidingParts)
                {
                    foreach (string name in info.ColliderNames)
                    {
                        if (name.Equals(collider.gameObject.name))
                        {
                            if (collider.transform.root.gameObject == info.Attacker.gameObject)
                            {                           
                                DamagedPart = trigger.generalBodyPart;
                                return true;
                            }
                        }
                    }
                }
            }            

            return false;
        }

        private void TakeDamage(AttackInfo info)
        {
            if (DamageTaken > 0)
            {
                return;
            }

            Debug.Log(info.Attacker.gameObject.name + " hits: " + this.gameObject.name);
            Debug.Log(this.gameObject.name + " hit in " + DamagedPart.ToString());

            //control.SkinnedMeshAnimator.runtimeAnimatorController = info.AttackAbility.GetDeathAnimator();

            // Pass the damaged part of the body (to determine which death animation to play)
            control.SkinnedMeshAnimator.runtimeAnimatorController = DeathAnimationManager.Instance.GetAnimator(DamagedPart, info);
            info.CurrentHits++;

            // turn off box collider of dead enemy character
            control.GetComponent<BoxCollider>().enabled = false;
            control.ledgeChecker.GetComponent<BoxCollider>().enabled = false;
            control.RIGID_BODY.useGravity = false;           

            DamageTaken++;
        }
    }
}
