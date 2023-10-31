using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MoveObject : MonoBehaviour
{
    //public PlayerCollider playerCollider;
    //public static string coll_x, coll_y, coll_z, coll_g;
    void Start()
    {
    }
    void Update()
    {
       // UnityEngine.Debug.Log(coll_x);
       // UnityEngine.Debug.Log(coll_g);


        // 左に移動
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(-0.001f, 0.0f, 0.0f);
        }
        // 右に移動
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(0.001f, 0.0f, 0.0f);
        }
        // 前に移動
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(0.0f, 0.0f, 0.001f);
        }
        // 後ろに移動
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(0.0f, 0.0f, -0.001f);
        }
    }
}