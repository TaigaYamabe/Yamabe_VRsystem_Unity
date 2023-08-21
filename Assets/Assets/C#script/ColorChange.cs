using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorChange : MonoBehaviour
{

    void Start()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            

                CancelInvoke();
                GetComponent<Renderer>().material.color = Color.red;
                Invoke("method1", 2.0f);
            }

        }
    
    
    void method1()
    {

        GetComponent<Renderer>().material.color = Color.white;

    }
    

}