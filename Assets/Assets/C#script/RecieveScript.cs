using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RecieveScript : MonoBehaviour
{

    GameObject rotationBox;
    RotationReset script;

    // Start is called before the first frame update
    void Start()
    {
        rotationBox = GameObject.Find("RightHandAnchor");
        script = rotationBox.GetComponent<RotationReset>();
    }

    // Update is called once per frame
    void Update()
    {
        float rotation_x = script.send_x;
        float rotation_y = script.send_y;
        float rotation_z = script.send_z;
        this.transform.rotation = Quaternion.Euler(rotation_x, rotation_y, rotation_z);
        float c1 = (float)Math.Cos(rotation_z * (Math.PI / 180.0f));
        float c2 = (float)Math.Cos(rotation_x * (Math.PI / 180.0f));
        float c3 = (float)Math.Cos(rotation_y * (Math.PI / 180.0f));
        float s1 = (float)Math.Sin(rotation_z * (Math.PI / 180.0f));
        float s2 = (float)Math.Sin(rotation_x * (Math.PI / 180.0f));
        float s3 = (float)Math.Sin(rotation_y * (Math.PI / 180.0f));
        float ex = c1 * c3 - s1 * s2 * s3;
        float ey = c3 * s1 + c1 * s2 * s3;
        float ez = -c2*s3;
        //Vector3 v = new Vector3(ex, ey, ez);
       //print(v);
    }
}
