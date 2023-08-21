using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime;
//using UniRx;

public class CameraMoveSmooth : MonoBehaviour
{
    /// <summary>
    /// カメラの移動先
    /// (ContextMenuItemによりInspectorからExecuteMoveを実行できる)
    /// </summary>
    //[SerializeField, ContextMenuItem("Move", "ExecuteMove")]
    //private Transform p_TargetTransform;

	[SerializeField]
    public GameObject cam;

    /// <summary>
    /// カメラの移動開始地点
    /// </summary>
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    /// <summary>
    /// カメラの移動目標地点
    /// </summary>
    private Vector3 _endPosition;
    private Quaternion _endRotation;


    [SerializeField]
    private float LEAP_TIME = 30.0f;

 
    // Time when the movement started.
    private float startTime;

    private bool isStartMoving = false;

    //public CameraMoveSmooth()
    private void Start()
    {
        // カメラの移動開始地点を保存する
        _startPosition = Camera.main.transform.position;
        _startRotation = Camera.main.transform.rotation;

        /*
        UnityEngine.Debug.Log("CameraMoveSmooth Start X=" + _startPosition.x.ToString() 
                                                + ", Y=" + _startPosition.y.ToString() 
                                                + ", Z=" + _startPosition.y.ToString());
        
        _endPosition = p_TargetTransform.position;
        _endRotation = p_TargetTransform.rotation;

        // Keep a note of the time the movement started.
        startTime = Time.time;
        */

    }

    // Follows the target position like with a spring
    public void FixedUpdate()
    {
        if (isStartMoving)
        {
            float t = Mathf.Min ((Time.time - startTime) / LEAP_TIME, 1f);
            float leapt = (t * t) * (3f - (2f * t));
            cam.transform.position = Vector3.Lerp (_startPosition, _endPosition, leapt);

            if (t == 1){
                isStartMoving = false;
            }
        }
    }

    public void Update()
    {
        
        //Camera Position Reset
        if (Input.GetKey (KeyCode.Space))
        {
            // カメラの移動開始地点を保存する
            _startPosition = Camera.main.transform.position;
            _startRotation = Camera.main.transform.rotation;

            UnityEngine.Debug.Log("CameraMoveSmooth Start X=" + _startPosition.x.ToString() 
                                                + ", Y=" + _startPosition.y.ToString() 
                                                + ", Z=" + _startPosition.y.ToString());

            startTime = Time.time;
            isStartMoving = true;
        }
        //Camera Position Reset
        if (Input.GetKey (KeyCode.Alpha0))
        {

            Vector3 Pos = new Vector3();
            Pos.x = 0f;
            Pos.y = 1f;
            Pos.z = 0.2f;
            cam.transform.position = Pos;
        
            cam.transform.rotation = Quaternion.Euler(90, -90, 0);;

            Camera.main.fieldOfView = 15f;

            UnityEngine.Debug.Log("Camera position reset. Pos = (" + Pos.x.ToString() 
                                    + ", " + Pos.y.ToString() 
                                    + ", " + Pos.z.ToString() + ")");

        }
        //Camera Position Reset
        if (Input.GetKey (KeyCode.Alpha1))
        {
            Vector3 Pos = new Vector3();
            Pos.x = 0f;
            Pos.y = 1f;
            Pos.z = -1f;
            cam.transform.position = Pos;
        
            cam.transform.rotation = Quaternion.Euler(40, 0, 90);;

            Camera.main.fieldOfView = 8f;

            UnityEngine.Debug.Log("Camera position P1. Pos = (" + Pos.x.ToString() 
                        + ", " + Pos.y.ToString() 
                        + ", " + Pos.z.ToString() + ")");
        }


    }
}
