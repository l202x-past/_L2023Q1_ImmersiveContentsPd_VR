using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_PlayerTeleport_Linear : MonoBehaviour
{
    public Transform teleportCircleUI; // 텔레포트를 표시할 UI
    LineRenderer lr; // 텔레포트 선을 그릴 렌더러
    Vector3 originScale = Vector3.one * 0.02f; // 초기 텔레포트의 UI 크기

    void Start()
    {
        teleportCircleUI.gameObject.SetActive(false);
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // 왼쪽 컨트롤러의 One 버튼을 누름
        if (ARAVRInput.GetDown(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            lr.enabled = true;
        }

        // 왼쪽 컨트롤러의 One 버튼에서 손을 뗌
        else if (ARAVRInput.GetUp(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            lr.enabled = false;
            if (teleportCircleUI.gameObject.activeSelf)
            {
                GetComponent<CharacterController>().enabled = false;
                transform.position = teleportCircleUI.position + Vector3.up;
                GetComponent<CharacterController>().enabled = true;
            }
            teleportCircleUI.gameObject.SetActive(false);
        }

        // 왼쪽 컨트롤러의 One 버튼을 누르고 있을 때
        if (ARAVRInput.Get(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            //lr.enabled = true;
            Ray ray = new Ray(ARAVRInput.LHandPosition, ARAVRInput.LHandDirection);
            RaycastHit hitInfo;
            int layer = 1 << LayerMask.NameToLayer("Ground"); // Terrain 레이어만 Ray 충돌 검출
            print("LHand Position:" + ARAVRInput.LHandPosition);
            if (Physics.Raycast(ray, out hitInfo, 200, layer))
            {
                // 텔레포트 표시
                lr.SetPosition(0, ray.origin - Vector3.up);
                lr.SetPosition(1, hitInfo.point);
                //lr.SetPosition(1, ray.origin + ARAVRInput.LHandDirection * 200);

                teleportCircleUI.gameObject.SetActive(true);
                teleportCircleUI.position = hitInfo.point;
                teleportCircleUI.forward = hitInfo.normal;
                teleportCircleUI.localScale = originScale * Mathf.Max(1, hitInfo.distance);
                print("teleport pos");
            }
            else // 충돌 지점이 없다면
            {
                // 향하는 방향으로 라인 표시
                lr.SetPosition(0, ray.origin);
                lr.SetPosition(1, ray.origin + ARAVRInput.LHandDirection * 200);
                teleportCircleUI.gameObject.SetActive(false);
                print("no teleport pos");
            }
        }
        else
        {
            //print("................");
        }
    }
}
