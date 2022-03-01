using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZhengJesse.Input;

namespace ZhengJesse.Lab3
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private MovementControl _MovementController;
        [SerializeField] private MazeRenderer _Maze;


        private Lab3Input inputScheme;
        private QuitHandler quitHandler;

        private void Awake()
        {
            //Intantiate a Lab3Input scheme.
            inputScheme = new Lab3Input();
            _MovementController.Initialize(inputScheme.Player.Move);
        }
        private void OnEnable()
        {
            //Set up NextCameraHandler
            //Set up QuitHandler
            quitHandler = new QuitHandler(inputScheme.Player.Quit);


            //Enable Quit, CameraSwith, and RotateMaze inputs.
            inputScheme.Player.Move.Enable();
            inputScheme.Player.Quit.Enable();
        }
    }
}
