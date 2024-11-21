using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime = 0.5f; // 플레이어가 내려가기 전 대기 시간

    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        // 아래로 내려가려는 입력을 감지
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            effector.rotationalOffset = 180f; // 아래로 통과 가능
        }

        // 대기 시간 후 다시 위로 충돌 가능
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            Invoke("ResetPlatform", waitTime);
        }
    }

    private void ResetPlatform()
    {
        effector.rotationalOffset = 0f; // 다시 위로 충돌 가능
    }
}
