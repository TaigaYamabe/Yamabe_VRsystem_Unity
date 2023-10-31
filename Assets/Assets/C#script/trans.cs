using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;
using System;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Diagnostics;
//using System.Diagnostics;

public class trans : MonoBehaviour
{
    public float shade = 0.5f;
    public float width = 0.1f;
    [SerializeField] CubeSpawner cubeSpawner;
    public static int time;
    public static int time_sin;
    private List<List<string>> LoadFileData = new List<List<string>>();
    private float a = 0;
    private float a_sin = 0;
    private float[] a_n = new float[70];
    private float a_cos = 0;
    private float max_a = 0;
    private float min_a = 0;
    private float[,] positions = new float[70,3];
    // Start is called before the first frame update
    void Start()
    {
        var filePath = "Assets/Assets/txt/電場.txt";
        // 読み込んだデータを格納するList<List<string>>
        //   List<List<string>> LoadFileData = new List<List<string>>();


        string line;
        String[] readLine;

        // 読み込むファイルが存在するか確認する
        if (File.Exists(filePath))
        // ファイルが存在するときだけ処理する
        {   /* StreamReaderを使った読み込み処理 */
            using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
            {

                // ファイルの末端に達するまで、ファイルから行を読み込んで、表示します。
                while (!sr.EndOfStream)
                {
                    // 内側のList<string> ループの内側でList<string>を作成するのがポイント
                    List<string> stringList = new List<string>();

                    line = sr.ReadLine();
                    // 読み込んだ1行をString[]に格納
                    readLine = line.Split();
                    // 内側のList<string>にString[]の内容を格納
                    stringList.AddRange(readLine);
                    // List<List<string>>に代入さうるために使用するList<List<string>>に内側のList<string>の内容を追加
                    LoadFileData.Add(stringList);

                    // 配列上書きの挙動が理解できていないので、readLineをクリア
                    readLine = null;
                }
            }


        }
        for (int i = 0; i < 70; i++)
        {
            
            positions[i, 0] = transform.GetChild(i).gameObject.GetComponent<Transform>().transform.position.x;
            positions[i, 1] = transform.GetChild(i).gameObject.GetComponent<Transform>().transform.position.y;
            positions[i, 2] = transform.GetChild(i).gameObject.GetComponent<Transform>().transform.position.z;


            if (max_a< Math.Abs(float.Parse(LoadFileData[i][2]))) {
                max_a = Math.Abs(float.Parse(LoadFileData[i][2]));
            }
            if (min_a > Math.Abs(float.Parse(LoadFileData[i][2])))
            {
                min_a = Math.Abs(float.Parse(LoadFileData[i][2]));
            }
        }


        //// 現在使用されているマテリアルを取得
        //Material mat = this.GetComponent<Renderer>().material;
        //// マテリアルの色設定に赤色を設定
        //mat.color = new Color(1.0f, 0.0f, 0.0f, 0.01f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time = cubeSpawner.iTimeCounter;
        if (time == 0)
        {
            for (int i = 0; i < 70; i++)
            {
                a_n[i] = 0;
            }
        }
    
        //赤と青--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        for (int i = 0; i < 70; i++)
        {

            time_sin = time-7;
            //time_sin = time;
            a = float.Parse(LoadFileData[i][2]) * (float)Math.Cos(time_sin * 0.0874);
            a_sin =float.Parse(LoadFileData[i][2]) * (float)Math.Cos(time * 0.0874) * (float)Math.Cos(time * 0.0874);
            if((float)Math.Cos(time * 0.0874) < 0)
            {
                a_sin = -a_sin;
            }
            //   a_n[i] = -a / max_a;
            if(time_sin >= 0)
            {
                a_n[i] += -a / (10 * max_a);
            }
            a_cos = (float)Math.Acos(a_n[i]);
            //Debug.Log(a_n);


            //円柱伸縮運動--------------------------------------------------------------------------------------------------------------------------------------------------
            //transform.GetChild(i).gameObject.GetComponent<Transform>().transform.localScale = new Vector3(Math.Abs(1.2f * a_n), 1, Math.Abs(1.2f * a_n));
            //--------------------------------------------------------------------------------------------------------------------------------------------------------------
            //円柱縦波移動--------------------------------------------------------------------------------------------------------------------------------------------------
            //transform.GetChild(i).gameObject.GetComponent<Transform>().transform.position = new Vector3(positions[i, 0], positions[i, 1], positions[i, 2] + a_n[i] * (0.0０３f));
            //修正版
            transform.GetChild(i).gameObject.GetComponent<Transform>().transform.position = new Vector3(positions[i, 0], positions[i, 1], positions[i, 2]+a_n[i]*(0.01f));
            //--------------------------------------------------------------------------------------------------------------------------------------------------------------

            //色変化
            if (a >= 0)
            {
                transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 1.0f, (a / max_a) * shade);
            }
            else
            {
                a = -a;
                transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.0f, (a / max_a) * shade);
            }

