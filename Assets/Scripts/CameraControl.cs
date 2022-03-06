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
        public float _MouseSensitivity = 30f;

        public float _Speed = 50f;

        private bool _Initialized = false;

        private float _rotationY;
        private float _rotationX;

        private DateTime? _NoMouseMovementStartTime;

        private InputAction moveAction;
        public void Initialize(InputAction moveAction)
        {
            //record the movement action and playerRespawner to be used in fixed update
            this.moveAction = moveAction;
        }

        /*
         * Control the camera to follow the user or rotate the maze.
         * If it detects that there is no mouse movement for an entire 1 second,
         * let the camera look at the player again.
         */
        void Update()
        {
            // When the game first starts, make the camera look at the player. 
            if(!_Initialized)
            {
                FollowTarget();
                _Initialized = true;

                return;
            }

            // Check if there is any mouse movement.
            Vector3 delta = Mouse.current.delta.ReadValue();
            if (delta.magnitude > 0.0)
            {
                _NoMouseMovementStartTime = null;
                //RotateToMousePositionOld();
                RotateToMousePosition();
                return;
            }
            // If it gets here, that means that the mouse is not moving
            if (!_NoMouseMovementStartTime.HasValue)
                _NoMouseMovementStartTime = DateTime.Now;


            if (_NoMouseMovementStartTime.HasValue)
            {
                TimeSpan tp = DateTime.Now - _NoMouseMovementStartTime.Value;
                /*
                * If the mouse has been not moving for an entire second, 
                * make the camera follow the player again.
                */
                if (tp.TotalMilliseconds > 1000)
                {
                    FollowTarget();
                    _NoMouseMovementStartTime = null;
                }
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
            float mouseX = UnityEngine.Input.GetAxis("Mouse X")* _MouseSensitivity;
            float mouseY = UnityEngine.Input.GetAxis("Mouse Y") * _MouseSensitivity;

            _rotationX += mouseY;
            _rotationY += mouseX;

            transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
        }

        private void RotateToMousePosition()
        {
            float mouseX = UnityEngine.Input.GetAxis("Mouse X") * _MouseSensitivity;
            float mouseY = UnityEngine.Input.GetAxis("Mouse Y") * _MouseSensitivity;

            _rotationX += mouseY;
            _rotationY += mouseX;

            Vector3 endpoint = new Vector3(_rotationY,_rotationX, 0.0f);
            Quaternion rot = Quaternion.LookRotation(endpoint);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _Speed * Time.deltaTime);
        }
    }
}
