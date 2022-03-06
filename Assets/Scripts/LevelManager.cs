using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZhengJesse.Lab3
{
    /*
     * This class manages the scenes. It displays the GameOver scene when the player
     * reaches the exit.
     */
    public class LevelManager : MonoBehaviour
    {
        private void Awake()
        {
            /*Set up GameOver scene to display game playing result and some instructions.*/

            string name = gameObject.name;
            if(string.Compare(name,"GameOver",false)==0)
            {
                UnityEngine.UI.Text[] txts =gameObject.GetComponents<UnityEngine.UI.Text>();

                if(txts != null && txts.Length>0)
                {
                    UnityEngine.UI.Text txt = txts[0];
                    string msg;
                    string coins = "no coin";
                    if (PlayerScore.GetPlayScore() > 1)
                        coins = $"{PlayerScore.GetPlayScore()} coins";
                    else if(PlayerScore.GetPlayScore()==1)
                        coins = $"{PlayerScore.GetPlayScore()} coin";

                    msg = $@"Congratulations! You have sucessfully navigated the maze! You've collected {coins} along the way.";
                    msg = $"{msg} \r\nPush Play for another game or Quit to exit the game.";
                    msg = $"{msg} \r\n\r\nThe game will automatically start 10 seconds after you see this message!";
                    txt.text = msg;
                }

                StartCoroutine("AutoStartGameWithDelay");


            }
        }

        IEnumerator AutoStartGameWithDelay()
        {
            yield return new WaitForSeconds(10f);
            StartGame();
        }
        /*
         * Starts a new game.
         */
        public void StartGame()
        {
            SceneManager.LoadScene("MazeScene");
        }
        /*
         * Quit the game if the player wants to.
         */
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
