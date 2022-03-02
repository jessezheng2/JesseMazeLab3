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
            if(other.gameObject.tag=="Samy")
            {
                //_TextToDisplay.SetActive(true);
                //StartCoroutine("WaitForSec");
                OnExitingMaze(this, new EventArgs());
            }
        }

        IEnumerator WaitForSec()
        {
            yield return new WaitForSeconds(5);
            //Destroy(_TextToDisplay);
            Destroy(gameObject);
        }

    }
}
