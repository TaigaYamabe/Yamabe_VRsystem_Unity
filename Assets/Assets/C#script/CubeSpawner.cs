//using System.ComponentModel.DataAnnotations.Schema;
//using System.Numerics;
using System.Net.Http.Headers;
//using System.Threading.Tasks.Dataflow;
//using System.Numerics;
//using System.Threading.Tasks.Dataflow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{

    GameObject Electronic;
    GameObject obj;
    //public static CubeSpawner instance;
    //ObjectPooler objectPooler;
    public InputFile InputFile;
    public GameObject cubePrefab;
    public int iTimeCounter = 0;
    public int MaxNum = 300;
    public int Num = 300;
    UnityEngine.Vector3 kero = new UnityEngine.Vector3(0.002f, 0.002f, 0.002f);
    private void Start()
    {
        Electronic = this.gameObject;
        obj = (GameObject)Resources.Load("Prefab");
        for (int i = 0; i <= MaxNum; i++)
        {
            GameObject object2 = (GameObject)Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);
            object2.SetActive(false);
            object2.name = $"{i}";
            object2.transform.parent = Electronic.transform;
        }
        //objectPooler = ObjectPooler.Instance;
        InputFile = new InputFile();
        InputFile.Read();
        UnityEngine.Debug.Log("CubeSpawner Start");
    } 

    //Update is called once per frame
    private void FixedUpdate()
    {
 
        if (InputFile != null)
        {
            if (iTimeCounter <= InputFile.inputFile.timeSlice.Count - 1)
            {

                //---------------------------------------------------------------------------------------------------------------------
                //下のコメントアウト外せば電子出現

                LocateParticle(iTimeCounter);

                //--------------------------------------------------------------------------------------------------------------------------



                //CubeList(iTimeCounter);
                iTimeCounter += 1;

            }
            else
            {
                iTimeCounter = 0;
            }
        }
    }
    public void DestoryIfExist(string name)
    {
        var gameObject = GameObject.Find(name);
        if (gameObject != null)
        {
            GameObject.Destroy(gameObject);
        }
        
    }

    //TimeSlice毎の粒子を表示する
    private void LocateParticle(int indexSlice)
    {
        Electronic = this.gameObject;
        if (indexSlice == 0)
        {
            for (int i = 0; i < InputFile.inputFile.timeSlice[indexSlice].particlePosition.Count; i++)
            {
                for (int index = 0; index <= MaxNum; index++)
                {
                    GameObject children = transform.GetChild(index).gameObject;
                    children.SetActive(false);
                }

                InputFile.clsParticlePosition T = InputFile.inputFile.timeSlice[indexSlice].particlePosition[i];
                //UnityEngine.Debug.Log((int)T.ID);
                if ((int)T.ID <= MaxNum)
                {
                    GameObject child = transform.GetChild((int)T.ID).gameObject;
                    child.SetActive(true);
                    child.transform.position = T.Pos;
                }
            }
        }
        else if (indexSlice >= 1)
        {

            //UnityEngine.Debug.Log(indexSlice);
            for (int i = 0; i < InputFile.inputFile.timeSlice[indexSlice].particlePosition.Count; i++)
            {
                InputFile.clsParticlePosition T = InputFile.inputFile.timeSlice[indexSlice].particlePosition[i];
                for (int k = 0; k < InputFile.inputFile.timeSlice[indexSlice - 1].particlePosition.Count; k++)
                {

                    InputFile.clsParticlePosition S = InputFile.inputFile.timeSlice[indexSlice - 1].particlePosition[k];
                    if (T.ID == S.ID)
                    {
                        if ((int)T.ID <= MaxNum)
                        {
                            GameObject child = transform.GetChild((int)T.ID).gameObject;
                            child.transform.position = T.Pos;
                            //objectPooler.SpawnFromPool("Cube1", T.Pos, UnityEngine.Quaternion.Euler(90f, 0f, 0f), kero, T.ID);
                        }
                        break;
                    }
                    else if (k == InputFile.inputFile.timeSlice[indexSlice - 1].particlePosition.Count - 1)
                    {
                        if ((int)T.ID <= MaxNum)
                        {
                            GameObject child = transform.GetChild((int)T.ID).gameObject;
                            child.SetActive(true);
                            child.transform.position = T.Pos;

                        }
                    }
                }
            }
            for (int i = 0; i < InputFile.inputFile.timeSlice[indexSlice-1].particlePosition.Count; i++)
            {
                InputFile.clsParticlePosition T = InputFile.inputFile.timeSlice[indexSlice-1].particlePosition[i];
                for (int k = 0; k < InputFile.inputFile.timeSlice[indexSlice].particlePosition.Count; k++)
                {

                    InputFile.clsParticlePosition S = InputFile.inputFile.timeSlice[indexSlice].particlePosition[k];
                    if (T.ID == S.ID)
                    {
                        break;
                    }
                    else if (k == InputFile.inputFile.timeSlice[indexSlice].particlePosition.Count - 1)
                    {
                        if ((int)T.ID <= MaxNum)
                        {
                            GameObject child = transform.GetChild((int)T.ID).gameObject;
                            child.SetActive(false);

                            //objectPooler.SpawnFromPool("Cube1", T.Pos, UnityEngine.Quaternion.Euler(90f, 0f, 0f), kero, T.ID);
                            //objectPooler.SpawnFromPool("White", T.Pos, UnityEngine.Quaternion.Euler(90f, 0f, 0f), kero);
                        }
                    }
                }
            }
        }
        //個数指定
        for (int index = 0; index <= MaxNum - Num; index++)
        {
            GameObject children = transform.GetChild(index).gameObject;
            children.SetActive(false);
        }
    }


    //private void LocateParticle2(int indexSlice)
    //{
    //    GameObject prefab = (GameObject)Resources.Load("Prefab");
    //    GameObject objGet;
    //    if (indexSlice == 0)
    //    {
    //        //for(int i=1; i < 2000; i++)
    //        //{
    //        //    objGet = GameObject.Find($"{i}");
    //        //    objGet.SetActive(false);
    //        //}

    //        for (int i = 0; i < objectPooler.InputFile.inputFile.timeSlice[indexSlice].particlePosition.Count; i++)
    //        {

    //            InputFile.clsParticlePosition T = objectPooler.InputFile.inputFile.timeSlice[indexSlice].particlePosition[i];
    //            objGet = GameObject.Find($"{T.ID}");
    //            objGet.transform.position = T.Pos;
    //            //GameObject instance = (GameObject)Instantiate(prefab, T.Pos, Quaternion.identity);
    //            //instance.name = $"{T.ID}";
    //            //objectPooler.SpawnFromPool("Cube1", T.Pos, UnityEngine.Quaternion.Euler(90f, 0f, 0f), kero, T.ID);
    //            //objectPooler.SpawnFromPool("White", T.Pos, UnityEngine.Quaternion.Euler(90f, 0f, 0f), kero);
    //        }

    //    }
    //    else if (indexSlice >= 1)
    //    {

    //        //UnityEngine.Debug.Log(indexSlice);
    //        for (int i = 0; i < objectPooler.InputFile.inputFile.timeSlice[indexSlice].particlePosition.Count; i++)
    //        {
    //            InputFile.clsParticlePosition T = objectPooler.InputFile.inputFile.timeSlice[indexSlice].particlePosition[i];
    //            for (int k = 0; k < objectPooler.InputFile.inputFile.timeSlice[indexSlice - 1].particlePosition.Count; k++)
    //            {

    //                InputFile.clsParticlePosition S = objectPooler.InputFile.inputFile.timeSlice[indexSlice - 1].particlePosition[k];
    //                if (T.ID == S.ID)
    //                {

    //                    objGet = GameObject.Find($"{T.ID}");
    //                    objGet.transform.position = T.Pos;
    //                    //objectPooler.SpawnFromPool("Cube1", T.Pos, UnityEngine.Quaternion.Euler(90f, 0f, 0f), kero, T.ID);
    //                    break;
    //                }
    //                else if (k == objectPooler.InputFile.inputFile.timeSlice[indexSlice - 1].particlePosition.Count - 1)
    //                {
    //                    GameObject instance = (GameObject)Instantiate(prefab, T.Pos, Quaternion.identity);
    //                    instance.name = $"{T.ID}";
    //                    //objectPooler.SpawnFromPool("Cube1", T.Pos, UnityEngine.Quaternion.Euler(90f, 0f, 0f), kero, T.ID);
    //                    //objectPooler.SpawnFromPool("White", T.Pos, UnityEngine.Quaternion.Euler(90f, 0f, 0f), kero);
    //                }
    //            }
    //        }
    //        //for (int i = 0; i < objectPooler.InputFile.inputFile.timeSlice[indexSlice - 1].particlePosition.Count; i++)
    //        //{
    //        //    InputFile.clsParticlePosition T = objectPooler.InputFile.inputFile.timeSlice[indexSlice - 1].particlePosition[i];
    //        //    for (int k = 0; k < objectPooler.InputFile.inputFile.timeSlice[indexSlice].particlePosition.Count; k++)
    //        //    {

    //        //        InputFile.clsParticlePosition S = objectPooler.InputFile.inputFile.timeSlice[indexSlice].particlePosition[k];
    //        //        if (T.ID == S.ID)
    //        //        {
    //        //            break;
    //        //        }
    //        //        else if (k == objectPooler.InputFile.inputFile.timeSlice[indexSlice].particlePosition.Count - 1)
    //        //        {
    //        //            objGet = GameObject.Find($"{T.ID}");
    //        //            Destroy(objGet);

    //        //            //objectPooler.SpawnFromPool("Cube1", T.Pos, UnityEngine.Quaternion.Euler(90f, 0f, 0f), kero, T.ID);
    //        //            //objectPooler.SpawnFromPool("White", T.Pos, UnityEngine.Quaternion.Euler(90f, 0f, 0f), kero);
    //        //        }
    //        //    }
    //        //}
    //    }
    //}

    //private void ParticleList(int indexSlice)
    //{


    //    if (indexSlice == 0)
    //    {
    //        for (int i = 0; i < objectPooler.InputFile.inputFile.timeSlice[indexSlice].particlePosition.Count; i++)
    //        {
    //            InputFile.clsParticlePosition T = objectPooler.InputFile.inputFile.timeSlice[indexSlice].particlePosition[i];

    //            objectPooler.SpawnFromPool("Cube1", T.Pos, UnityEngine.Quaternion.Euler(90f, 0f, 0f), kero, T.ID);
    //            //objectPooler.SpawnFromPool("White", T.Pos, UnityEngine.Quaternion.Euler(90f, 0f, 0f), kero);
    //        }
    //    }
    //    else if (indexSlice >= 1)
    //    {

    //        //UnityEngine.Debug.Log(indexSlice);
    //        for (int i = 0; i < objectPooler.InputFile.inputFile.timeSlice[indexSlice].particlePosition.Count; i++)
    //        {
    //            InputFile.clsParticlePosition T = objectPooler.InputFile.inputFile.timeSlice[indexSlice].particlePosition[i];
    //            for (int k = 0; k < objectPooler.InputFile.inputFile.timeSlice[indexSlice - 1].particlePosition.Count; k++)
    //            {

    //                InputFile.clsParticlePosition S = objectPooler.InputFile.inputFile.timeSlice[indexSlice - 1].particlePosition[k];
    //                if (T.ID == S.ID)
    //                {
    //                    float t = 180 / (float)System.Math.PI;
    //                    UnityEngine.Vector3 CubeVector = T.Pos - S.Pos;
    //                    float mag = CubeVector.magnitude;
    //                    //UnityEngine.Debug.Log(mag);
    //                    UnityEngine.Vector3 scale = new UnityEngine.Vector3(0.003f - mag / 1.5f, 0.003f + mag, 0.003f - mag / 1.5f);

    //                    //objectPooler.SpawnFromPool("Cube", T.Pos, UnityEngine.Quaternion.Euler((float)System.Math.Atan2(CubeVector.z, CubeVector.y) * t, 0f, (float)System.Math.Atan2(CubeVector.x, CubeVector.y) * t), scale);
    //                    objectPooler.SpawnFromPool("Cube1", T.Pos, UnityEngine.Quaternion.Euler((float)System.Math.Atan2(CubeVector.z, CubeVector.y) * t, (float)System.Math.Atan2(CubeVector.x, CubeVector.z) * t, 0f), scale, T.ID);
    //                    //objectPooler.SpawnFromPool("White", T.Pos, UnityEngine.Quaternion.Euler((float)System.Math.Atan2(CubeVector.z, CubeVector.y) * t, (float)System.Math.Atan2(CubeVector.x, CubeVector.z) * t, 0f), scale);

    //                    break;
    //                }
    //                else if (k == objectPooler.InputFile.inputFile.timeSlice[indexSlice - 1].particlePosition.Count - 1)
    //                {
    //                    objectPooler.SpawnFromPool("Cube1", T.Pos, UnityEngine.Quaternion.Euler(90f, 0f, 0f), kero, T.ID);
    //                    //objectPooler.SpawnFromPool("White", T.Pos, UnityEngine.Quaternion.Euler(90f, 0f, 0f), kero);
    //                }
    //            }
    //        }
    //    }

    //}

    //private void CubeList(int indexSlice)
    //{


    //    if (indexSlice == 0)
    //    {
    //        for (int i = 0; i < objectPooler.InputFile.inputFile.timeSlice[indexSlice].particlePosition.Count; i++)
    //        {
    //            InputFile.clsParticlePosition T = objectPooler.InputFile.inputFile.timeSlice[indexSlice].particlePosition[i];

    //            objectPooler.SpawnFromPool("Cube1", T.Pos, UnityEngine.Quaternion.Euler(90f, 0f, 0f), kero, T.ID);
    //        }
    //    }
    //    else if (indexSlice >= 1)
    //    {

    //        //UnityEngine.Debug.Log(indexSlice);
    //        for (int i = 0; i < objectPooler.InputFile.inputFile.timeSlice[indexSlice].particlePosition.Count; i++)
    //        {
    //            InputFile.clsParticlePosition T = objectPooler.InputFile.inputFile.timeSlice[indexSlice].particlePosition[i];
    //            for (int k = 0; k < objectPooler.InputFile.inputFile.timeSlice[indexSlice - 1].particlePosition.Count; k++)
    //            {

    //                InputFile.clsParticlePosition S = objectPooler.InputFile.inputFile.timeSlice[indexSlice - 1].particlePosition[k];
    //                if (T.ID == S.ID)
    //                {
    //                    float t = 180 / (float)System.Math.PI;
    //                    UnityEngine.Vector3 CubeVector = T.Pos - S.Pos;
    //                    float mag = CubeVector.magnitude;
    //                    //UnityEngine.Debug.Log(mag);
    //                    UnityEngine.Vector3 scale = new UnityEngine.Vector3(0.002f + mag / 3, 0.002f - mag / 2, 0.002f + mag / 3);
    //                    if (mag > 0.001)
    //                    {
    //                        //objectPooler.SpawnFromPool("Cube", T.Pos, UnityEngine.Quaternion.Euler((float)System.Math.Atan2(CubeVector.z, CubeVector.y) * t, 0f, (float)System.Math.Atan2(CubeVector.x, CubeVector.y) * t), scale);
    //                        objectPooler.SpawnFromPool("Cube", T.Pos, UnityEngine.Quaternion.Euler((float)System.Math.Atan2(CubeVector.z, CubeVector.y) * t, (float)System.Math.Atan2(CubeVector.x, CubeVector.z) * t, 0f), scale, T.ID);
    //                        //objectPooler.SpawnFromPool("Cube", T.Pos, UnityEngine.Quaternion.Euler(90f, 0f, 0f));
    //                        break;
    //                    }
    //                    else if (mag <= 0.001 && mag > 0.0005)
    //                    {
    //                        objectPooler.SpawnFromPool("Cube1", T.Pos, UnityEngine.Quaternion.Euler((float)System.Math.Atan2(CubeVector.z, CubeVector.y) * t, 0f, 0f), scale, T.ID);
    //                        //objectPooler.SpawnFromPool("Cube1", T.Pos, UnityEngine.Quaternion.Euler((float)System.Math.Atan2(CubeVector.z, CubeVector.y) * t, 0f, (float)System.Math.Atan2(CubeVector.x, CubeVector.y) * t), scale);
    //                        //objectPooler.SpawnFromPool("Cube", T.Pos, UnityEngine.Quaternion.Euler(90f, 0f, 0f));
    //                        break;
    //                    }
    //                    else if (mag <= 0.0005 && mag > 0.0001)
    //                    {
    //                        objectPooler.SpawnFromPool("Cube2", T.Pos, UnityEngine.Quaternion.Euler((float)System.Math.Atan2(CubeVector.z, CubeVector.y) * t, 0f, 0f), scale, T.ID);
    //                        //objectPooler.SpawnFromPool("Cube2", T.Pos, UnityEngine.Quaternion.Euler((float)System.Math.Atan2(CubeVector.z, CubeVector.y) * t, 0f, (float)System.Math.Atan2(CubeVector.x, CubeVector.y) * t), scale);
    //                        //objectPooler.SpawnFromPool("Cube", T.Pos, UnityEngine.Quaternion.Euler(90f, 0f, 0f));
    //                        break;
    //                    }
    //                    else
    //                    {
    //                        objectPooler.SpawnFromPool("Cube3", T.Pos, UnityEngine.Quaternion.Euler((float)System.Math.Atan2(CubeVector.z, CubeVector.y) * t, 0f, 0f), scale, T.ID);
    //                        //objectPooler.SpawnFromPool("Cube3", T.Pos, UnityEngine.Quaternion.Euler((float)System.Math.Atan2(CubeVector.z, CubeVector.y) * t, 0f, (float)System.Math.Atan2(CubeVector.x, CubeVector.y) * t), scale);
    //                        //objectPooler.SpawnFromPool("Cube", T.Pos, UnityEngine.Quaternion.Euler(90f, 0f, 0f));
    //                        break;
    //                    }

    //                }
    //                else if (k == objectPooler.InputFile.inputFile.timeSlice[indexSlice - 1].particlePosition.Count - 1)
    //                {
    //                    objectPooler.SpawnFromPool("Cube1", T.Pos, UnityEngine.Quaternion.Euler(90f, 0f, 0f), kero, T.ID);
    //                }
    //            }
    //        }
    //    }

    //}
}
