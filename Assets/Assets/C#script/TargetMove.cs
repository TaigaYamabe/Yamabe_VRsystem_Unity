using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour
{
    public float target_x = 0.0f;
    public float target_y = 0.0f;
    public float target_z = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        target_x = this.transform.position.x;
        target_y = this.transform.position.y;
        target_z = this.transform.position.z;
    }
}

