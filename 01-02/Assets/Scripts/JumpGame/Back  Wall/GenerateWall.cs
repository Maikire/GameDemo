using UnityEngine;

namespace JumpGame
{
    /// <summary>
    /// GenerateWall
    /// </summary>
    public class GenerateWall : MonoBehaviour
    {
        [Tooltip("Y位置上限")]
        public float PositionMax = 2.4f;
        [Tooltip("Y位置下限")]
        public float PositionMin = -2.33f;
        [Tooltip("位置间隔")]
        public float Interval = 6;
        [Tooltip("移出屏幕的偏移量")]
        public float Offset = 150f;
        [HideInInspector]
        public Transform[] Walls;
        private float RandomPosition; //随机位置
        private Vector3 NewPosition;
        [HideInInspector]
        public Transform Last; //最后一个

        [Tooltip("加分出现概率 X/100")]
        public float Probability = 20;

        private void Start()
        {
            Walls = new Transform[this.transform.childCount];
            NewPosition = new Vector3(0, 0, 0);
            Initialization();
            Last = Walls[Walls.Length - 1];
        }

        private void Update()
        {
            GeneratePosition();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialization()
        {
            //获取所有的Wall
            for (int i = 0; i < Walls.Length; i++)
            {
                Walls[i] = this.transform.GetChild(i);
            }
        }

        /// <summary>
        /// 生成位置
        /// </summary>
        private void GeneratePosition()
        {
            foreach (var item in Walls)
            {
                //移出屏幕的判定
                if (Camera.main.WorldToScreenPoint(item.position).x < -Offset)
                {
                    //重新移动到新位置
                    RandomPosition = Random.Range(PositionMin, PositionMax);
                    NewPosition.x = Last.position.x + Interval;
                    NewPosition.y = RandomPosition;
                    item.position = NewPosition;

                    //加10分的物体，概率出现
                    if (Random.Range(0, 100) < Probability)
                    {
                        GameObject temp = DrumstickPool.Intance.GetDrumstick(item.position);
                        temp.GetComponent<DrumstickController>().MoveSpeed = item.GetComponent<WallController>().CurrentMoveSpeed;
                    }

                    Last = item;
                }
            }
        }



    }
}