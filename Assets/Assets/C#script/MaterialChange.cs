using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using System.IO;
using System.Text;
public class MaterialChange : MonoBehaviour
{
    [SerializeField] Material mat1 = default;
    [SerializeField] Material mat2 = default;
    private GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 70; i++)
        {
            for (int k = 0; k < 7; k++)
            {
                child = transform.GetChild(i).gameObject.GetComponent<Transform>().transform.GetChild(k).gameObject.transform.GetChild(0).gameObject;
                float a = transform.GetChild(i).gameObject.GetComponent<Transform>().transform.GetChild(k).gameObject.transform.localScale.x;
                if (a <= 0)
                {
                    child.GetComponent<MeshRenderer>().material = mat1;
                }
                else
                {
                    child.GetComponent<MeshRenderer>().material = mat2;
                }
             }
        }
    }
}
