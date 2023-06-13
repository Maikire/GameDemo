using System;
using System.Net;

namespace ChatDemo
{
    /// <summary>
    /// 接收事件的信息类
    /// </summary>
    public class ReceiveEventArgs : EventArgs
    {
        /// <summary>
        /// ChatMessage
        /// </summary>
        public ChatMessage Message;

        /// <summary>
        /// IPEndPoint
        /// </summary>
        public IPEndPoint SourceRemote;

        public ReceiveEventArgs() { }

        public ReceiveEventArgs(ChatMessage message, IPEndPoint sourceRemote)
        {
            Message = message;
            SourceRemote = sourceRemote;
        }


    }
}

