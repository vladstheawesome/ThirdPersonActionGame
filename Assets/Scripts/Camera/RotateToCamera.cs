using System;
using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.Control;
using UnityEngine;

namespace ThirdPersonGame.GameCamera
{
    public class RotateToCamera : MonoBehaviour
    {
        public Transform Player;
        public float angularSpeed;

        [SerializeField] [HideInInspector]
        private Vector3 initialOffset;
        private Vector3 currentOffset;

        [ContextMenu("Set Current Offset")]
        private void SetCurrentOffset()
        {
            if (Player == null)
            {
                return;
            }

            initialOffset = transform.position - Player.position;
        }

        // Start is called before the first frame update
        void Start()
        {
            Intialise();
            currentOffset = initialOffset;
        }


        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = Player.position + currentOffset;
            float movement = Input.GetAxis("Horizontal") * angularSpeed * Time.deltaTime;

            if (!Mathf.Approximately(movement, 0f))
            //if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            {
                transform.RotateAround(Player.position, Vector3.up, movement);
                currentOffset = transform.position - Player.position;
                Player.rotation = transform.rotation;
                transform.rotation = Player.rotation;
            }
        }

        private void Intialise()
        {
            if (!Player)
            {
                Debug.LogError("Please make sure to assign a player target!");
                Debug.Break();
            }
        }
    }
}
