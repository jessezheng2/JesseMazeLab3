using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZhengJesse.Lab3
{
    public class LevelManager : MonoBehaviour
    {
        private void Awake()
        {
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
                    txt.text = msg;
                }
            }
        }
        public void StartGame()
        {
            SceneManager.LoadScene("MazeScene");
        }
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
