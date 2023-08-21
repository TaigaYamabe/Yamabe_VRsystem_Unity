using System.Net.NetworkInformation;
using System.Numerics;
using System.Collections.Specialized;
using System.Security.AccessControl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


public class InputFile //: MonoBehaviour
{
    public class clsInputFile
    {
        public List<clsTimeSlice> timeSlice;
    }
    public class clsTimeSlice
    {
        public List<clsParticlePosition> particlePosition;
    }
    public class clsParticlePosition
    {
        public UnityEngine.Vector3 Pos;
        public float G;
        public float ID;
        public byte color;
    }   
    internal clsInputFile inputFile;
    private string tmpString = String.Empty;
 
    // Start is called before the first frame update
    public void Read()
    {
        inputFile = new clsInputFile();
        inputFile.timeSlice = new List<clsTimeSlice>();
     
        string filePath = "Assets/Assets/txt/Buncher_002k_w_ID.txt";
        UnityEngine.Vector3 maxParticlePos = new UnityEngine.Vector3();
        UnityEngine.Vector3 minParticlePos = new UnityEngine.Vector3();
        System.Drawing.PointF maxminG = new System.Drawing.PointF();

		//Time slice数の取得
        using (StreamReader streamReader = new StreamReader (filePath, Encoding.UTF8)) {

            //粒子の座標(max,min)を取得する
            while(!streamReader.EndOfStream) {
				tmpString = streamReader.ReadLine ();
                //UnityEngine.Debug.Log("tmpString = " + tmpString);
                if(tmpString.Trim() != "")//trimして空白じゃなかったら
                {
                    if(tmpString.Contains("time          ")) //文にtimeが含まれていたら
                    {
                        inputFile.timeSlice.Add(new clsTimeSlice());//listであるtimeSliceの末尾にインスタンスを生成
                        inputFile.timeSlice[inputFile.timeSlice.Count - 1].particlePosition = new List<clsParticlePosition>();//inputFile.timeSlice[inputFile.timeSlice.Count - 1](timeSliceの末尾)にList作る
                        //UnityEngine.Debug.Log("inputFile.timeSlice.Count = " + inputFile.timeSlice.Count.ToString());
                    }
                    else
                    {
                        if(inputFile.timeSlice.Count > 0)
                        {
                            int indexTimeSlice = inputFile.timeSlice.Count - 1;
                                    
                            inputFile.timeSlice[indexTimeSlice].particlePosition.Add(new clsParticlePosition());//listであるparticlePositionの末尾にインスタンスを生成
                            int indexParticle = inputFile.timeSlice[indexTimeSlice].particlePosition.Count - 1;

                            inputFile.timeSlice[indexTimeSlice].particlePosition[indexParticle] = new clsParticlePosition();//listであるparticlePositionの末尾にインスタンスを生成
                            string[] item = tmpString.Split(' ',StringSplitOptions.RemoveEmptyEntries);//空白で4つに分割
                            //UnityEngine.Debug.Log("tmpString = " + tmpString + " :[0] = "+ item[0]+ " :[1] = "+ item[1]+ " :[2] = "+ item[2]+ " :[3] = "+ item[3]);
                            if(item[0] != "x")
                            {
                                inputFile.timeSlice[indexTimeSlice].particlePosition[indexParticle].Pos.x = float.Parse(item[0].Trim());
                                inputFile.timeSlice[indexTimeSlice].particlePosition[indexParticle].Pos.y = float.Parse(item[1].Trim());
                                inputFile.timeSlice[indexTimeSlice].particlePosition[indexParticle].Pos.z = float.Parse(item[2].Trim())-0.01f;
                                inputFile.timeSlice[indexTimeSlice].particlePosition[indexParticle].G = float.Parse(item[3].Trim());
                                inputFile.timeSlice[indexTimeSlice].particlePosition[indexParticle].ID = float.Parse(item[4].Trim());


                                //粒子の座標(max,min)を取得する
                                checkPosition(inputFile.timeSlice[indexTimeSlice].particlePosition[indexParticle].Pos,
                                            ref maxParticlePos ,
                                            ref minParticlePos ,
                                            inputFile.timeSlice[indexTimeSlice].particlePosition[indexParticle].G, 
                                            ref maxminG);
                            }
                        }
                    }
                }
            }
		}
        UnityEngine.Debug.Log("InputFile read :Time Slice = " + inputFile.timeSlice.Count.ToString());
        
        UnityEngine.Debug.Log(minParticlePos.x.ToString() + " <= X <= " + maxParticlePos.x.ToString());
        UnityEngine.Debug.Log(minParticlePos.y.ToString() + " <= Y <= " + maxParticlePos.y.ToString());
        UnityEngine.Debug.Log(minParticlePos.z.ToString() + " <= Z <= " + maxParticlePos.z.ToString());
        UnityEngine.Debug.Log(maxminG.X.ToString() + " <= G <= " + maxminG.Y.ToString());

    }

