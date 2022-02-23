using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ZhengJesse.Lab2
{
    public class NextCameraHandler
    {
        private InputAction cameraSwitch;
        private CameraSwitcher cameraSwitcher;

        /*
         a camera switch handle.
         */
        public NextCameraHandler(InputAction cameraSwitch, CameraSwitcher cameraSwitcher)
        {
            //Keep references of CameraSwitch InputAction and CameraSwitcher to be used 
            //in CameraSwitch event handler
            this.cameraSwitch = cameraSwitch;
            this.cameraSwitcher = cameraSwitcher;
            cameraSwitch.performed += CameraSwitch_performed;
        }
        private void CameraSwitch_performed(InputAction.CallbackContext obj)
        {
            //Switch to the next camera.
            cameraSwitcher.NextCamera();
        }
    }
}
