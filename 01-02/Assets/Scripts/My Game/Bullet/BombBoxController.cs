using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    /// <summary>
    /// AmmoBoxController
    /// </summary>
    public class BombBoxController : MonoBehaviour
    {
        [Tooltip("ID")]
        public int ID;

        [Tooltip("移动速度")]
        public float Speed = 1.5f;

        private void Update()
        {
            Move();

            if (Camera.main.WorldToScreenPoint(this.transform.position).x > (Screen.width + 150))
            {
                BulletPoolMnager.Intance.RecoveryBullet(ID, this.gameObject);
            }
        }

        /// <summary>
        /// 控制移动
        /// </summary>
        private void Move()
        {
            this.transform.Translate(Speed * Time.deltaTime, 0, 0);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                collision.GetComponent<Skill>().CurrentAvailableTimes_Bomb++;
                BulletPoolMnager.Intance.RecoveryBullet(ID, this.gameObject);
            }
        }


    }
}