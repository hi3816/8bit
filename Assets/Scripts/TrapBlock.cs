using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBlock : MonoBehaviour
{
    public GameObject blockPrefab;    // 생성할 블록 프리팹
    public Transform spawnPoint;     // 블록이 생성될 위치
    private void OnCollisionEnter2D (Collision2D collision)
    {
        // 충돌한 객체가 플레이어인지 확인
        if (collision.collider.CompareTag("Player"))
        {
            // 플레이어가 머리로 부딪혔는지 확인 (충돌 방향이 위쪽에서 아래로)
            if (collision.contacts[0].normal.y > 0.5f)
            {
                // 블록 생성
                Instantiate(blockPrefab, spawnPoint.position, Quaternion.identity);
            }
        }
    }
}
