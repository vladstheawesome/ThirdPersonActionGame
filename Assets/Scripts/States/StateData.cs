using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonGame.States
{
    // Base class for all our predefined actions
    // Every script that inherits from StateData, has to follow this format
    public abstract class StateData : ScriptableObject
    {
        public abstract void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo);
        public abstract void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo);
        public abstract void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo);
    }
}
