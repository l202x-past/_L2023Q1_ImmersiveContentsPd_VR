using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_PlayerLookAt : MonoBehaviour
{
    // 현재 각도
    Vector3 angle;

    // 마우스 감도
    public float sensitivity = 200;

    void Start()
    {
        // 시작할 때 현재 카메라의 각도를 적용함
        angle.y = -Camera.main.transform.eulerAngles.x;
        angle.x = Camera.main.transform.eulerAngles.y;
        angle.z = Camera.main.transform.eulerAngles.z;

        //print(angle);
    }

    void Update()
    {
        // 마우스 입력으로 카메라 회전
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        //y = Mathf.Clamp(y, -80f, 20f);

        angle.x += x * sensitivity * Time.deltaTime;
        angle.y += y * sensitivity * Time.deltaTime;
        //transform.eulerAngles = new Vector3(-angle.y, angle.x, transform.eulerAngles.z);

        angle.y = Mathf.Clamp(angle.y, -60f, 90f); // -90 = bottom, 20 = up
        transform.localRotation = Quaternion.Euler(-angle.y, angle.x, 0);
    }
}
