using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // 플레이어 Transform
    private float fixedY;    // 고정된 Y 위치

    void Start()
    {
        // 카메라의 초기 Y 위치를 저장합니다.
        if (player != null)
        {
            fixedY = transform.position.y;
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // X 위치는 플레이어를 따라가고, Y 위치는 고정합니다.
            transform.position = new Vector3(player.position.x, fixedY, transform.position.z);
        }
    }
}
