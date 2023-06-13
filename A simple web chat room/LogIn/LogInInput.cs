using Common;
using TMPro;
using UnityEngine;

namespace ChatDemo
{
    /// <summary>
    /// 登录信息输入
    /// </summary>
    public class LogInInput : MonoBehaviour
    {
        /// <summary>
        /// Input_Name
        /// </summary>
        private TMP_InputField Input_Name;
        /// <summary>
        /// Input_IP
        /// </summary>
        private TMP_InputField Input_IP;
        /// <summary>
        /// Input_Port
        /// </summary>
        private TMP_InputField Input_Port;

        /// <summary>
        /// 登录信息
        /// </summary>
        public LogInContent Content
        {
            get
            {
                return new LogInContent()
                {
                    Name = Input_Name?.text,
                    IP = Input_IP.text,
                    Port = Input_Port.text,
                };
            }
        }

        private void Awake()
        {
            Input_Name = this.transform.FindChildByName("Input_Name")?.GetComponent<TMP_InputField>();
            Input_IP = this.transform.FindChildByName("Input_IP").GetComponent<TMP_InputField>();
            Input_Port = this.transform.FindChildByName("Input_Port").GetComponent<TMP_InputField>();
        }


    }
}
