using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZhengJesse.Lab3
{
    public class ExitManager : MonoBehaviour
    {
        public event EventHandler OnExitingMaze;
        // Start is called before the first frame update
        void Start()
        {
        }


        void OnTriggerEnter(Collider other)
        {
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
