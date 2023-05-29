using UnityEngine;

namespace JumpGame
{
    /// <summary>
    /// BackController
    /// </summary>
    public class BackController : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            collision.transform.GetComponent<PlayerController>().IsGameOver = true;
        }

    }
}