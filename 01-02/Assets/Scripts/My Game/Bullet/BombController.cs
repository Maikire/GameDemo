using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    /// <summary>
    /// BombController
    /// </summary>
    public class BombController : MonoBehaviour
    {
        [Tooltip("ID")]
        public int ID;

        [Tooltip("爆炸计时")]
        public float BombTime = 1;
        [HideInInspector]
        public float BombTimer = 0; //爆炸计时器

        private void Update()
        {
            BombTimer += Time.deltaTime;

            if (BombTimer > BombTime)
            {
                GameObject temp = BulletPoolMnager.Intance.GetBullet(1, this.transform.position);
                temp.transform.localScale = Vector3.one * 2;

                foreach (var item in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    EnemyPoolMnager.Intance.RecoveryEnemy(item.GetComponent<EnemyController>().ID, item);
                }

                BombTimer = 0;

                BulletPoolMnager.Intance.RecoveryBullet(ID, this.gameObject);
            }
        }


    }
}