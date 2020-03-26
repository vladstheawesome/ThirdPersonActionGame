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
        
        public RuntimeAnimatorController Animator; // The animation of the death
        public bool LaunchIntoAir;
        public bool IsFacingAttacker;
    }
}
