using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZhengJesse.Lab3
{
    public class PlayerScore : MonoBehaviour
    {
        private static int _Score = 0;

        public static void Initialize()
        {
            _Score = 0;
        }
        public void AddScore()
        {
            _Score++;
        }
        private void OnGUI()
        {
            GUI.contentColor = Color.red;
            string scoreMsg = $"Score: {_Score}";
            GUI.Label(new Rect(20, 20, 100, 20), scoreMsg);
        }
        public static int GetPlayScore()
        {
            return _Score;
        }
    }
}
