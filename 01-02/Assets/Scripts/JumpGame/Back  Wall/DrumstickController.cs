using UnityEngine;

namespace JumpGame
{
    /// <summary>
    /// DrumstickController
    /// </summary>
    public class DrumstickController : MonoBehaviour
    {
        [Tooltip("当前移动速度")]
        public float MoveSpeed;
        [Tooltip("移出屏幕的偏移量")]
        public float Offset = 150;

        private void Update()
        {
            Move();
            Recovery();
        }

        /// <summary>
        /// 控制移动
        /// </summary>
        private void Move()
        {
            this.transform.Translate(Vector2.left * MoveSpeed * Time.deltaTime);
        }

        /// <summary>
        /// Recovery
        /// </summary>
        private void Recovery()
        {
            if (Camera.main.WorldToScreenPoint(this.transform.position).x < -Offset)
            {
                DrumstickPool.Intance.RecoveryDrumstick(this.gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.GetComponent<PlayerController>().Score += 10;
            DrumstickPool.Intance.RecoveryDrumstick(this.gameObject);
        }


    }
}