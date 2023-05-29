using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyGame
{
    /// <summary>
    /// SkillUI_Times
    /// </summary>
    public class SkillUI_Times : MonoBehaviour
    {
        [Tooltip("技能信息")]
        public Skill SkillInformation;
        private int CurrentAvailableTimes;

        private GameObject ChangeColor;
        private TextMeshProUGUI TextMeshPro;

        private void Start()
        {
            ChangeColor = this.transform.GetChild(1).gameObject;
            TextMeshPro = this.GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Update()
        {
            CurrentAvailableTimes = SkillInformation.CurrentAvailableTimes_Bomb;
            TextMeshPro.text = CurrentAvailableTimes.ToString();

            if (CurrentAvailableTimes <= 0)
            {
                ChangeColor.SetActive(true);
            }
            else
            {
                ChangeColor.SetActive(false);
            }
        }

    }
}