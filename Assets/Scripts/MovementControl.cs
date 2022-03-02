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

        private float _SpeedFactor = 1;



        public void Initialize(InputAction moveAction,InputAction movementSpeedAction)
        {
            //record the movement action and playerRespawner to be used in fixed update
            this._MoveAction = moveAction;

            movementSpeedAction.performed += MovementSpeedAction_performed;
        }

        private void MovementSpeedAction_performed(InputAction.CallbackContext obj)
        {
            //When the Shift key is pressed, reduce the speed by 1/2. 
            //When the Shift key is pressed again, restore the original speed.

            if (_SpeedFactor == 1)
                _SpeedFactor = 0.5f;
            else
                _SpeedFactor = 1;
        }

        private void FixedUpdate()
        {
            //Move the player to the new position
            Vector2 moveInput = _MoveAction.ReadValue<Vector2>();
            if (moveInput.magnitude == 0)
                return;

            float speed = _Speed * _SpeedFactor;
            Vector3 delta = new Vector3(moveInput.x, 0, moveInput.y) * speed * Time.deltaTime;
            Vector3 target = _PlayerToMove.transform.position + delta;
            _PlayerToMove.transform.position = Vector3.MoveTowards(_PlayerToMove.transform.position, target, speed * Time.deltaTime);
        }
    }
}
