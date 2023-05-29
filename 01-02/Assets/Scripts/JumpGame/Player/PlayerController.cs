using System.Collections;
using UnityEngine;

namespace JumpGame
{
    /// <summary>
    /// PlayerController
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [Tooltip("向上的速度")]
        public float Move_Up;
        private Rigidbody2D Player;
        private Animator PlayerAnimator;

        [HideInInspector]
        public int Score; //得分
        [HideInInspector]
        public bool IsGameOver;

        private void Start()
        {
            Player = this.GetComponent<Rigidbody2D>();
            PlayerAnimator = this.GetComponent<Animator>();
            Score = 0;
            IsGameOver = false;
        }

        /// <summary>
        /// 控制跳跃
        /// </summary>
        public void Jump()
        {
            if (!IsGameOver)
            {
                Player.velocity = Vector2.up * Move_Up;
                PlayerAnimator.SetBool("IsJump", true);
                StartCoroutine(SetAnimator());
            }
        }

        private IEnumerator SetAnimator()
        {
            yield return new WaitForEndOfFrame();
            PlayerAnimator.SetBool("IsJump", false);
        }




    }
}
