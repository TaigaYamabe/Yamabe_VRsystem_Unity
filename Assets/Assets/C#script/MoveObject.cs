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


        // ���Ɉړ�
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(-0.001f, 0.0f, 0.0f);
        }
        // �E�Ɉړ�
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(0.001f, 0.0f, 0.0f);
        }
        // �O�Ɉړ�
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(0.0f, 0.0f, 0.001f);
        }
        // ���Ɉړ�
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(0.0f, 0.0f, -0.001f);
        }
    }
}