    //粒子の座標(max,min)を取得する


    private void checkPosition(UnityEngine.Vector3 Pos, ref UnityEngine.Vector3 maxParticlePosition, ref UnityEngine.Vector3 minParticlePosition,
                                float G, ref System.Drawing.PointF maxminG)
    {
        if (maxParticlePosition.x < Pos.x){maxParticlePosition.x = Pos.x;}
        if (maxParticlePosition.y < Pos.y){maxParticlePosition.y = Pos.y;}
        if (maxParticlePosition.z < Pos.z){maxParticlePosition.z = Pos.z;}
        
        if (minParticlePosition.x > Pos.x){minParticlePosition.x = Pos.x;}
        if (minParticlePosition.y > Pos.y){minParticlePosition.y = Pos.y;}
        if (minParticlePosition.z > Pos.z){minParticlePosition.z = Pos.z;}     

        if (maxminG.Y < G){maxminG.Y = G;}  
        if (maxminG.X > G){maxminG.X = G;}  
    }



    /*public void ReadE()
    {
        inputFile = new clsInputFile();
        inputFile.timeSlice = new List<clsTimeSlice>();
     
        string filePath = @"C:\Users\tetsu\UnityProjects\Data\OUTSF7.TXT";
        UnityEngine.Vector3 maxParticlePos = new UnityEngine.Vector3();
        UnityEngine.Vector3 minParticlePos = new UnityEngine.Vector3();
        System.Drawing.PointF maxminG = new System.Drawing.PointF();

		//Time slice数の取得
        using (StreamReader streamReader = new StreamReader (filePath, Encoding.UTF8)) {

            //粒子の座標(max,min)を取得する
            while(!streamReader.EndOfStream) {
				tmpString = streamReader.ReadLine ();
                //UnityEngine.Debug.Log("tmpString = " + tmpString);
                if(tmpString.Trim() != "")
                {
                    if(tmpString.Contains("time          ")) 
                    {
                        inputFile.timeSlice.Add(new clsTimeSlice());
                        inputFile.timeSlice[inputFile.timeSlice.Count - 1].particlePosition = new List<clsParticlePosition>();
                        //UnityEngine.Debug.Log("inputFile.timeSlice.Count = " + inputFile.timeSlice.Count.ToString());
                    }
                    else
                    {
                        if(inputFile.timeSlice.Count > 0)
                        {
                            int indexTimeSlice = inputFile.timeSlice.Count - 1;
                                    
                            inputFile.timeSlice[indexTimeSlice].particlePosition.Add(new clsParticlePosition());
                            int indexParticle = inputFile.timeSlice[indexTimeSlice].particlePosition.Count - 1;

                            inputFile.timeSlice[indexTimeSlice].particlePosition[indexParticle] = new clsParticlePosition();
                            string[] item = tmpString.Split(' ',StringSplitOptions.RemoveEmptyEntries);
                            //UnityEngine.Debug.Log("tmpString = " + tmpString + " :[0] = "+ item[0]+ " :[1] = "+ item[1]+ " :[2] = "+ item[2]+ " :[3] = "+ item[3]);
                            if(item[0] != "x")
                            {
                                inputFile.timeSlice[indexTimeSlice].particlePosition[indexParticle].Pos.x = float.Parse(item[0].Trim());
                                inputFile.timeSlice[indexTimeSlice].particlePosition[indexParticle].Pos.y = float.Parse(item[1].Trim());
                                inputFile.timeSlice[indexTimeSlice].particlePosition[indexParticle].Pos.z = float.Parse(item[2].Trim());
                                inputFile.timeSlice[indexTimeSlice].particlePosition[indexParticle].G = float.Parse(item[3].Trim());

                                //粒子の座標(max,min)を取得する
                                checkPosition(inputFile.timeSlice[indexTimeSlice].particlePosition[indexParticle].Pos,
                                            ref maxParticlePos ,
                                            ref minParticlePos ,
                                            inputFile.timeSlice[indexTimeSlice].particlePosition[indexParticle].G, 
                                            ref maxminG);
                            }
                        }
                    }
                }
            }
		}
        UnityEngine.Debug.Log("InputFile read :Time Slice = " + inputFile.timeSlice.Count.ToString());
        
        UnityEngine.Debug.Log(minParticlePos.x.ToString() + " <= X <= " + maxParticlePos.x.ToString());
        UnityEngine.Debug.Log(minParticlePos.y.ToString() + " <= Y <= " + maxParticlePos.y.ToString());
        UnityEngine.Debug.Log(minParticlePos.z.ToString() + " <= Z <= " + maxParticlePos.z.ToString());
        UnityEngine.Debug.Log(maxminG.X.ToString() + " <= G <= " + maxminG.Y.ToString());

    }*/
    // Update is called once per frame
    void Update()
    {
        
    }
}
