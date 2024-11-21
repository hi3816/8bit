using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpike : MonoBehaviour
{
    public GameObject spikePrefab; // 생성할 가시 프리팹
    public Transform spawnPoint;  // 가시가 생성될 위치
    public float detectionRange = 5f; // 플레이어 감지 범위
    public LayerMask playerLayer;    // 플레이어 레이어
    private AudioManager audioManager;

    private bool isSpikeSpawned = false; // 가시가 이미 생성되었는지 확인

    private void Start()
    {
        // AudioManager 인스턴스 찾기
        audioManager = FindObjectOfType<AudioManager>();
    }


    private void Update()
    {
        // 주변에 플레이어가 있는지 감지
        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer);
        if (hit != null && !isSpikeSpawned)
        {
            SpawnSpike();
        }
    }

    private void SpawnSpike()
    {
        // 가시 생성
        Instantiate(spikePrefab, spawnPoint.position, Quaternion.identity);
        isSpikeSpawned = true; // 가시가 생성된 상태로 설정

        audioManager.PlayTrapSpikeSound();
        /*if (spawnSound != null)
        {
            audioSource.PlayOneShot(spawnSound);//나중에 소리 생성용
        }*/
    }

    private void OnDrawGizmosSelected()
    {
        // 감지 범위 시각화
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
