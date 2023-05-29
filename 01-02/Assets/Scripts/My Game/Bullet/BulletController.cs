using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    /// <summary>
    /// 子弹的控制器
    /// </summary>
    public class BulletController : MonoBehaviour
    {
        [Tooltip("ID")]
        public int ID;

        [Tooltip("移动速度")]
        public float Speed = 1;

        private void Update()
        {
            Move();

            if (Camera.main.WorldToScreenPoint(this.transform.position).x < -150)
            {
                BulletPoolMnager.Intance.RecoveryBullet(ID, this.gameObject);
            }
        }

        /// <summary>
        /// 控制移动
        /// </summary>
        private void Move()
        {
            this.transform.Translate(-Speed * Time.deltaTime, 0, 0);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag != "BombBox")
            {
                BulletPoolMnager.Intance.GetBullet(1, collision.transform.position);
                EnemyPoolMnager.Intance.RecoveryEnemy(collision.GetComponent<EnemyController>().ID, collision.gameObject);
                BulletPoolMnager.Intance.RecoveryBullet(ID, this.gameObject);
            }
        }


    }
}