using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_PlayerMove : MonoBehaviour
{
    // Move Speed
    public float speed = 5;
    CharacterController cc;

    // Gravity
    public float gravity = -20; // 아래 방향
    float yVelocity = 0;

    // Jump
    public float jumpPower = 5;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Move
        float h = ARAVRInput.GetAxis("Horizontal");
        float v = ARAVRInput.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir = Camera.main.transform.TransformDirection(dir); // 바라보는 방향으로 입력 방향 할당

        // Gravity
        yVelocity += gravity * Time.deltaTime;
        if (cc.isGrounded)
        {
            yVelocity = 0;
        }
        //print(yVelocity);

        // Jump. ARAVRInput.Button.Two = Space
        if (ARAVRInput.GetDown(ARAVRInput.Button.Two, ARAVRInput.Controller.RTouch))
        {
            yVelocity = jumpPower;
        }

        // Move + Gravity
        dir.y = yVelocity;
        cc.Move(dir * speed * Time.deltaTime);
    }
}
