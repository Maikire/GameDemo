using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MyGame
{
    /// <summary>
    /// Restart
    /// </summary>
    public class Restart : MonoBehaviour
    {
        [Tooltip("GameOverUI")]
        public GameObject GameOverUI;
        [Tooltip("生成器")]
        public GenerateEnemy GenerateEnemy;
        [Tooltip("玩家发射子弹")]
        public PlayerFire PlayerFire;
        [Tooltip("玩家技能")]
        public Skill Skill;
        [Tooltip("血量")]
        public HouseCollider House;
        [Tooltip("得分")]
        public ScoreUI Score;

        private void Update()
        {
            //Restart
            if (Input.GetKeyDown(KeyCode.V))
            {
                GameOverUI.SetActive(false);

                GenerateEnemy.gameObject.SetActive(true);
                GenerateEnemy.TimeInterval = GenerateEnemy.TimeIntervalMax;
                GenerateEnemy.CurrentSpeed = 1;

                PlayerFire.FireSpeed = 1;
                PlayerFire.IsFire = true;

                Skill.CurrentCoolingTime_FireSpeedUp = 0;
                Skill.CurrentCoolingTime_MoveSpeedDown = 0;
                Skill.CurrentAvailableTimes_Bomb = Skill.AvailableTimes_Bomb;

                House.CurrentBlood = House.Blood;
                House.IsGameOver = false;

                Score.Score = 0;
                Score.Timer = 0;

                RecoveryAll();
            }

            //GameOver
            if (House.IsGameOver)
            {
                GameOverUI.SetActive(true);
                PlayerFire.IsFire = false;
                GenerateEnemy.gameObject.SetActive(false);
                RecoveryAll();
            }
        }

        /// <summary>
        /// 回收所有 敌人/子弹
        /// </summary>
        public static void RecoveryAll()
        {
            //回收所有敌人
            foreach (var item in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                EnemyPoolMnager.Intance.RecoveryEnemy(item.GetComponent<EnemyController>().ID, item);
            }

            //回收所有子弹
            foreach (var item in GameObject.FindGameObjectsWithTag("Bullet"))
            {
                BulletController temp = item.GetComponent<BulletController>();
                if (temp != null)
                {
                    BulletPoolMnager.Intance.RecoveryBullet(temp.ID, item);
                }
            }

            //回收所有炸弹箱
            foreach (var item in GameObject.FindGameObjectsWithTag("BombBox"))
            {
                BulletPoolMnager.Intance.RecoveryBullet(item.GetComponent<BombBoxController>().ID, item);
            }
        }



    }
}
