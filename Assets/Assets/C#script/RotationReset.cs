using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RotationReset : MonoBehaviour
{
    float local_x = 0.0f;
    float local_y = 0.0f;
    float local_z = 0.0f;
    public float send_x = 0.0f;
    public float send_y = 0.0f;
    public float send_z = 0.0f;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {   Transform myTransform= this.transform;
        Vector3 localRotate = myTransform.eulerAngles;

        if (Input.GetKey(KeyCode.Z)){
            local_x = localRotate.x;
            local_y = localRotate.y - 90.0f;
            local_z = localRotate.z;
        }
        //if (gameObject.transform.localEulerAngles.z <= 30)//ƒIƒCƒ‰[Šp‚ðŽæ“¾‚µ‚Ä”äŠr
        //{
        //    transform.Rotate(new Vector3(0, 0, 0.5f));//1ƒtƒŒ[ƒ€‚²‚Æ‚É0.5“x‰ñ“]
        //}

        localRotate.x -= local_x;
        localRotate.y -= local_y;
        localRotate.z -= local_z;
        send_x = localRotate.x;
        send_y = localRotate.y;
        send_z = localRotate.z;
        myTransform.localEulerAngles = localRotate;

        //Debug.Log(myTransform.localEulerAngles);
       // Debug.Log(local_x);
       // Debug.Log(local_y);
       // Debug.Log(local_z);
    }
}
