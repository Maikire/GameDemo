using UnityEngine;

namespace JumpGame
{
    /// <summary>
    /// LayerIgnore
    /// </summary>
    public class LayerIgnore : MonoBehaviour
    {
        //9：Player  10：Back  12:Wall   
        private void Awake()
        {
            Physics2D.IgnoreLayerCollision(10, 12);
            Physics2D.IgnoreLayerCollision(12, 12);

        }


    }
}