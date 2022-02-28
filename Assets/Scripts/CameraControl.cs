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


        Vector3 latMousePos;

        private InputAction moveAction;
        public void Initialize(InputAction moveAction)
        {
            //record the movement action and playerRespawner to be used in fixed update
            this.moveAction = moveAction;
        }

        // Start is called before the first frame update
        void Start()
        {
            latMousePos = Mouse.current.position.ReadValue();

        }

        // Update is called once per frame
        void Update()
        {
            Vector3 delta = Mouse.current.delta.ReadValue();

            UnityEngine.Debug.Log($"delta.magnitude: {delta.magnitude}");

            if (delta.magnitude == 0.0)
            {
                FollowTarget();
            }
            else
            {
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
