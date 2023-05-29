using UnityEngine;

namespace MyGame
{
    /// <summary>
    /// 技能
    /// </summary>
    public class Skill : MonoBehaviour
    {
        //加快攻速：Z  ID: 0
        //敌人减速：X  ID: 1
        //炸弹：C  ID: 2

        #region  加快攻速
        private bool TurnOn_FireSpeedUp = false; //开启

        [Tooltip("加快攻速（乘算）")]
        public float FireSpeedUp = 1.5f;

        [Tooltip("加快攻速持续时间")]
        public float Duration_FireSpeedUp = 2;
        [HideInInspector]
        public float CurrentDuration_FireSpeedUp = 0; //当前持续时间

        [Tooltip("加快攻速冷却时间")]
        public float CoolingTime_FireSpeedUp = 5;
        [HideInInspector]
        public float CurrentCoolingTime_FireSpeedUp = 0; //当前冷却时间
        #endregion

        #region 敌人减速
        private bool TurnOn_MoveSpeedDown = false; //开启
        private GameObject[] EffectedEnemys;

        [Tooltip("敌人减速（乘算）")]
        public float MoveSpeedDown = 0.5f;

        [Tooltip("敌人减速持续时间")]
        public float Duration_MoveSpeedDown = 2;
        [HideInInspector]
        public float CurrentDuration_MoveSpeedDown = 0; //当前持续时间

        [Tooltip("敌人减速冷却时间")]
        public float CoolingTime_MoveSpeedDown = 5;
        [HideInInspector]
        public float CurrentCoolingTime_MoveSpeedDown = 0; //当前冷却时间
        #endregion

        #region 炸弹
        [Tooltip("炸弹可用次数")]
        public int AvailableTimes_Bomb = 3;
        [HideInInspector]
        public int CurrentAvailableTimes_Bomb;
        #endregion

        private void Start()
        {
            CurrentAvailableTimes_Bomb = AvailableTimes_Bomb;
        }

        private void Update()
        {
            Skill_FireSpeedUp();
            Skill_MoveSpeedDown();
            Skill_Bomb();
        }

        /// <summary>
        /// 计时器
        /// </summary>
        /// <param name="coolingTime"></param>
        private float Timer(float time)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            if (time <= 0)
            {
                return 0;
            }
            return time;
        }

        /// <summary>
        /// 处理计时器
        /// </summary>
        /// <param name="CurrentDuration">当前持续时间</param>
        /// <param name="CurrentCoolingTime">当前冷却时间</param>
        private void UseTimer(ref float CurrentDuration, ref float CurrentCoolingTime)
        {
            //持续时间
            if (CurrentDuration > 0)
            {
                CurrentDuration = Timer(CurrentDuration);
            }

            //冷却时间
            if (CurrentCoolingTime > 0)
            {
                CurrentCoolingTime = Timer(CurrentCoolingTime);
            }
        }

        /// <summary>
        /// 加快攻速
        /// </summary>
        private void Skill_FireSpeedUp()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (CurrentCoolingTime_FireSpeedUp == 0)
                {
                    CurrentDuration_FireSpeedUp = Duration_FireSpeedUp;
                    CurrentCoolingTime_FireSpeedUp = CoolingTime_FireSpeedUp;

                    this.GetComponent<PlayerFire>().FireSpeed *= FireSpeedUp;
                    TurnOn_FireSpeedUp = true;
                }
            }

            if (TurnOn_FireSpeedUp)
            {
                if (CurrentDuration_FireSpeedUp == 0)
                {
                    this.GetComponent<PlayerFire>().FireSpeed /= FireSpeedUp;
                    TurnOn_FireSpeedUp = false;
                }
            }

            UseTimer(ref CurrentDuration_FireSpeedUp, ref CurrentCoolingTime_FireSpeedUp);
        }

        /// <summary>
        /// 敌人减速
        /// </summary>
        private void Skill_MoveSpeedDown()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (CurrentCoolingTime_MoveSpeedDown == 0)
                {
                    CurrentDuration_MoveSpeedDown = Duration_MoveSpeedDown;
                    CurrentCoolingTime_MoveSpeedDown = CoolingTime_MoveSpeedDown;

                    EffectedEnemys = GameObject.FindGameObjectsWithTag("Enemy");
                    foreach (var item in EffectedEnemys)
                    {
                        item.GetComponent<EnemyController>().Speed *= MoveSpeedDown;
                    }
                    TurnOn_MoveSpeedDown = true;
                }
            }

            if (TurnOn_MoveSpeedDown)
            {
                if (CurrentDuration_MoveSpeedDown == 0)
                {
                    foreach (var item in EffectedEnemys)
                    {
                        //速度永远减慢 0.2
                        item.GetComponent<EnemyController>().Speed /= MoveSpeedDown * 1.2f;
                    }
                    TurnOn_MoveSpeedDown = false;
                }
            }

            UseTimer(ref CurrentDuration_MoveSpeedDown, ref CurrentCoolingTime_MoveSpeedDown);
        }

        /// <summary>
        /// 炸弹
        /// </summary>
        private void Skill_Bomb()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (CurrentAvailableTimes_Bomb > 0)
                {
                    BulletPoolMnager.Intance.GetBullet(2, this.transform.position);
                    CurrentAvailableTimes_Bomb--;
                }
            }
        }


    }
}