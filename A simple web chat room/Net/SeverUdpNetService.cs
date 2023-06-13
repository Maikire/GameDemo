using Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ChatDemo
{
    /// <summary>
    /// 服务端UDP网络服务
    /// </summary>
    public class SeverUdpNetService : MonoSingleton<SeverUdpNetService>
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
        /// 服务端线程
        /// </summary>
        private Thread SeverThread;

        /// <summary>
        /// 客户端列表
        /// </summary>
        public List<IPEndPoint> ClientRemotes { get; private set; }

        private void Start()
        {
            ClientRemotes = new List<IPEndPoint>();
            SeverThread = new Thread(ReceiveMessage);
        }

        /// <summary>
        /// 初始化
        /// 由登录窗口传递IP和端口
        /// </summary>
        /// <param name="content">登录信息</param>
        public void Initialize(LogInContent content)
        {
            Content = content;

            //分配端口
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(Content.IP), int.Parse(Content.Port));
            UDPClient = new UdpClient(iPEndPoint);

            //开启线程
            SeverThread.Start();
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="remote">目标终结点</param>
        public void SendMessage(ChatMessage message, IPEndPoint remote)
        {
            byte[] bytes = message.ObjectToBytes();
            UDPClient.Send(bytes, bytes.Length, remote);
        }

        /// <summary>
        /// 接收消息
        /// </summary>
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

                //根据消息类型执行对应的逻辑
                MessageReceived(message, sourceRemote);

                //事件参数
                ReceiveEventArgs args = new ReceiveEventArgs(message, sourceRemote);

                //在主线程中触发事件
                ThreadCrossHelper.Instance.ExecuteOnMainThread(() =>
                {
                    OnReceive?.Invoke(this, args);
                });
            }
        }

        /// <summary>
        /// 根据消息类型执行对应的逻辑
        /// </summary>
        /// <param name="message"></param>
        /// <param name="remote"></param>
        private void MessageReceived(ChatMessage message, IPEndPoint remote)
        {
            switch (message.Type)
            {
                case MessageType.OnLine:
                    ClientRemotes.Add(remote);
                    break;

                case MessageType.OffLine:
                    ClientRemotes.Remove(remote);
                    break;

                case MessageType.General:
                    ClientRemotes.ForEach((item) => { SendMessage(message, item); });
                    break;

                default:
                    break;
            }
        }

        private void OnApplicationQuit()
        {
            SeverThread.Abort();
            UDPClient?.Dispose();
        }


    }
}
