using System;
using System.IO;
using System.Text;

namespace ChatDemo
{
    /// <summary>
    /// 聊天信息
    /// </summary>
    public class ChatMessage
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageType Type;

        /// <summary>
        /// 发送者
        /// </summary>
        public string SenderName;

        /// <summary>
        /// 发送内容
        /// </summary>
        public string Content;

        /// <summary>
        /// ChatMessage
        /// </summary>
        public ChatMessage() { }

        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="messageType">messageType</param>
        /// <param name="senderName">senderName</param>
        /// <param name="content">content</param>
        public ChatMessage(MessageType messageType, string senderName, string content)
        {
            Type = messageType;
            SenderName = senderName;
            Content = content;
        }

        /// <summary>
        /// object -> byte[]
        /// </summary>
        /// <returns></returns>
        public byte[] ObjectToBytes()
        {
            //string/int/bool... -- 二进制写入器 -- 内存流 --> byte[]

            //内存流
            using (MemoryStream stream = new MemoryStream())
            {
                //二进制写入器
                //属性 -> 内存流
                BinaryWriter writer = new BinaryWriter(stream);

                WriteString(writer, Type.ToString());
                WriteString(writer, SenderName);
                WriteString(writer, Content);

                //内存流 -> byte[]
                return stream.ToArray();
            }
        }

        /// <summary>
        /// 写入二进制数据
        /// </summary>
        /// <param name="writer">writer</param>
        /// <param name="str">string</param>
        private void WriteString(BinaryWriter writer, string str)
        {
            //不用这个，因为这个只支持一种编码
            //writer.Write(str);

            //编码
            byte[] byte_Type = Encoding.UTF8.GetBytes(str);
            //写入长度
            writer.Write(byte_Type.Length);
            //写入内容
            writer.Write(byte_Type);
        }

        /// <summary>
        /// byte[] -> object
        /// </summary>
        /// <param name="bytes">bytes</param>
        /// <returns></returns>
        public static ChatMessage BytesToObject(byte[] bytes)
        {
            //byte[] -- 内存流 -- 二进制读取器 --> string/int/bool...

            using (MemoryStream stream = new MemoryStream(bytes))
            {
                BinaryReader reader = new BinaryReader(stream);

                MessageType type = (MessageType)Enum.Parse(typeof(MessageType), ReadString(reader));
                string senderName = ReadString(reader);
                string content = ReadString(reader);

                return new ChatMessage(type, senderName, content);
            }
        }

        /// <summary>
        /// 读取二进制数据
        /// </summary>
        /// <param name="reader">reader</param>
        /// <returns></returns>
        private static string ReadString(BinaryReader reader)
        {
            //不用这个，因为这个只支持一种编码
            //reader.ReadString();

            //读4个字节：标记了接下来要读取的长度
            int length = reader.ReadInt32();
            //读指定的长度
            byte[] bytes = reader.ReadBytes(length);
            //转字符串
            return Encoding.UTF8.GetString(bytes);
        }


    }
}
