using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ChatDemo
{
    /// <summary>
    /// LogInButton
    /// </summary>
    public class LogInButtons : MonoBehaviour
    {
        /// <summary>
        /// LogInType
        /// </summary>
        private enum LogInType
        {
            /// <summary>
            /// 客户端
            /// </summary>
            Client,

            /// <summary>
            /// 服务端
            /// </summary>
            Sever,
        }

        [Header("Client")]
        [Tooltip("客户端登录")]
        public Button Client;
        [Tooltip("ClientContent")]
        public GameObject ClientContent;
        [Tooltip("ClientImage")]
        private Image ClientImage;

        [Header("Sever")]
        [Tooltip("服务端登录")]
        public Button Sever;
        [Tooltip("SeverContent")]
        public GameObject SeverContent;
        [Tooltip("SeverImage")]
        private Image SeverImage;

        [Header("Enter")]
        [Tooltip("登录按钮")]
        public Button Enter;

        /// <summary>
        /// LogInType
        /// </summary>
        private LogInType LogType;

        private void Awake()
        {
            //考虑到遍历物体的性能开销，所以不用代码找物体
            //Client = this.transform.FindChildByName("Client").GetComponent<Button>();
            //ClientContent = this.transform.FindChildByName("ClientContent").gameObject;
            //Sever = this.transform.FindChildByName("Sever").GetComponent<Button>();
            //SeverContent = this.transform.FindChildByName("SeverContent").gameObject;
            //Enter = this.transform.FindChildByName("Enter").GetComponent<Button>();

            ClientImage = Client.GetComponent<Image>();
            SeverImage = Sever.GetComponent<Image>();
        }

        private void Start()
        {
            LogType = LogInType.Client;
            Client.onClick.AddListener(OnClientButtonClick);
            Sever.onClick.AddListener(OnServerButtonClick);
            Enter.onClick.AddListener(OnEnterButtonClick);
        }

        /// <summary>
        /// OnClientButtonClick
        /// </summary>
        private void OnClientButtonClick()
        {
            ClientImage.color = Color.red;
            ClientContent.SetActive(true);

            SeverImage.color = Color.white;
            SeverContent.SetActive(false);

            LogType = LogInType.Client;
        }

        /// <summary>
        /// OnServerButtonClick
        /// </summary>
        private void OnServerButtonClick()
        {
            ClientImage.color = Color.white;
            ClientContent.SetActive(false);

            SeverImage.color = Color.red;
            SeverContent.SetActive(true);

            LogType = LogInType.Sever;
        }

        /// <summary>
        /// OnEnterButtonClick
        /// </summary>
        private void OnEnterButtonClick()
        {
            switch (LogType)
            {
                case LogInType.Client:
                    GoClient();
                    break;

                case LogInType.Sever:
                    GoServer();
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// GoClient
        /// </summary>
        private void GoClient()
        {
            LogInInput logInInput = ClientContent.GetComponent<LogInInput>();
            ClientUdpNetService.Instance.Initialize(logInInput.Content);

            SeverUdpNetService.Instance.enabled = false;

            SceneManager.LoadScene("ChatDemoClient", LoadSceneMode.Single);
        }

        /// <summary>
        /// GoServer
        /// </summary>
        private void GoServer()
        {
            LogInInput logInInput = SeverContent.GetComponent<LogInInput>();
            SeverUdpNetService.Instance.Initialize(logInInput.Content);

            ClientUdpNetService.Instance.enabled = false;

            SceneManager.LoadScene("ChatDemoSever", LoadSceneMode.Single);
        }


    }
}
