using Common;
using System.Net;
using UnityEngine;

namespace ChatDemo
{
    /// <summary>
    /// 创建服务端消息
    /// </summary>
    public class CreateSeverMessage : MonoBehaviour
    {
        [Tooltip("服务端消息预制件")]
        public GameObject SeverMessage;

        public void AddMessage(IPEndPoint sourceRemote)
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                ObjectPool.Instance.RecoverGameObject(this.transform.GetChild(i).gameObject);
            }

            for (int i = 0; i < SeverUdpNetService.Instance.ClientRemotes.Count; i++)
            {
                GameObject obj = ObjectPool.Instance.GetGameObject("SeverMessage", SeverMessage, this.transform);
                obj.GetComponent<SeverMessageManger>().SetMessage(sourceRemote);
            }
        }


    }
}
