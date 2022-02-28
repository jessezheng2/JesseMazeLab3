using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ZhengJesse.Lab3
{
    public class CameraControl : MonoBehaviour
    {
        [SerializeField]
        private Transform target;

        [SerializeField] private Vector3 offset;
        public float smoothSpeed = 0.125f;

        public float speed = 50f;
        public float sensitivity = 10f;

        private bool characterMoved = false;

        private int count = 0;

        private DateTime? noMouseMovementStartTime;

        private InputAction moveAction;
        public void Initialize(InputAction moveAction)
        {
            //record the movement action and playerRespawner to be used in fixed update
            this.moveAction = moveAction;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 delta = Mouse.current.delta.ReadValue();

            UnityEngine.Debug.Log($"delta.magnitude: {delta.magnitude}");

            if (delta.magnitude == 0.0)
            {
                if(!noMouseMovementStartTime.HasValue)
                    noMouseMovementStartTime = DateTime.Now;

                TimeSpan tp = DateTime.Now - noMouseMovementStartTime.Value;

                if(tp.TotalMilliseconds>200)
                    FollowTarget();
            }
            else
            {
                noMouseMovementStartTime = null;
                RotateToMousePosition();
            }

        }
        private void FollowTarget()
        {
            if (target == null)
                return;
            Vector3 desiredPos = target.position + offset;
            transform.position = desiredPos;
            transform.LookAt(target);
        }
        private void RotateToMousePosition()
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            Vector3 Worldpos = Camera.allCameras[0].ScreenToWorldPoint(mousePos);

            UnityEngine.Debug.Log($"new system: {mousePos.x}. {Worldpos.x}");

            var step = speed * Time.deltaTime;

            Quaternion rot = Quaternion.Euler(mousePos.x, mousePos.y, mousePos.y);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, step);
        }
    }
}
