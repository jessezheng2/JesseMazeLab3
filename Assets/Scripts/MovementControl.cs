using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ZhengJesse.Lab3
{
    public class MovementControl : MonoBehaviour
    {
        [SerializeField] private float speed = 0f;

        [SerializeField]
        private GameObject playerToMove;

        private InputAction moveAction;

        private Vector3 _MazeExit;
        private float _MazeCubeWidth = 3;


        public void Initialize(InputAction moveAction, Vector3 mazeExit)
        {
            //record the movement action and playerRespawner to be used in fixed update
            this.moveAction = moveAction;
            this._MazeExit = mazeExit;

        }

        private void FixedUpdate()
        {
            //Move the player to the new position
            Vector2 moveInput = moveAction.ReadValue<Vector2>();
            if (moveInput.magnitude == 0)
                return;

            Vector3 moveDirection = Vector3.forward * moveInput.y + Vector3.right * moveInput.x;

            Vector3 target = playerToMove.transform.position + moveDirection;
            float step = speed * Time.deltaTime;
            playerToMove.transform.position = Vector3.MoveTowards(playerToMove.transform.position, target, step);


            Vector3 v = playerToMove.transform.position;

            float f = _MazeCubeWidth / 2;

            if (_MazeExit.x- f <=v.x && v.x<=_MazeExit.x+f && _MazeExit.y-f<=v.y && _MazeExit.y<=_MazeExit.y+f)
                UnityEngine.Debug.Log($"Win");



            //playerToMove.transform.position += moveDirection * speed * Time.deltaTime;
        }
    }
}
