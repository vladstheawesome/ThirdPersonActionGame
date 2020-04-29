using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonGame.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointDwellTime = 3f;
        [Range(0, 1)]
        [SerializeField] float patrolSpeedFraction = 0.2f;

        public float turnSpeed = 90f;

        Mover mover;
        CharacterControl control;

        LazyValue<Vector3> guardPosition;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        int currentWaypointIndex = 0;
        bool isIdle;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            control = this.GetComponent<CharacterControl>();
            guardPosition = new LazyValue<Vector3>(GetGuardPosition);
        }

        private Vector3 GetGuardPosition()
        {
            return transform.position;
        }

        void Start()
        {
            guardPosition.ForceInit();
        }

        void Update()
        {
            PatrolBehaviour();

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition.value;

            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();

                //StartCoroutine(TurnBeforeMoving());
                //yield return new WaitForSeconds(waypointDwellTime);
            }

            if (timeSinceArrivedAtWaypoint > waypointDwellTime)
            {
                mover.StartMoveAction(nextPosition, patrolSpeedFraction);
                //control.SkinnedMeshAnimator.SetBool(TransitionParameter.Move.ToString(), true);
                SetWayPointIdle(false);
            }
            else
            {
                //control.SkinnedMeshAnimator.SetBool(TransitionParameter.Move.ToString(), false);
                SetWayPointIdle(true);
            }
        }

        public bool SetWayPointIdle(bool v)
        {
            isIdle = v;
            return isIdle;
        }

        public bool GetWayPointIdle()
        {
            return isIdle;
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        //IEnumerator TurnBeforeMoving()
        //{

        //}

        IEnumerator TurnToFace(Vector3 lookTaget)
        {
            Vector3 dirToLookTarget = (lookTaget - transform.position).normalized;
            float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

            while(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle) > Mathf.Epsilon)
            {
                float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
                this.transform.eulerAngles = Vector3.up * angle;
                yield return null;
            }
        }
    }
}
