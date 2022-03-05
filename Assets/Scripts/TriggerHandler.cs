using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZhengJesse.Lab3
{
    public class TriggerHandler : MonoBehaviour
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
                AudioSource _Success = Camera.main.GetComponent<AudioSource>();
                _Success.Play();
                StartCoroutine("WaitForSoundToPlay");
            }

        }
        IEnumerator WaitForSoundToPlay()
        {
            yield return new WaitForSeconds(3f);
            OnExitingMaze(this, new EventArgs());
        }
    }
}
