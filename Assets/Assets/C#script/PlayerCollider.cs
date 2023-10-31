using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Diagnostics;
using System.Collections.Specialized;

public class PlayerCollider : MonoBehaviour
{
    G_script script;
    // public static PlayerCollider instance;
    public Vector3 colider_rot = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 colider_vec = new Vector3(0.0f, 0.0f, 0.0f);
    public float colider_g = 0.0f;
    // 開始処理
    //void Start()
    //{
    //    colider_rot = new Vector3(0.0f, 0.0f, 0.0f);
    //    colider_g = 0.0f;
    //    // Colliderをキャッシュ
    //    //collider = GetComponent<SphereCollider>();
    //}

    //// 更新処理
    //void Update()
    //{
    //    UnityEngine.Debug.Log(colider_rot);
    //    UnityEngine.Debug.Log(colider_g);
    //}

    private void OnTriggerEnter(Collider other)
    {
        // もし接触している（重なっている）相手オブジェクトの名前が"Cube"ならば
        if (other.tag == "Enemy")
        {
            script = other.GetComponent<G_script>();
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            // キャラクター正面から見た衝突位置の角度を取得
            Vector3 diff = 1000 * transform.position - 1000 * other.transform.position;
            // コンソールに"接触中！"と表示
            //UnityEngine.Debug.Log(diff.normalized);
            //UnityEngine.Debug.Log(script.G);
            colider_rot = diff.normalized;
            colider_vec = script.V.normalized;
            colider_g = script.G;
        }
        UnityEngine.Debug.Log(colider_vec);
        //UnityEngine.Debug.Log(colider_g);

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            colider_rot = new Vector3(0.0f, 0.0f, 0.0f);
            colider_vec = new Vector3(0.0f, 0.0f, 0.0f);
            colider_g = 0.0f;
        }
    }

    // ゲームオブジェクト同士が接触している（重なっている）間、持続的に実行

}
