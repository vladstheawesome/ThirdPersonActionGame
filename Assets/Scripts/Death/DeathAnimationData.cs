using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.PooledObjects;
using UnityEngine;

namespace ThirdPersonGame.Death
{
    [CreateAssetMenu(fileName = "New ScriptableObject", menuName = "ThirdPersonGame/Death/DeathAnimationData")]
    public class DeathAnimationData : ScriptableObject
    {
        // We want to be able to specify which body part   
        // each of the death animations is associated with
        public List<GeneralBodyPart> GeneralBodyParts = new List<GeneralBodyPart>();

        // The animation of the death
        public RuntimeAnimatorController Animator;

        public bool IsFacingAttacker;
    }
}
