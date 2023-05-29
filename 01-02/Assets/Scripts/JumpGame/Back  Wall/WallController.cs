using UnityEngine;

namespace JumpGame
{
    /// <summary>
    /// WallController
    /// </summary>
    public class WallController : MonoBehaviour
    {
        [Tooltip("移动速度")]
        public float MoveSpeed;
        [Tooltip("当前移动速度")]
        public float CurrentMoveSpeed;
        [HideInInspector]
        [Tooltip("原始位置")]
        public Vector2 OriginalLocation;
        [HideInInspector]
        [Tooltip("true：启用触发器")]
        public bool IsTriggerActive;

        private void Start()
        {
            OriginalLocation = this.transform.position;
            IsTriggerActive = true;
            CurrentMoveSpeed = MoveSpeed;
        }

        private void Update()
        {
            Move();
        }

        /// <summary>
        /// 控制移动
        /// </summary>
        private void Move()
        {
            this.transform.Translate(Vector2.left * CurrentMoveSpeed * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            collision.transform.GetComponent<PlayerController>().IsGameOver = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (IsTriggerActive)
            {
                collision.GetComponent<PlayerController>().Score++;
            }
        }


    }
}