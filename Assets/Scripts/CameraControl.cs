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


        public float smoothSpeed = 0.125f;

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

            UnityEngine.Debug.Log($"delta.magnitude: {delta.magnitude}");

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
                RotateToMousePositionOld();

        }
        private void FollowTarget()
        {
            if (_Character == null)
                return;
            Vector3 desiredPos = _Character.position + _CameraCharacterOffset;
            transform.position = desiredPos;
            transform.LookAt(_Character);
        }
        
        private void RotateToMousePosition()
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            Vector3 Worldpos = Camera.allCameras[0].ScreenToWorldPoint(mousePos);

            UnityEngine.Debug.Log($"new system: {mousePos.x}. {Worldpos.x}");

            var step = _speed * Time.deltaTime;

            Quaternion rot = Quaternion.Euler(Worldpos.x, Worldpos.y, Worldpos.y);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, step);
        }
        
        private void RotateToMousePositionOld()
        {
            float mouseX = UnityEngine.Input.GetAxis("Mouse X")* _mouseSensitivity;
            float mouseY = UnityEngine.Input.GetAxis("Mouse Y") * _mouseSensitivity;

            Vector3 mousePos = Mouse.current.position.ReadValue();
            Vector3 worldpos = Camera.allCameras[0].ScreenToWorldPoint(mousePos);


            _rotationX += mouseY;
            _rotationY += mouseX;

            transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
        }

        /*
        private void RotateToMousePosition()
        {
            //return;
            Vector3 mousePos = Mouse.current.position.ReadValue();
            Vector3 Worldpos = Camera.allCameras[0].ScreenToWorldPoint(mousePos);
            var step = speed * Time.deltaTime;

            Quaternion rot = Quaternion.LookRotation(Vector3.forward, Worldpos - transform.position);
            rot = Quaternion.Euler(rot.x,rot.y, -rot.z-45);
            UnityEngine.Debug.Log(rot);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, step);



            //Quaternion rot = Quaternion.Euler(mousePos.x, mousePos.y, mousePos.y);

            //transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, step);
        }
        */
    }
}
