using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZhengJesse.Lab2
{
    public class CameraSwitcher : MonoBehaviour
    {
        [SerializeField] private Camera[] cameras;
        [SerializeField] private Camera defaultCamera;
        private int index = 0;
        public void Initialize()
        {
            index = 0;
            // Loop through each camera and disable it.
            // Enable the default camera
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].gameObject.SetActive(false);
                //Set the index of the default camera
                if (cameras[i] == defaultCamera)
                    index = i;
            }
            //enable defaultCamera;
            EnableCamera();
        }

        /*
         * Switch to the next camera and enable it and also disable all other camera.
         */
        public void NextCamera()
        {
            index++;
            if (index >= cameras.Length)
                index = 0;

            //Enable the chosen camera
            EnableCamera();

            //Disable all other camera
            for (int i = 0; i < cameras.Length; i++)
            {
                if (i != index)
                    cameras[i].gameObject.SetActive(false);
            }
        }
        /*
         * Called to enable the chosen camera and set its target to the current active character.
         */
        private void EnableCamera()
        {
            Camera camera = cameras[index];
            FollowWithOffset followWithOffset = camera.GetComponent<FollowWithOffset>();
            camera.gameObject.SetActive(true);
        }
    }

}
