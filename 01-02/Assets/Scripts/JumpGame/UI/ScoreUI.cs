using TMPro;
using UnityEngine;

namespace JumpGame
{
    /// <summary>
    /// ScoreUI
    /// </summary>
    public class ScoreUI : MonoBehaviour
    {
        public PlayerController Player;
        private TextMeshProUGUI Text;

        private void Start()
        {
            Text = this.GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            Text.text = "<color=black>Score: " + Player.Score + "</color>";
        }


    }
}