using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // 플레이어 Transform
    private float offsetX;    // 고정된 Y 위치

    void Start()
    {
        if (player == null) return;
        offsetX = transform.position.x - player.position.x;
    }

    void Update()
    {
        if (player == null) return;

        transform.position = new Vector3(player.position.x + offsetX, transform.position.y, transform.position.z);
    }
}
