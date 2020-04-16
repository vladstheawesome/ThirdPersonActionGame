using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.PooledObjects;
using UnityEngine;

namespace ThirdPersonGame.Death
{
    public enum DeathType
    {
        NONE,
        LAUNCH_INTO_AIR,
        GROUND_SHOCK,
        SHOULDER_HIT,
        SABER_KNOCKOUT,
    }

    [CreateAssetMenu(fileName = "New ScriptableObject", menuName = "ThirdPersonGame/Death/DeathAnimationData")]
    public class DeathAnimationData : ScriptableObject
    {
        // We want to be able to specify which body part   
        // each of the death animations is associated with
        public List<GeneralBodyPart> GeneralBodyParts = new List<GeneralBodyPart>();
        
        public RuntimeAnimatorController Animator; // The animation of the death
        //public bool LaunchIntoAir;
        public DeathType deathType;
        public bool IsFacingAttacker;
    }
}
