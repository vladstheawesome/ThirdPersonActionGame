using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.Death;
using ThirdPersonGame.PooledObjects;
using UnityEngine;

namespace ThirdPersonGame.Core
{
    public class DeathAnimationManager : Singleton<DeathAnimationManager>
    {
        DeathAnimationLoader deathAnimationLoader;
        List<RuntimeAnimatorController> Candidates = new List<RuntimeAnimatorController>();

        void SetupDeathAnimationLoader()
        {
            if(deathAnimationLoader == null)
            {
                GameObject obj = Instantiate(Resources.Load("DeathAnimationLoader", typeof(GameObject)) as GameObject);
                DeathAnimationLoader loader = obj.GetComponent<DeathAnimationLoader>();

                deathAnimationLoader = loader;
            }
        }

        public RuntimeAnimatorController GetAnimator(GeneralBodyPart generalBodyPart, AttackInfo info)
        {
            // Based on a particular body part, we are going  
            // to return a specific death animation

            SetupDeathAnimationLoader();

            Candidates.Clear();

            foreach (DeathAnimationData data in deathAnimationLoader.DeathAnimationDataList)
            {
                if (info.LaunchIntoAir)
                {
                    if (data.LaunchIntoAir)
                    {
                        Candidates.Add(data.Animator);
                    }
                }
                else
                {
                    foreach (GeneralBodyPart part in data.GeneralBodyParts)
                    {
                        // if part matches
                        if (part == generalBodyPart)
                        {
                            // add the death animations for that body part
                            Candidates.Add(data.Animator);
                            break;
                        }
                    }
                }                
            }

            return Candidates[Random.Range(0, Candidates.Count)];
        }
    }
}
