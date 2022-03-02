using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZhengJesse.Lab3
{
    public class LevelManager : MonoBehaviour
    {
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
