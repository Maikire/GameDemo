using System.Collections.Generic;
using UnityEngine;

namespace JumpGame
{
    /// <summary>
    /// DrumstickPool
    /// </summary>
    public class DrumstickPool : MonoBehaviour
    {
        public static DrumstickPool Intance; //使用这个类的入口

        [Tooltip("Drumstick")]
        public GameObject Drumstick;
        private Queue<GameObject> Pool;

        private void Awake()
        {
            Intance = this;
        }

        private void Start()
        {
            Pool = new Queue<GameObject>();
        }

        /// <summary>
        /// GetDrumstick
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public GameObject GetDrumstick(Vector2 position)
        {
            if (Pool.Count > 0)
            {
                GameObject temp = Pool.Dequeue();
                temp.transform.position = position;
                temp.SetActive(true);
                return temp;
            }
            else
            {
                return Instantiate(Drumstick, position, Quaternion.identity);
            }
        }

        /// <summary>
        /// RecoveryDrumstick
        /// </summary>
        /// <param name="drumstick"></param>
        public void RecoveryDrumstick(GameObject drumstick)
        {
            Pool.Enqueue(drumstick);
            drumstick.SetActive(false);
        }


    }
}