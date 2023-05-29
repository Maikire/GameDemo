using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    /// <summary>
    /// 子弹池
    /// </summary>
    public class BulletPoolMnager : MonoBehaviour
    {
        //ID:
        //Bullet：0
        //BulletExplosion：1
        //Bomb：2
        //BombBox：3

        public static BulletPoolMnager Intance; //使用这个类的入口

        public GameObject[] AllBulletPrefabs;
        private Dictionary<int, GameObject> AllBullet;
        private Dictionary<int, Queue<GameObject>> AllBulletPool;

        private void Awake()
        {
            Intance = this;
        }

        private void Start()
        {
            AllBullet = new Dictionary<int, GameObject>();
            AllBulletPool = new Dictionary<int, Queue<GameObject>>();

            for (int i = 0; i < AllBulletPrefabs.Length; i++)
            {
                AllBullet.Add(i, AllBulletPrefabs[i]);
                AllBulletPool.Add(i, new Queue<GameObject>());
            }
        }

        /// <summary>
        /// 生成子弹
        /// </summary>
        /// <param name="id"></param>
        /// <param name="posion"></param>
        /// <returns></returns>
        public GameObject GetBullet(int id, Vector2 posion)
        {
            GameObject tempBullet;
            Queue<GameObject> tempQueue;
            AllBullet.TryGetValue(id, out tempBullet);
            AllBulletPool.TryGetValue(id, out tempQueue);

            if (tempQueue.Count > 0)
            {
                GameObject bullet = tempQueue.Dequeue();
                bullet.transform.position = posion;
                bullet.transform.rotation = Quaternion.identity;
                bullet.transform.localScale = tempBullet.transform.localScale;

                bullet.SetActive(true);
                return bullet;
            }
            else
            {
                return Instantiate(tempBullet, posion, Quaternion.identity);
            }
        }

        /// <summary>
        /// 回收子弹
        /// </summary>
        /// <param name="id"></param>
        /// <param name="enemy"></param>
        public void RecoveryBullet(int id, GameObject bullet)
        {
            Queue<GameObject> tempQueue;
            AllBulletPool.TryGetValue(id, out tempQueue);

            tempQueue.Enqueue(bullet);
            bullet.SetActive(false);
        }


    }
}