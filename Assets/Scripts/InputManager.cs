using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZhengJesse.Input;

namespace ZhengJesse.Lab3
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private CameraSwitcher cameraSwitcher;
        [SerializeField] private RotateController rotationController;

        private Lab2Input inputScheme;
        private QuitHandler quitHandler;
        
        private void Awake()
        {
            //Intantiate a Lab2Input scheme.
            inputScheme = new Lab2Input();
            cameraSwitcher.Initialize();
        }
        private void OnEnable()
        {
            //Set up NextCameraHandler
            var nextCameraHandler = new NextCameraHandler(inputScheme.Player.CameraSwitch, this.cameraSwitcher);

            //Set up QuitHandler
            quitHandler = new QuitHandler(inputScheme.Player.Quit);

            //Initialize RotationController
            rotationController.Initialize(inputScheme.Player.RotateMaze);

            //Enable Quit, CameraSwith, and RotateMaze inputs.
            inputScheme.Player.Quit.Enable();
            inputScheme.Player.CameraSwitch.Enable();
            inputScheme.Player.RotateMaze.Enable();

        }
    }
}
