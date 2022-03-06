using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZhengJesse.Input;
using UnityEngine.SceneManagement;

namespace ZhengJesse.Lab3
{
    /* 
     * This is the main class that controls the flow of the game.
     * First, it creates the maze.
     * Then, it responds to the player's input to move the player in the maze.
     * When the player reaches the exit of the maze, it displays a new scene
     * and provides options for the player to play another round or exit.
     */
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private MovementControl _MovementController;
        [SerializeField] private MazeRenderer _Maze;
        [SerializeField] private ExitManager _ExitManager;


        private Lab3Input inputScheme;
        private QuitHandler quitHandler;

        private void Awake()
        {
            //Instantiate a Lab3Input scheme.
            inputScheme = new Lab3Input();
        }
        private void Start()
        {
             PlayerScore.Initialize();
            _Maze.BuildMaze();
            _MovementController.Initialize(inputScheme.Player.Move);
            _ExitManager.OnExitingMaze += _TriggerHandler_OnExitingMaze;
        }

        private void _TriggerHandler_OnExitingMaze(object sender, EventArgs e)
        {
            SceneManager.LoadScene("GameOver");
        }

        private void OnEnable()
        {
            quitHandler = new QuitHandler(inputScheme.Player.Quit);

            //Enable move and quit inputs.
            inputScheme.Player.Move.Enable();
            inputScheme.Player.Quit.Enable();
        }
    }
}
