using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MyGame
{
    /// <summary>
    /// 射击
    /// </summary>
    public class PlayerFire : MonoBehaviour
    {
        [Tooltip("攻击速度")]
        public float FireSpeed = 1;
        private float Timer = 0;
        [HideInInspector]
        public bool IsFire; //是否开启射击

        private void Start()
        {
            IsFire = true;
        }

        private void Update()
        {
            if (IsFire)
            {
                Timer += Time.deltaTime;

                if (Timer > 1 / FireSpeed)
                {
                    BulletPoolMnager.Intance.GetBullet(0, this.transform.GetChild(0).position);
                    Timer = 0;
                }
            }
        }



    }
}