using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZhengJesse.Lab3
{
    public class CoinManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
             transform.Rotate(90 * Time.deltaTime, 0, 0);
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag=="Samy")
            {
                Vector3 p = new Vector3(0, 3, 0);
                transform.position += p;
                StartCoroutine("WaitForSecUp",other);
            }
        }
        IEnumerator WaitForSecUp(Collider other)
        {
            yield return new WaitForSeconds(0.5f);
            Vector3 p = new Vector3(0, -3, 0);
            transform.position += p;
            StartCoroutine("WaitForSecDown", other);
        }
        IEnumerator WaitForSecDown(Collider other)
        {
            yield return new WaitForSeconds(0.05f);
            AudioSource chime = other.GetComponent<AudioSource>();
            chime.Play();
            PlayerScore.AddScore();
            Destroy(gameObject);
        }
    }
}
