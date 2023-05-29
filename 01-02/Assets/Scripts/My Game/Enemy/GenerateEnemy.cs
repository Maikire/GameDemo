using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace MyGame
{
    /// <summary>
    /// 生成敌人
    /// </summary>
    public class GenerateEnemy : MonoBehaviour
    {
        private Transform[] AllPositions; //生成点位

        [Tooltip("炸弹箱生成概率 X/100")]
        public int BomboBoxProbability = 10;
        [Tooltip("敌人最大生成时间间隔")]
        public float TimeIntervalMax = 2;
        [Tooltip("敌人最小生成时间间隔")]
        public float TimeIntervalMin = 0.5f;
        [Tooltip("间隔到达最小值需要的时间（秒）")]
        public float ToMinIntervalTime = 30;
        [HideInInspector]
        public float TimeInterval; //生成时间间隔
        private float Timer; //生成计时器

        [Tooltip("敌人最大移动速度（倍率）")]
        public float MaxSpeed = 3;
        [HideInInspector]
        public float CurrentSpeed; //当前的倍率
        [Tooltip("移动速度到达最大值需要的时间（秒）")]
        public float ToMaxSpeedTime = 60;

        private void Start()
        {
            //计算生成点位
            AllPositions = new Transform[this.transform.childCount];
            for (int i = 0; i < AllPositions.Length; i++)
            {
                AllPositions[i] = this.transform.GetChild(i);
            }

            Timer = 0; //计时器
            TimeInterval = TimeIntervalMax; //生成间隔
            CurrentSpeed = 1; //速度增量
        }

        private void Update()
        {
            Generate();
        }

        /// <summary>
        /// 生成敌人
        /// </summary>
        private void Generate()
        {
            Timer += Time.deltaTime;

            //计算生成间隔
            if (TimeInterval > TimeIntervalMin)
            {
                TimeInterval -= ((TimeIntervalMax - TimeIntervalMin) / ToMinIntervalTime) * Time.deltaTime;
            }
            else
            {
                TimeInterval = TimeIntervalMin;
            }

            //计算速度增量
            if (CurrentSpeed < MaxSpeed)
            {
                CurrentSpeed += MaxSpeed / ToMaxSpeedTime * Time.deltaTime;
            }
            else
            {
                CurrentSpeed = MaxSpeed;
            }

            if (Timer > TimeInterval)
            {
                int RandomPosition = Random.Range(0, AllPositions.Length);
                int RandomEnemy = Random.Range(0, EnemyPoolMnager.Intance.AllEnemyPrefabs.Length);

                GameObject temp = EnemyPoolMnager.Intance.GetEnemy(RandomEnemy, AllPositions[RandomPosition].position);
                temp.GetComponent<EnemyController>().Speed *= CurrentSpeed;

                if (TimeInterval == TimeIntervalMin)
                {
                    if (Random.Range(0, 100) < BomboBoxProbability)
                    {
                        BulletPoolMnager.Intance.GetBullet(3, AllPositions[RandomPosition].position + (Vector3.left * 2));
                    }
                }

                Timer = 0;
            }
        }


    }
}