using Common;
using System.Net;
using TMPro;
using UnityEngine;

namespace ChatDemo
{
    /// <summary>
    /// 服务端信息管理
    /// </summary>
    public class SeverMessageManger : MonoBehaviour
    {
        /// <summary>
        /// 设置信息
        /// </summary>
        /// <param name="sourceRemote">source remote</param>
        public void SetMessage(IPEndPoint sourceRemote)
        {
            this.transform.FindChildByName("SourceIPInformation").GetComponent<TextMeshProUGUI>().text = sourceRemote.Address.ToString();
            this.transform.FindChildByName("SourcePortInformation").GetComponent<TextMeshProUGUI>().text = sourceRemote.Port.ToString();

        }


    }
}
