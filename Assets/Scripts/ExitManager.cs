using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZhengJesse.Lab3
{
    /* To handle the event when the player reaches the exit
    */
    public class ExitManager : MonoBehaviour
    {
        public event EventHandler OnExitingMaze;


        void OnTriggerEnter(Collider other)
        {
            /*The player has reached the exit. Play "Yay..." sound and waits for
             * 2 seconds to complete the sound playing and then 
             * notify InputManger that the game is over.
             * InputManager will load GameOver scene 
             */
            if (other.gameObject.tag == "Samy")
            {
                AudioSource success = Camera.main.GetComponent<AudioSource>();
                success.Play();
                StartCoroutine("WaitForSoundToPlay");
            }

        }
        IEnumerator WaitForSoundToPlay()
        {
            yield return new WaitForSeconds(2f);
            OnExitingMaze(this, new EventArgs());
        }
    }
}
