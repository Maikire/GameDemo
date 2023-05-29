using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

namespace MyGame
{
    /// <summary>
    /// 触碰到房间后的各种行为
    /// </summary>
    public class HouseCollider : MonoBehaviour
    {
        [Tooltip("生命值")]
        public float Blood = 10;
        [HideInInspector]
        public float CurrentBlood; //当前生命值
        private float PreviousBlood; //上一帧的生命值
        private Transform BloodUI; //血条
        private Vector3 ChangeScale; //血条
        private Vector3 OriginalScale; //初始的血条

        private SpriteRenderer ChangeColor; //受击特效（改变颜色）
        private float Timer; // 改变颜色 计时器

        [HideInInspector]
        public bool IsGameOver = false; //true: GameOver

        private void Start()
        {
            CurrentBlood = Blood;
            PreviousBlood = Blood;
            BloodUI = this.transform.GetChild(0);
            ChangeScale = BloodUI.localScale;
            OriginalScale = BloodUI.localScale;
            ChangeColor = this.GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            HitEffects();
            ChangeBloodUI();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            CurrentBlood -= collision.GetComponent<EnemyController>().Damage;
            if (CurrentBlood <= 0)
            {
                CurrentBlood = 0;
                IsGameOver = true;
            }

            //受击特效
            BulletPoolMnager.Intance.GetBullet(1, collision.transform.position);
            ChangeColor.color = Color.red;
            Timer = 0;

            //回收敌人
            EnemyPoolMnager.Intance.RecoveryEnemy(collision.GetComponent<EnemyController>().ID, collision.gameObject);
        }

        /// <summary>
        /// 改变血条
        /// </summary>
        private void ChangeBloodUI()
        {
            if (PreviousBlood == CurrentBlood)
            {
                return;
            }

            ChangeScale.x = OriginalScale.x * (CurrentBlood / Blood);
            BloodUI.localScale = ChangeScale;

            PreviousBlood = CurrentBlood;
        }

        /// <summary>
        /// 清除受击特效
        /// </summary>
        private void HitEffects()
        {
            Timer += Time.deltaTime;

            if (Timer > 0.1f)
            {
                ChangeColor.color = Color.white;
            }
        }


    }
}