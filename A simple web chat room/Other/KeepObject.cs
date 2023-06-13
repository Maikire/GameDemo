using UnityEngine;

namespace ChatDemo
{
    /// <summary>
    /// 切换场景保留物体
    /// </summary>
    public class KeepObject : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }


    }
}

