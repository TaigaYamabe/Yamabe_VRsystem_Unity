using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Client : MonoBehaviour
{
    public static string sphere_x, sphere_y, sphere_z;
    public static string pos_x, pos_y, pos_z, rot_x, rot_y, rot_z,time;
    public static string collision_x, collision_y, collision_z,g;
    public static string resMsg;
    System.Net.Sockets.NetworkStream ns;
    System.Net.Sockets.TcpClient tcp;
    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
   // [SerializeField] GameObject target1;
    [SerializeField] RotationReset rotationReset;
    [SerializeField] PlayerCollider playerCollider;
    //private GameObject sphereObject;
    [SerializeField] CubeSpawner cubeSpawner;
    [SerializeField] TargetMove targetMove;


    // Use this for initialization
    void Start()
    {
        //サーバに送信したいデータをぶち込む
        pos_x = "1234567";
        pos_y = "1234567";
        pos_z = "1234567";
        time  = "1234567";
        rot_x = "1234567";
        rot_y = "1234567";
        rot_z = "1234567";
        collision_x = "1234567";
        collision_y = "1234567";
        collision_z = "1234567";
        g = "1234567";
        sphere_x = "1234567";
        sphere_y = "1234567";
        sphere_z = "1234567";



        //サーバーのIPアドレスとポート番号
        string ipOrHost = "localhost";
        int port = 12345;

        //TcpClientを作成し、サーバーと接続
        tcp = new System.Net.Sockets.TcpClient(ipOrHost, port);
        Debug.Log("サーバー({0}:{1})と接続しました。" +
            ((System.Net.IPEndPoint)tcp.Client.RemoteEndPoint).Address + "," +
            ((System.Net.IPEndPoint)tcp.Client.RemoteEndPoint).Port + "," +
            ((System.Net.IPEndPoint)tcp.Client.LocalEndPoint).Address + "," +
            ((System.Net.IPEndPoint)tcp.Client.LocalEndPoint).Port);

        //NetworkStreamを取得
        ns = tcp.GetStream();
        //target1 = GameObject.Find("RightHandAnchor");
        //script = target1.GetComponent<RotationReset>();
        //sphereObject = GameObject.Find("CubeSpawner");
        //cubeSpawner = sphereObject.GetComponent<CubeSpawner>();
        //playerCollider = this.GetComponent<PlayerCollider>();
        //Transform myTransform = this.transform;
    }
    // Update is called once per frame
    void Update()
    {
       // sw.Start();
        float rotation_x = rotationReset.send_x;
        float rotation_y = rotationReset.send_y;
        float rotation_z = rotationReset.send_z;

        Transform myTransform = this.transform;
       // Transform handTransform = target1.transform;


        //↓にC++に送るデータをぶち込む
        pos_x = myTransform.position.x.ToString("f4");
        pos_y = myTransform.position.y.ToString("f4");
        pos_z = myTransform.position.z.ToString("f4");
        rot_x = rotation_x.ToString("f4");
        rot_y = rotation_y.ToString("f4");
        rot_z = rotation_z.ToString("f4");
        time = cubeSpawner.iTimeCounter.ToString();
        g = playerCollider.colider_g.ToString("f4");
        sphere_x = targetMove.target_x.ToString("f4");
        sphere_y = targetMove.target_y.ToString("f4");
        sphere_z = targetMove.target_z.ToString("f4");

        //電子の衝突角度方向への力覚提示
        //collision_x = playerCollider.colider_rot.x.ToString("f4");
        //collision_y = playerCollider.colider_rot.y.ToString("f4");
        //collision_z = playerCollider.colider_rot.z.ToString("f4");


        //電子の速度ベクトルの力覚提示
        collision_x = playerCollider.colider_vec.x.ToString("f4");
        collision_y = playerCollider.colider_vec.y.ToString("f4");
        collision_z = playerCollider.colider_vec.z.ToString("f4");






        //タイムアウト設定
        ns.ReadTimeout = 1000;
        ns.WriteTimeout = 1000;

        //サーバーにデータを送信
        //文字列をByte型配列に変換
        System.Text.Encoding enc = System.Text.Encoding.UTF8;
        byte[] sendBytes = enc.GetBytes(pos_x + ',' + pos_y + ',' + pos_z + ',' + time + ',' + rot_x + ',' + rot_y + ',' + rot_z + ',' + collision_x + ',' + collision_y + ',' + collision_z + ',' + g + ',' + sphere_x + ',' + sphere_y + ',' + sphere_z);
        //byte[] sendBytes = enc.GetBytes(pos_x + ',' + pos_y + ',' + pos_z + ',' + time + ',' + rot_x + ',' + rot_y + ',' + rot_z + ',');

        //データを送信する
        ns.Write(sendBytes, 0, sendBytes.Length);
        //float time2 = Time.time;
        // sw.Stop();
        //  Debug.Log(sw.Elapsed.Milliseconds);
        //Debug.Log(sendBytes);
        //Debug.Log(pos_x + ',' + pos_y + ',' + pos_z + ',' + time + ',' + v3_x + ',' + v3_y + ',' + v3_z + ',');
        //Console.WriteLine("計測時間：{0}", (double)sw.ElapsedMilliseconds / 1000);

        //サーバーから送られたデータを受信する
        //今回は一周期分の時間が送られてくる。
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        byte[] resBytes = new byte[256];
        int resSize = 256;
        //データを受信
        resSize = ns.Read(resBytes, 0, resBytes.Length);
        //受信したデータを蓄積
        ms.Write(resBytes, 0, resSize);
        //受信したデータを文字列に変換
       // resMsg = enc.GetString(ms.GetBuffer(), 0, (int)ms.Length);
        ms.Close();
        //末尾の\nを削除
       // resMsg = resMsg.TrimEnd('\n');
        //Debug.Log(resMsg);

        //スペース押すと閉じる
        if (Input.GetKey(KeyCode.Space))
        {
            ns.Close();
            tcp.Close();
            Debug.Log("切断しました。");
        }
    }
}
