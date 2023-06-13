using Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChatDemo
{
    /// <summary>
    /// 客户端逻辑
    /// </summary>
    public class ChatClient : MonoBehaviour
    {
        [Tooltip("增加消息")]
        public CreateClientMessage Create;

        /// <summary>
        /// SendButton
        /// </summary>
        private Button SendButton;
        /// <summary>
        /// 输入的信息
        /// </summary>
        private TMP_InputField InputMessage;

        private void Awake()
        {
            SendButton = this.transform.FindChildByName("SendButton").GetComponent<Button>();
            InputMessage = this.transform.FindChildByName("InputMessage").GetComponent<TMP_InputField>();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Return))
            {
                OnSendMessage();
            }
        }

        private void OnEnable()
        {
            SendButton.onClick.AddListener(OnSendMessage);
            ClientUdpNetService.Instance.OnReceive += OnReceiveMessage;
        }

        private void OnDisable()
        {
            SendButton.onClick.RemoveListener(OnSendMessage);
            ClientUdpNetService.Instance.OnReceive -= OnReceiveMessage;
        }

        /// <summary>
        /// 按下发送消息按钮
        /// </summary>
        private void OnSendMessage()
        {
            if (InputMessage.text == string.Empty) return;

            ChatMessage message = new ChatMessage()
            {
                Type = MessageType.General,
                SenderName = ClientUdpNetService.Instance.Content.Name,
                Content = InputMessage.text
            };

            Create.AddMessage(MessageOwer.My, message);
            ClientUdpNetService.Instance.SendMessage(message);

            InputMessage.text = string.Empty;
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="receive"></param>
        private void OnReceiveMessage(object sender, ReceiveEventArgs receive)
        {
            if (receive.Message.SenderName != ClientUdpNetService.Instance.Content.Name)
            {
                Create.AddMessage(MessageOwer.Other, receive.Message);
            }
        }


    }
}
