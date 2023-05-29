using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

namespace MyGame
{
    /// <summary>
    /// 敌人池
    /// </summary>
    public class EnemyPoolMnager : MonoBehaviour
    {
        //ID：
        //Alien Slug：0
        //Zombie：1

        public static EnemyPoolMnager Intance; //使用这个类的入口

        public GameObject[] AllEnemyPrefabs;
        private Dictionary<int, GameObject> AllEnemy;
        private Dictionary<int, Queue<GameObject>> AllEnemyPool;

        private void Awake()
        {
            Intance = this;
        }

        private void Start()
        {
            AllEnemy = new Dictionary<int, GameObject>();
            AllEnemyPool = new Dictionary<int, Queue<GameObject>>();

            for (int i = 0; i < AllEnemyPrefabs.Length; i++)
            {
                AllEnemy.Add(i, AllEnemyPrefabs[i]);
                AllEnemyPool.Add(i, new Queue<GameObject>());
            }
        }

        /// <summary>
        /// 生成敌人
        /// </summary>
        /// <param name="id"></param>
        /// <param name="posion"></param>
        /// <returns></returns>
        public GameObject GetEnemy(int id, Vector2 posion)
        {
            Queue<GameObject> tempQueue;
            GameObject tempEnemy;
            AllEnemy.TryGetValue(id, out tempEnemy);
            AllEnemyPool.TryGetValue(id, out tempQueue);

            if (tempQueue.Count > 0)
            {
                GameObject enemy = tempQueue.Dequeue();

                enemy.transform.position = posion;
                enemy.transform.rotation = Quaternion.identity;
                enemy.GetComponent<EnemyController>().Speed = tempEnemy.GetComponent<EnemyController>().Speed;

                enemy.SetActive(true);
                return enemy;
            }
            else
            {
                return Instantiate(tempEnemy, posion, Quaternion.identity);
            }
        }

        /// <summary>
        /// 回收敌人
        /// </summary>
        /// <param name="id"></param>
        /// <param name="enemy"></param>
        public void RecoveryEnemy(int id, GameObject enemy)
        {
            Queue<GameObject> tempQueue;
            AllEnemyPool.TryGetValue(id, out tempQueue);

            tempQueue.Enqueue(enemy);
            enemy.SetActive(false);
        }

    }
}