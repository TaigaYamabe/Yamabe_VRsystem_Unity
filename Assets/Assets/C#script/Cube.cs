using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour, IPooledObject
{
    public float upForce = 1f;
    public float sideForce = .1f;

    // Start is called before the first frame update
    public void OnObjectSpawn()
    {
        float xForce = UnityEngine.Random.Range(-sideForce, sideForce);
        float yForce = UnityEngine.Random.Range(upForce / 2f, upForce);
        float zForce = UnityEngine.Random.Range(-sideForce, sideForce);

        //Vector3 force = new Vector3(xForce, yForce, zForce);

        //float t = 180 / (float)System.Math.PI;
        //GetComponent<Rigidbody>().transform.eulerAngles = new Vector3((float)System.Math.Atan(zForce / yForce) * t, 0f, -(float)System.Math.Atan(xForce / yForce) * t);

        //GetComponent<Rigidbody>().velocity = force;

        //Transform myTransform = this.transform;

        //// ワールド座標を基準に、回転を取得
        //Vector3 worldAngle = myTransform.eulerAngles;
        //worldAngle.x = (float)System.Math.Atan(zForce / yForce) * t; // ワールド座標を基準に、x軸を軸にした回転を10度に変更
        //worldAngle.y = 0f; // ワールド座標を基準に、y軸を軸にした回転を10度に変更
        //worldAngle.z = -(float)System.Math.Atan(xForce / yForce) * t; // ワールド座標を基準に、z軸を軸にした回転を10度に変更
        //myTransform.eulerAngles = worldAngle; // 回転角度を設定

    }
}
    





