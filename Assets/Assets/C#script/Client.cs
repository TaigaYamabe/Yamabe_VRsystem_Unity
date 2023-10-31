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
        //�T�[�o�ɑ��M�������f�[�^���Ԃ�����
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



        //�T�[�o�[��IP�A�h���X�ƃ|�[�g�ԍ�
        string ipOrHost = "localhost";
        int port = 12345;

        //TcpClient���쐬���A�T�[�o�[�Ɛڑ�
        tcp = new System.Net.Sockets.TcpClient(ipOrHost, port);
        Debug.Log("�T�[�o�[({0}:{1})�Ɛڑ����܂����B" +
            ((System.Net.IPEndPoint)tcp.Client.RemoteEndPoint).Address + "," +
            ((System.Net.IPEndPoint)tcp.Client.RemoteEndPoint).Port + "," +
            ((System.Net.IPEndPoint)tcp.Client.LocalEndPoint).Address + "," +
            ((System.Net.IPEndPoint)tcp.Client.LocalEndPoint).Port);

        //NetworkStream���擾
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


        //����C++�ɑ���f�[�^���Ԃ�����
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

        //�d�q�̏Փˊp�x�����ւ̗͊o��
        //collision_x = playerCollider.colider_rot.x.ToString("f4");
        //collision_y = playerCollider.colider_rot.y.ToString("f4");
        //collision_z = playerCollider.colider_rot.z.ToString("f4");


        //�d�q�̑��x�x�N�g���̗͊o��
        collision_x = playerCollider.colider_vec.x.ToString("f4");
        collision_y = playerCollider.colider_vec.y.ToString("f4");
        collision_z = playerCollider.colider_vec.z.ToString("f4");






        //�^�C���A�E�g�ݒ�
        ns.ReadTimeout = 1000;
        ns.WriteTimeout = 1000;

        //�T�[�o�[�Ƀf�[�^�𑗐M
        //�������Byte�^�z��ɕϊ�
        System.Text.Encoding enc = System.Text.Encoding.UTF8;
        byte[] sendBytes = enc.GetBytes(pos_x + ',' + pos_y + ',' + pos_z + ',' + time + ',' + rot_x + ',' + rot_y + ',' + rot_z + ',' + collision_x + ',' + collision_y + ',' + collision_z + ',' + g + ',' + sphere_x + ',' + sphere_y + ',' + sphere_z);
        //byte[] sendBytes = enc.GetBytes(pos_x + ',' + pos_y + ',' + pos_z + ',' + time + ',' + rot_x + ',' + rot_y + ',' + rot_z + ',');

        //�f�[�^�𑗐M����
        ns.Write(sendBytes, 0, sendBytes.Length);
        //float time2 = Time.time;
        // sw.Stop();
        //  Debug.Log(sw.Elapsed.Milliseconds);
        //Debug.Log(sendBytes);
        //Debug.Log(pos_x + ',' + pos_y + ',' + pos_z + ',' + time + ',' + v3_x + ',' + v3_y + ',' + v3_z + ',');
        //Console.WriteLine("�v�����ԁF{0}", (double)sw.ElapsedMilliseconds / 1000);

        //�T�[�o�[���瑗��ꂽ�f�[�^����M����
        //����͈�������̎��Ԃ������Ă���B
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        byte[] resBytes = new byte[256];
        int resSize = 256;
        //�f�[�^����M
        resSize = ns.Read(resBytes, 0, resBytes.Length);
        //��M�����f�[�^��~��
        ms.Write(resBytes, 0, resSize);
        //��M�����f�[�^�𕶎���ɕϊ�
       // resMsg = enc.GetString(ms.GetBuffer(), 0, (int)ms.Length);
        ms.Close();
        //������\n���폜
       // resMsg = resMsg.TrimEnd('\n');
        //Debug.Log(resMsg);

        //�X�y�[�X�����ƕ���
        if (Input.GetKey(KeyCode.Space))
        {
            ns.Close();
            tcp.Close();
            Debug.Log("�ؒf���܂����B");
        }
    }
}
