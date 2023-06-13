using Common;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ChatDemo
{
    /// <summary>
    /// 客户端UDP网络服务
    /// </summary>
    public class ClientUdpNetService : MonoSingleton<ClientUdpNetService>
    {
        /// <summary>
        /// 接收到消息后触发
        /// </summary>
        public event EventHandler<ReceiveEventArgs> OnReceive;

        /// <summary>
        /// 登录信息
        /// </summary>
        public LogInContent Content;

        /// <summary>
        /// UdpClient
        /// </summary>
        private UdpClient UDPClient;

        /// <summary>
        /// 客户端线程
        /// </summary>
        private Thread ClientThread;

        private void Start()
        {
            ClientThread = new Thread(ReceiveMessage);
        }

        /// <summary>
        /// 初始化
        /// 由登录窗口传递IP和端口
        /// </summary>
        /// <param name="content">登录信息</param>
        public void Initialize(LogInContent content)
        {
            Content = content;

            //随机分配端口
            UDPClient = new UdpClient();

            //与服务端连接（仅仅是配置自身的Socket）
            //单播：只能给服务器发数据
            UDPClient.Connect(IPAddress.Parse(Content.IP), int.Parse(Content.Port));

            //开启线程
            ClientThread.Start();

            NotiyfSever(MessageType.OnLine);
        }

        private void NotiyfSever(MessageType type)
        {
            if (Content == null) return;
            SendMessage(new ChatMessage(type, Content.Name, string.Empty));
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="message">信息</param>
        public void SendMessage(ChatMessage message)
        {
            byte[] bytes = message.ObjectToBytes();
            //创建Socket对象时建立了链接，所以不能绑定终结点
            UDPClient.Send(bytes, bytes.Length);
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="message"></param>
        private void ReceiveMessage()
        {
            while (true)
            {
                //任意IP地址，任意端口
                IPEndPoint sourceRemote = new IPEndPoint(IPAddress.Any, 0);

                //接收消息
                byte[] bytes = UDPClient.Receive(ref sourceRemote);

                //转化消息
                ChatMessage message = ChatMessage.BytesToObject(bytes);

                //事件参数
                ReceiveEventArgs args = new ReceiveEventArgs(message, sourceRemote);

                //在主线程中触发事件
                ThreadCrossHelper.Instance.ExecuteOnMainThread(() =>
                {
                    OnReceive?.Invoke(this, args);
                });
            }
        }

        private void OnApplicationQuit()
        {
            NotiyfSever(MessageType.OffLine);
            ClientThread.Abort();
            UDPClient?.Dispose();
        }


    }
}
