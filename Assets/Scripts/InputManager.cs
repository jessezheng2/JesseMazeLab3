using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZhengJesse.Input;
using UnityEngine.SceneManagement;

namespace ZhengJesse.Lab3
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private MovementControl _MovementController;
        [SerializeField] private MazeRenderer _Maze;
        [SerializeField] private TriggerHandler _TriggerHandler;


        private Lab3Input inputScheme;
        private QuitHandler quitHandler;

        private void Awake()
        {
            //Intantiate a Lab3Input scheme.
            inputScheme = new Lab3Input();
        }
        private void Start()
        {
             PlayerScore.Initialize();
            _Maze.BuildMaze();
            _MovementController.Initialize(inputScheme.Player.Move);
            _TriggerHandler.OnExitingMaze += _TriggerHandler_OnExitingMaze;
        }

        private void _TriggerHandler_OnExitingMaze(object sender, EventArgs e)
        {
            SceneManager.LoadScene("GameOver");
        }

        private void OnEnable()
        {
            quitHandler = new QuitHandler(inputScheme.Player.Quit);

            //Enable Quit, CameraSwith, and RotateMaze inputs.
            inputScheme.Player.Move.Enable();
            inputScheme.Player.Quit.Enable();
            //inputScheme.Player.MovementSpeed.Enable();
        }
    }
}
