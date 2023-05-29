using UnityEngine;

namespace MyGame
{
    /// <summary>
    /// 控制角色 移动/攻击
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [Tooltip("移动速度")]
        public float Speed = 8;
        private float Horizontal; //左右
        private float Vertical; //上下
        private Vector2 TargetPosion; //目标位置
        private Rigidbody2D Player;

        private void Start()
        {
            TargetPosion = new Vector2(0, 0);
            Player = this.GetComponent<Rigidbody2D>();
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
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");

            if (Horizontal == 0 && Vertical == 0)
            {
                return;
            }

            TargetPosion.x = Horizontal;
            TargetPosion.y = Vertical;

            Player.MovePosition(Player.position + TargetPosion * Speed * Time.deltaTime);
        }







    }
}
