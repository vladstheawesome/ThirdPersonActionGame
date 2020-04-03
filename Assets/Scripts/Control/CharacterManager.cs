using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.Core;
using UnityEngine;

namespace ThirdPersonGame.Control
{
    public class CharacterManager : Singleton<CharacterManager>
    {
        public List<CharacterControl> Characters = new List<CharacterControl>();

        // TODO: Get Player from Playable Characters

        // Get Player from Animator
        public CharacterControl GetCharacter(Animator animator)
        {
            foreach (CharacterControl control in Characters)
            {
                if (control.SkinnedMeshAnimator == animator)
                {
                    return control;
                }
            }

            return null;

            //CharacterControl control = new CharacterControl();

            //control.SkinnedMeshAnimator = animator;

            //return control;
        }
    }
}
