using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    /// <summary>
    /// 敌人控制器
    /// </summary>
    public class EnemyController : MonoBehaviour
    {
        [Tooltip("ID")]
        public int ID;

        [Tooltip("移动速度")]
        public float Speed = 1;

        [Tooltip("伤害值")]
        public float Damage;

        private void Update()
        {
            Move();
        }

        /// <summary>
        /// 控制移动
        /// </summary>
        private void Move()
        {
            this.transform.Translate(Speed * Time.deltaTime, 0, 0);
        }



    }
}