using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonGame.Control
{
    public class Guard : MonoBehaviour
    {
        public Light spotLight;
        public float viewDistance;
        public LayerMask viewMask;

        float viewAngle;
        Transform player;
        Color originalSpotLightColor;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            viewAngle = spotLight.spotAngle;
            originalSpotLightColor = spotLight.color;
        }

        void Update()
        {
            if(CanSeePlayer())
            {
                spotLight.color = Color.red;
            }
            else
            {
                spotLight.color = originalSpotLightColor;
            }
        }

        bool CanSeePlayer()
        {
            // Check 1: Distance between Guard and Player
            if (Vector3.Distance(this.transform.position, player.transform.position) < viewDistance)
            {
                // Check 2: Player is within view angle of Guard
                Vector3 dirToPlayer = (player.position - this.transform.position).normalized;
                float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
                if (angleBetweenGuardAndPlayer < viewAngle / 2f)
                {
                    // Check 3: Is player in line of sight of guard
                    if(!Physics.Linecast(transform.position, player.position, viewMask))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
        }
    }
}
