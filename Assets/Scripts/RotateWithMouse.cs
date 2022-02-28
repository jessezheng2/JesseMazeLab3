using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ZhengJesse.Lab3
{
    public class RotateWithMouse : MonoBehaviour
    {
        public float speed=5f;
        public float sensitivity = 10f;

        float xRotation = 0f;
        Vector2 lastPos;
        Vector3 latMousePos;

        private void Awake()
        {
            latMousePos = Mouse.current.position.ReadValue();

        }
        // Update is called once per frame
        void Update()
        {
            //float x = UnityEngine.Input.GetAxis("Mouse X") ;

            Vector3 delta = Mouse.current.delta.ReadValue();

            if (delta.magnitude == 0.0)
                return;

            Vector3 mousePos = Mouse.current.position.ReadValue();

            

            Vector3 Worldpos = Camera.allCameras[0].ScreenToWorldPoint(mousePos);

            UnityEngine.Debug.Log($"new system: {mousePos.x}. {Worldpos.x}");



            //transform.Rotate(Vector3.up * mousePos.x * sensitivity*Time.deltaTime);


            var step = speed * Time.deltaTime;

            Quaternion rot = Quaternion.Euler(mousePos.x, mousePos.y, mousePos.y);

            //rot=transform.rotation.a
            // Rotate our transform a step closer to the target's.
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, step);
            //transform.eulerAngles += new Vector3(0, step, 0);
            rot.z = transform.localRotation.z;
           transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, step);
        }
    }
}
