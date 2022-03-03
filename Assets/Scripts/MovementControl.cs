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

        private float _SpeedFactor = 1f;


        public void Initialize(InputAction moveAction)
        {
            //record the movement action and playerRespawner to be used in fixed update
            this._MoveAction = moveAction;
        }
        private void FixedUpdate()
        {
            //Move the player to the new position
            Vector2 moveInput = _MoveAction.ReadValue<Vector2>();
            if (moveInput.magnitude == 0)
                return;

            _SpeedFactor = 1;
            if (UnityEngine.Input.GetKey(KeyCode.LeftShift) || UnityEngine.Input.GetKey(KeyCode.RightShift))
            {
                _SpeedFactor = 0.5f;
            }


            float speed = _Speed * _SpeedFactor;
            Vector3 delta = new Vector3(moveInput.x, 0, moveInput.y) * speed * Time.deltaTime;
            Vector3 target = _PlayerToMove.transform.position + delta;
            _PlayerToMove.transform.position = Vector3.MoveTowards(_PlayerToMove.transform.position, target, speed * Time.deltaTime);
        }
    }
}
