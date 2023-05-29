using System.Collections;
using UnityEngine;

namespace JumpGame
{
    /// <summary>
    /// ReStart
    /// </summary>
    public class ReStart : MonoBehaviour
    {
        [HideInInspector]
        public bool IsStartGame = false;

        public GenerateWall WallManger;
        public PlayerController Player;
        public GameObject GameoverUI;
        public GameObject StartUI;
        public GameObject PressUI;

        /// <summary>
        /// 加速的次数
        /// </summary>
        private int AccelerationTimes = 0;

        private void Update()
        {
            JudgeScore();
            IfGameOver();
        }

        public void StartButton()
        {
            IsStartGame = true;
            ReStartAll();
            Player.gameObject.SetActive(true);
            PressUI.SetActive(true);
            StartUI.SetActive(false);
        }

        public void ReStartButton()
        {
            ReStartAll();
            PressUI.SetActive(true);
            GameoverUI.SetActive(false);
        }

        private void StopAll()
        {
            foreach (var item in WallManger.Walls)
            {
                item.GetComponent<WallController>().CurrentMoveSpeed = 0;
            }
            foreach (var item in GameObject.FindGameObjectsWithTag("Drumstick"))
            {
                item.GetComponent<DrumstickController>().MoveSpeed = 0;
            }
        }

        private void ReStartAll()
        {
            Player.IsGameOver = false;
            Player.Score = 0;

            //Wall
            foreach (var item in WallManger.Walls)
            {
                WallController wallController = item.GetComponent<WallController>();
                wallController.IsTriggerActive = false;
                wallController.CurrentMoveSpeed = wallController.MoveSpeed;
                item.position = wallController.OriginalLocation;
                this.StartCoroutine(ResetWallTrigger(wallController));
            }
            WallManger.Last = WallManger.Walls[WallManger.Walls.Length - 1];

            //Drumstick
            foreach (var item in GameObject.FindGameObjectsWithTag("Drumstick"))
            {
                DrumstickPool.Intance.RecoveryDrumstick(item);
            }

            //Player
            Player.transform.position = Vector3.left * 2;

            //Acceleration Times
            AccelerationTimes = 0;
        }

        private void IfGameOver()
        {
            if (Player.IsGameOver)
            {
                StopAll();

                if (IsStartGame)
                {
                    PressUI.SetActive(false);
                    GameoverUI.SetActive(true);
                }
            }
        }

        /// <summary>
        /// 延迟开启触发器检测
        /// </summary>
        /// <param name="wallController"></param>
        /// <returns></returns>
        private IEnumerator ResetWallTrigger(WallController wallController)
        {
            yield return new WaitForSeconds(0.5f);
            wallController.IsTriggerActive = true;
        }

        /// <summary>
        /// 根据分数增加移动速度
        /// </summary>
        private void JudgeScore()
        {
            if (AccelerationTimes == 0)
            {
                if (Player.Score >= 40)
                {
                    SetSpeeed(3.5f);
                    AccelerationTimes++;
                }
            }
            else if (AccelerationTimes == 1)
            {
                if (Player.Score >= 80)
                {
                    SetSpeeed(4);
                    AccelerationTimes++;
                }
            }
        }

        /// <summary>
        /// 设置速度
        /// </summary>
        /// <param name="speed">速度</param>
        private void SetSpeeed(float speed)
        {
            foreach (var item in WallManger.Walls)
            {
                item.GetComponent<WallController>().CurrentMoveSpeed = speed;
            }
            foreach (var item in GameObject.FindGameObjectsWithTag("Drumstick"))
            {
                item.GetComponent<DrumstickController>().MoveSpeed = speed;
            }
        }


    }
}
