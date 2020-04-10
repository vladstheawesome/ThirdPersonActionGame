using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.Core;
using UnityEngine;

namespace ThirdPersonGame.PooledObjects
{
    public class PoolObject : MonoBehaviour
    {
        public PoolObjectType poolObjectType;
        public float ScheduledOffTime; // How long an attack will last for
        private Coroutine OffRoutine;

        private void OnEnable()
        {
            if (OffRoutine != null)
            {
                // Stop current routine, as theres a new owner
                StopCoroutine(OffRoutine);
            }

            if (ScheduledOffTime > 0f)
            {
                // Start routine for new owner
                OffRoutine = StartCoroutine(_ScheduledOff());
            }
        }

        public void TurnOff()
        {
            this.transform.parent = null;
            this.transform.position = Vector3.zero;
            this.transform.rotation = Quaternion.identity;
            PoolManager.Instance.AddObject(this);
        }

        IEnumerator _ScheduledOff()
        {
            yield return new WaitForSeconds(ScheduledOffTime);

            if (!PoolManager.Instance.PoolDictionary[poolObjectType].Contains(this.gameObject))
            {
                TurnOff();
            }
        }
    }
}
