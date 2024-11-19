using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPlat : MonoBehaviour
{
    public ParticleSystem destroyEffect; // 파티클 효과
    public AudioClip destroySound;       // 사운드
    private AudioSource audioSource;

    //나중에 사운드 추가
    /*private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }*/

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 이펙트 생성
            /*if (destroyEffect != null)
            {
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
            }

            // 사운드 재생
            if (destroySound != null && audioSource != null)
            {
                audioSource.PlayOneShot(destroySound);
            }*/

            // 오브젝트 삭제
            Destroy(gameObject);
        }
    }
}
