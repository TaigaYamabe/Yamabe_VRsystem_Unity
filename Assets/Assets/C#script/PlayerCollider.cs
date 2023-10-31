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
    // �J�n����
    //void Start()
    //{
    //    colider_rot = new Vector3(0.0f, 0.0f, 0.0f);
    //    colider_g = 0.0f;
    //    // Collider���L���b�V��
    //    //collider = GetComponent<SphereCollider>();
    //}

    //// �X�V����
    //void Update()
    //{
    //    UnityEngine.Debug.Log(colider_rot);
    //    UnityEngine.Debug.Log(colider_g);
    //}

    private void OnTriggerEnter(Collider other)
    {
        // �����ڐG���Ă���i�d�Ȃ��Ă���j����I�u�W�F�N�g�̖��O��"Cube"�Ȃ��
        if (other.tag == "Enemy")
        {
            script = other.GetComponent<G_script>();
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            // �L�����N�^�[���ʂ��猩���Փˈʒu�̊p�x���擾
            Vector3 diff = 1000 * transform.position - 1000 * other.transform.position;
            // �R���\�[����"�ڐG���I"�ƕ\��
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

    // �Q�[���I�u�W�F�N�g���m���ڐG���Ă���i�d�Ȃ��Ă���j�ԁA�����I�Ɏ��s

}
