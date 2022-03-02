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
        private Transform _Character;

        [SerializeField] private Vector3 _CameraCharacterOffset;

        [SerializeField]
        public float _mouseSensitivity = 30f;



        public float _speed = 50f;

        private bool _Initialized = false;

        private float _rotationY;
        private float _rotationX;


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


            if(!_Initialized)
            {
                FollowTarget();
                _Initialized = true;

                return;
            }

            if (delta.magnitude == 0.0)
            {
                if (!noMouseMovementStartTime.HasValue)
                    noMouseMovementStartTime = DateTime.Now;
            }
            else
            {
                noMouseMovementStartTime = null;
            }
            if (noMouseMovementStartTime.HasValue)
            {
                TimeSpan tp = DateTime.Now - noMouseMovementStartTime.Value;

                if (tp.TotalMilliseconds > 1000)
                {
                    FollowTarget();
                    noMouseMovementStartTime = null;
                    return;
                }
            }
            else
            {
                //RotateToMousePositionOld();
                RotateToMousePosition();
            }
        }
        private void FollowTarget()
        {
            if (_Character == null)
                return;
            Vector3 desiredPos = _Character.position + _CameraCharacterOffset;
            
            Vector3 delta = transform.position - desiredPos;

            //if (delta.magnitude > smoothSpeed)
            {
                transform.position = desiredPos;
                transform.LookAt(_Character);
            }
        }

        private void RotateToMousePositionOld()
        {
            float mouseX = UnityEngine.Input.GetAxis("Mouse X")* _mouseSensitivity;
            float mouseY = UnityEngine.Input.GetAxis("Mouse Y") * _mouseSensitivity;

            _rotationX += mouseY;
            _rotationY += mouseX;

            transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
        }

        private void RotateToMousePosition()
        {
            float mouseX = UnityEngine.Input.GetAxis("Mouse X") * _mouseSensitivity;
            float mouseY = UnityEngine.Input.GetAxis("Mouse Y") * _mouseSensitivity;

            _rotationX += mouseY;
            _rotationY += mouseX;

            Vector3 endpoint = new Vector3(_rotationY,_rotationX, 0.0f);
            Quaternion rot = Quaternion.LookRotation(endpoint);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _speed * Time.deltaTime);
        }
    }
}
