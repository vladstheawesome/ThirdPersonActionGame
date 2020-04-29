using System;
using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.Core;
using ThirdPersonGame.States;
using UnityEngine;
using UnityEngine.AI;

namespace ThirdPersonGame.Control
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] Transform target;
        [SerializeField] float maxSpeed = 6f;
        //[SerializeField] float maxNavPathLength = 40f;

        NavMeshAgent navMeshAgent;

        CharacterControl control;

        // Start is called before the first frame update
        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            control = this.GetComponent<CharacterControl>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;            
        }

        private void UpdateAnimator()
        {
            var isIdle = GetComponent<AIController>().GetWayPointIdle();
            if (!isIdle)
            {
                control.SkinnedMeshAnimator.SetBool(TransitionParameter.Move.ToString(), true);
            }

            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
            control.SkinnedMeshAnimator.SetBool(TransitionParameter.Move.ToString(), false);
        }
    }
}
