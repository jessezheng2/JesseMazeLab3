using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZhengJesse.Lab2
{
    public class FollowWithOffset : MonoBehaviour
    {
        [SerializeField]
        private Transform target;
        [SerializeField] private Vector3 offset;
        public float smoothSpeed = 0.125f;

        // Update is called once per frame
        void FixedUpdate()
        {
            if (target == null)
                return;
            Vector3 desiredPos = target.position + offset;

            transform.position = desiredPos;
            transform.LookAt(target);
        }
    }

}
