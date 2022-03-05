using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZhengJesse.Lab3
{
    /* To manage the coins in the maze
     * When player collide with a floating coin, the play earns one point
     */
    public class CoinManager : MonoBehaviour
    {
        void Update()
        {
            /*Rotate the coin*/
            transform.Rotate(90 * Time.deltaTime, 0, 0);
        }
        private void OnTriggerEnter(Collider other)
        {
            /*If the coin collides with Samy, the coin will move up and stay up 
             * for 0.5 seconds before being collected by the player. This is to simulate
               the bobbing animation. After the coin is collected by the player, it will
               plays a chime.
             */
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
