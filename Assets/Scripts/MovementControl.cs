using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ZhengJesse.Lab3
{
    public class MovementControl : MonoBehaviour
    {
        [SerializeField] private float _Speed = 10f;

        [SerializeField]
        private GameObject _PlayerToMove;

        private InputAction _MoveAction;
        private InputAction _LeftShiftAction;
        private InputAction _RightShiftAction;

        public void Initialize(InputAction moveAction, InputAction leftShift, InputAction rightShift)
        {
            //record the movement action and playerRespawner to be used in fixed update
            this._MoveAction = moveAction;
            this._LeftShiftAction = leftShift;
            this._RightShiftAction = rightShift;
        }
        /*
         * Move the player according to the player's input. 
         * If the player presses the shift key while trying to navigate the maze, it will move the player
         * at 50% of the normal speed.
         */
        private void FixedUpdate()
        {
            //Move the player to the new position
            Vector2 moveInput = _MoveAction.ReadValue<Vector2>();
            if (moveInput.magnitude == 0)
                return;
            
            //If shift key is not pressed, the player will move at a normal speed that was configured for the game.

            float speedFactor = 1;
            float fl = _LeftShiftAction.ReadValue<float>();
            float fr = _RightShiftAction.ReadValue<float>();
            if (fl > 0.1f || fr > 0.1f)
            {
                //If the shift key is pressed, the player will move at half the normal speed.
                speedFactor = 0.5f;
            }


            float speed = _Speed * speedFactor;
            Vector3 delta = new Vector3(moveInput.x, 0, moveInput.y) * speed * Time.deltaTime;
            Vector3 target = _PlayerToMove.transform.position + delta;
            _PlayerToMove.transform.position = Vector3.MoveTowards(_PlayerToMove.transform.position, target, speed * Time.deltaTime);
        }
    }
}
