using UnityEngine;

namespace ChatDemo
{
    /// <summary>
    /// 创建客户端消息
    /// </summary>
    public class CreateClientMessage : MonoBehaviour
    {
        [Tooltip("我的消息预制件")]
        public GameObject MessagePrefab_My;
        [Tooltip("其他人的消息预制件")]
        public GameObject MessagePrefab_Other;

        /// <summary>
        /// 增加新消息
        /// </summary>
        /// <param name="ower">消息属于谁</param>
        /// <param name="chatMessage">消息</param>
        public void AddMessage(MessageOwer ower, ChatMessage chatMessage)
        {
            switch (ower)
            {
                case MessageOwer.My:
                    Create(MessagePrefab_My, chatMessage);
                    break;

                case MessageOwer.Other:
                    Create(MessagePrefab_Other, chatMessage);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 创建物体
        /// </summary>
        /// <param name="prefab">预制件</param>
        /// <param name="chatMessage">信息</param>
        private void Create(GameObject prefab, ChatMessage chatMessage)
        {
            GameObject obj = Instantiate(prefab, this.transform);
            obj.GetComponent<ClientMessageManger>().SetMessage(chatMessage.Content, chatMessage.SenderName);
        }


    }
}
