using Common;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChatDemo
{
    /// <summary>
    /// 客户端信息管理
    /// </summary>
    public class ClientMessageManger : MonoBehaviour
    {
        /// <summary>
        /// 设置信息
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="name">发送者</param>
        public void SetMessage(string message, string name)
        {
            this.transform.FindChildByName("Message").GetComponent<TextMeshProUGUI>().text = message;
            this.transform.FindChildByName("Name").GetComponent<TextMeshProUGUI>().text = name;
            this.transform.FindChildByName("Time").GetComponent<TextMeshProUGUI>().text = DateTime.Now.ToString();

        }


    }
}
