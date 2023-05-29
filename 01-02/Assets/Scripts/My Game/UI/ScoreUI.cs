using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MyGame
{
    /// <summary>
    /// ScoreUI
    /// </summary>
    public class ScoreUI : MonoBehaviour
    {
        [HideInInspector]
        public int Score; //得分（存活时间）
        [HideInInspector]
        public float Timer; //计时器

        private TextMeshProUGUI Text;
        [Tooltip("获取当前生命值")]
        public HouseCollider House;

        private void Start()
        {
            Text = GetComponent<TextMeshProUGUI>();
            Score = 0;
            Timer = 0;
        }

        private void Update()
        {
            if (!House.IsGameOver)
            {
                Timer += Time.deltaTime;
                Score = (int)Timer;
                Text.text = "<color=black>Score: " + Score + "</color>";
            }
        }


    }
}
