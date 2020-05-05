using System;
using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.Interact;
using UnityEditor;
using UnityEngine;

namespace ThirdPersonGame.Control
{
    public class Guard : MonoBehaviour
    {
        public Light spotLight;
        public float viewDistance;
        public LayerMask viewMask;
        public GameObject GuardVision;

        float viewAngle;
        Transform player;
        Color originalSpotLightColor;        
        Vector3 originPoint;
        Vector3 originPointUpdate;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            viewAngle = spotLight.spotAngle;
            originalSpotLightColor = spotLight.color;

            originPoint = GuardVision.transform.position;            
        }

        void Update()
        {
            originPointUpdate = GuardVision.transform.position;

            if (CanSeePlayer())
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
                    //if (!Physics.Linecast(transform.position, player.position, viewMask))
                    //{
                    //    return true;
                    //}    

                    var hasPatrolPath = GuardHasPatrolPath(this.transform);

                    if (hasPatrolPath == true)
                    {
                        originPoint = originPointUpdate;

                        if (Physics.Linecast(originPoint, player.position / 2, viewMask))
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (!Physics.Linecast(originPoint, player.position / 2 /*playerMidSection*/, viewMask)
                            || !Physics.Linecast(originPoint, player.position, viewMask)
                            || !Physics.Linecast(originPointUpdate, player.position, viewMask)
                            )
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool GuardHasPatrolPath(Transform transform)
        {
            var patrolPathExists = transform.GetComponent<PatrolPathController>();

            if (patrolPathExists.enabled == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
            Gizmos.color = Color.green;
            Gizmos.DrawRay(originPointUpdate, transform.forward * viewDistance);
        }
    }
}
