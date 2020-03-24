using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.Control;
using UnityEngine;

namespace ThirdPersonGame.PooledObjects
{
    public enum GeneralBodyPart
    {
        Upper,
        Lower,
        Arm,
        Leg,
    }

    public class TriggerDetector : MonoBehaviour
    {
        public GeneralBodyPart generalBodyPart;

        public List<Collider> CollidingParts = new List<Collider>();
        private CharacterControl owner;

        private void Awake()
        {
            owner = this.GetComponentInParent<CharacterControl>();
        }

        private void OnTriggerEnter(Collider col)
        {
            // Detect punching

            // Collider is part of the player
            if (owner.RagdollParts.Contains(col))
            {
                return;
            }

            CharacterControl attacker = col.transform.root.GetComponent<CharacterControl>();

            // Character control not attached to root 
            // (its not a player, its just a physical object)
            if (attacker == null)
            {
                return;
            }

            // if its same game object as character control
            if (col.gameObject == attacker.gameObject)
            {
                return;
            }

            // all above test passed -> its a body part from another player
            if (!CollidingParts.Contains(col))
            {
                CollidingParts.Add(col);
            }
        }

        private void OnTriggerExit(Collider attacker)
        {
            // The collider is no longer touching our player
            if (CollidingParts.Contains(attacker))
            {
                CollidingParts.Remove(attacker);
            }
        }
    }
}
