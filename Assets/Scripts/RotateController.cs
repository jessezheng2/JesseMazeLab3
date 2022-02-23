using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ZhengJesse.Lab2
{
    public class RotateController : MonoBehaviour
    {
        [SerializeField]
        public GameObject MazeToRotate;

        [SerializeField]
        public Vector3 AnglesToRotate;

        // A flag to indicate if it should rotate the maze about the Y axis.
        private bool ToRotate = false;

        // Update is called once per frame
        void Update()
        {
            //Rotate the maze if necessary

            if(ToRotate)
                MazeToRotate.transform.Rotate(AnglesToRotate * Time.deltaTime);
        }
        public void Initialize(InputAction rotateAction)
        {
            rotateAction.performed += RotateAction_performed;
        }

        private void RotateAction_performed(InputAction.CallbackContext obj)
        {
            //Toogle to rotate or stop rotating the maze when "0" key is pressed.
            ToRotate = !ToRotate;
        }
    }
}
