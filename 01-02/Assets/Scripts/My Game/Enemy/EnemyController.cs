using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    /// <summary>
    /// ���˿�����
    /// </summary>
    public class EnemyController : MonoBehaviour
    {
        [Tooltip("ID")]
        public int ID;

        [Tooltip("�ƶ��ٶ�")]
        public float Speed = 1;

        [Tooltip("�˺�ֵ")]
        public float Damage;

        private void Update()
        {
            Move();
        }

        /// <summary>
        /// �����ƶ�
        /// </summary>
        private void Move()
        {
            this.transform.Translate(Speed * Time.deltaTime, 0, 0);
        }



    }
}