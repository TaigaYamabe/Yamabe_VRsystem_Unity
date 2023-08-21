using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using System.IO;
using System.Text;

public class ScaleChange : MonoBehaviour
{
    private GameObject sphereObject;
    //private GameObject child0;
    //private GameObject child1;
    //private GameObject child2;
    //private GameObject child3;
    //private GameObject child4;
    //private GameObject child5;
    //private GameObject child6;
    //private GameObject child7;
    //private GameObject child8;
    //private GameObject child9;
    //private GameObject child10;
    //private GameObject child11;
    //private GameObject child12;
    //private GameObject child13;
    //private GameObject child14;
    //private GameObject child15;
    //private GameObject child16;
    //private GameObject child17;
    //private GameObject child18;
    //private GameObject child19;
    //private GameObject child20;
    CubeSpawner cubeSpawner;
    public static int time;

    //var filePath = @"C:/Users/t_yam/Yamabe_VRSystem(Visual Studio)/EH_FieldMap.TXT";
    // �ǂݍ��񂾃f�[�^���i�[����List<List<string>>
    // Start is called before the first frame update
    float[] array = new float[] {
            (float)0.000000E+00,
            (float)1.104418E-02,
            (float)1.583923E-01,
            (float)1.196097E+00,
            (float)9.864443E-01,
           (float)-1.338088E+00,
           (float)-1.313811E+00,
            (float)1.172797E+00,
            (float)1.824842E+00,
           (float)-1.949650E-01,
           (float)-1.937554E+00,
           (float)-1.568972E+00,
            (float)8.979749E-01,
            (float)2.067897E+00,
            (float)1.254111E+00,
           (float)-1.176090E+00,
          (float)-2.088096E+00,
           (float)-1.276456E+00,
           (float)-1.659115E-01,
           (float)-1.156984E-02,
          (float)-4.745384E-05
        };
    float[] array_s = new float[21];
    private List<List<string>> LoadFileData = new List<List<string>>();




    void Start()
    {
        var filePath = "Assets/Assets/txt/�d��.txt";
        // �ǂݍ��񂾃f�[�^���i�[����List<List<string>>
     //   List<List<string>> LoadFileData = new List<List<string>>();


        string line;
        String[] readLine;

        // �ǂݍ��ރt�@�C�������݂��邩�m�F����
        if (File.Exists(filePath))
        // �t�@�C�������݂���Ƃ�������������
        {   /* StreamReader���g�����ǂݍ��ݏ��� */
            using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
            {

                // �t�@�C���̖��[�ɒB����܂ŁA�t�@�C������s��ǂݍ���ŁA�\�����܂��B
                while (!sr.EndOfStream)
                {
                    // ������List<string> ���[�v�̓�����List<string>���쐬����̂��|�C���g
                    List<string> stringList = new List<string>();

                    line = sr.ReadLine();
                    // �ǂݍ���1�s��String[]�Ɋi�[
                    readLine = line.Split();
                    // ������List<string>��String[]�̓��e���i�[
                    stringList.AddRange(readLine);
                    // List<List<string>>�ɑ�������邽�߂Ɏg�p����List<List<string>>�ɓ�����List<string>�̓��e��ǉ�
                    LoadFileData.Add(stringList);

                    // �z��㏑���̋����������ł��Ă��Ȃ��̂ŁAreadLine���N���A
                    readLine = null;
                }
            }


        }
        
        sphereObject = GameObject.Find("CubeSpawner");
        cubeSpawner = sphereObject.GetComponent<CubeSpawner>();
       
    }

    // Update is called once per frame
    void Update()
    {
        time = cubeSpawner.iTimeCounter;
       
        for (int i = 0; i < 70; i++)
        {
            for (int k = 0; k < 7; k++)
            {
                int s = (k + 1) / 2;
                transform.GetChild(i).gameObject.GetComponent<Transform>().transform.GetChild(k).gameObject.transform.localScale = new Vector3(float.Parse(LoadFileData[70 * s + i][2]) * (float)Math.Cos(time * 0.0874)*(-1.5f), float.Parse(LoadFileData[70 * s + i][2]) * (float)Math.Cos(time * 0.0874)*(-1.5f), float.Parse(LoadFileData[70 * s + i][2]) * (float)Math.Cos(time * 0.0874) * (-1.5f));

                //transform.GetChild(i).gameObject.GetComponent<Transform>().transform.GetChild(k).gameObject.transform.localScale = new Vector3((float)Math.Cos(time * 0.0874), (float)Math.Cos(time * 0.0874), 1);
            }
        }
        
    }
}
