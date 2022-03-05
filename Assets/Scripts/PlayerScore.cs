using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZhengJesse.Lab3
{
    public class PlayerScore : MonoBehaviour
    {
        private static int _Score = 0;
        private GUIStyle _TextStyle = new GUIStyle();

        public static void Initialize()
        {
            _Score = 0;
        }
        public static void AddScore()
        {
            _Score++;
        }
        private void OnGUI()
        {
            _TextStyle.fontSize = 20;
            _TextStyle.normal.textColor = Color.red;


            GUI.contentColor = Color.green;
            string scoreMsg = $"# of Coins Collected: {_Score}";
            GUI.Label(new Rect(20, 20, 100, 20), scoreMsg, _TextStyle);
        }
        public static int GetPlayScore()
        {
            return _Score;
        }
    }
}
