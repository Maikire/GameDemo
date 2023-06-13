using UnityEngine;

namespace ChatDemo
{
    /// <summary>
    /// 服务端逻辑
    /// </summary>
    public class ChatSever : MonoBehaviour
    {
        [Tooltip("增加消息")]
        public CreateSeverMessage Create;

        private void OnEnable()
        {
            SeverUdpNetService.Instance.OnReceive += OnReceiveMessage;
        }

        private void OnDisable()
        {
            SeverUdpNetService.Instance.OnReceive -= OnReceiveMessage;
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="receive"></param>
        private void OnReceiveMessage(object sender, ReceiveEventArgs receive)
        {
            if (receive.Message.Type != MessageType.General)
            {
                Create.AddMessage(receive.SourceRemote);
            }
        }


    }
}
