using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    /// <summary>
    /// 忽略层级
    /// </summary>
    public class LayerIgnore : MonoBehaviour
    {
        //6：House  8：Enemy  9：Player  10：Back  11:Bullet
        private void Start()
        {
            Physics2D.IgnoreLayerCollision(9, 6);

            Physics2D.IgnoreLayerCollision(8, 8);
            Physics2D.IgnoreLayerCollision(8, 9);
            Physics2D.IgnoreLayerCollision(8, 10);

            Physics2D.IgnoreLayerCollision(11, 6);
            Physics2D.IgnoreLayerCollision(11, 9);
            Physics2D.IgnoreLayerCollision(11, 10);
            Physics2D.IgnoreLayerCollision(11, 11);
        }

    }
}