            //色変化なし
            //if (a >= 0)
            //{
            //    transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 1.0f, shade);
            //}
            //else
            //{
            //    a = -a;
            //    transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 1.0f, shade);
            //}
            
            transform.GetChild(i).gameObject.GetComponent<Transform>().transform.localScale = new Vector3(1.0f, width, 1.0f);
            
            
            //for (int k = 0; k < 7; k++)
            //{
            //    int s = (k + 1) / 2;
            //    transform.GetChild(i).gameObject.GetComponent<Transform>().transform.GetChild(k).gameObject.transform.localScale = new Vector3(float.Parse(LoadFileData[70 * s + i][2]) * (float)Math.Cos(time * 0.0874) * (-1.5f), float.Parse(LoadFileData[70 * s + i][2]) * (float)Math.Cos(time * 0.0874) * (-1.5f), float.Parse(LoadFileData[70 * s + i][2]) * (float)Math.Cos(time * 0.0874) * (-1.5f));

            //    //transform.GetChild(i).gameObject.GetComponent<Transform>().transform.GetChild(k).gameObject.transform.localScale = new Vector3((float)Math.Cos(time * 0.0874), (float)Math.Cos(time * 0.0874), 1);
            //}
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //ヒートマップ風
        //for (int i = 0; i < 70; i++)
        //{
        //    a = float.Parse(LoadFileData[i][2]) * (float)Math.Cos(time * 0.0874);
        //    //a = float.Parse(LoadFileData[i][2]) * (float)Math.Cos(time * 0.04);
        //    a_n[i] = -a / max_a;
        //    a_cos = (float)Math.Acos(a_n[i]);
        //    //Debug.Log(a_n[i] );


        //    //円柱伸縮運動--------------------------------------------------------------------------------------------------------------------------------------------------
        //    transform.GetChild(i).gameObject.GetComponent<Transform>().transform.localScale = new Vector3(Math.Abs(1.2f * a_n[i] ), 1, Math.Abs(1.2f * a_n[i]));
        //    //--------------------------------------------------------------------------------------------------------------------------------------------------------------


        //    if (a_n[i]  >= 0.5f)
        //    {
        //        transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f - 2 * (a_n[i]  - 0.5f), 0.0f, shade);
        //    }
        //    else if (a_n[i]  >= 0.0f)
        //    {
        //        transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = new Color(2 * a_n[i] , 1.0f, 0.0f, shade);
        //    }
        //    else if (a_n[i]  >= -0.5f)
        //    {
        //        transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = new Color(0.0f, 1.0f, 1.0f - 2 * (a_n[i]  + 0.5f), shade);
        //    }
        //    else if (a_n[i]  >= -1.0f)
        //    {
        //        transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = new Color(0.0f, 2 * (a_n[i]  + 1.0f), 1.0f, shade);
        //    }

        //}
    }
}
