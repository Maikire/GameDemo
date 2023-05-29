using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyGame
{
    /// <summary>
    /// SkillUI_Timer
    /// </summary>
    public class SkillUI_Timer : MonoBehaviour
    {
        [Tooltip("技能信息")]
        public Skill SkillInformation;
        [Tooltip("技能ID")]
        public int SkillID;

        private float CoolingTime; //冷却时间
        private float CurrentCoolingTime; //当前冷却时间

        private Image ChangeColor;

        private void Start()
        {
            ChangeColor = this.transform.GetChild(1).GetComponent<Image>();
        }

        private void Update()
        {
            switch (SkillID)
            {
                case 0:
                    CoolingTime = SkillInformation.CoolingTime_FireSpeedUp;
                    CurrentCoolingTime = SkillInformation.CurrentCoolingTime_FireSpeedUp;
                    break;

                case 1:
                    CoolingTime = SkillInformation.CoolingTime_MoveSpeedDown;
                    CurrentCoolingTime = SkillInformation.CurrentCoolingTime_MoveSpeedDown;
                    break;

                default:
                    break;
            }

            ChangeColor.fillAmount = (CoolingTime - CurrentCoolingTime) / CoolingTime;
        }


    }
}