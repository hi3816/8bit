using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockH : MonoBehaviour
{
    public float detectionRange = 10f; // 플레이어 감지 거리
    public LayerMask playerLayer;     // 플레이어 레이어 지정
    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        // Rigidbody2D 컴포넌트 가져오기
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // 시작할 때 중력 비활성화
        rb.gravityScale = 0;
        // Idle 상태 유지
        animator.Play("Idle");
    }

    private void Update()
    {
        // 아래로 Raycast를 발사하여 플레이어 감지
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, detectionRange, playerLayer);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            // 플레이어 감지 시 눈 뜨기 애니메이션 재생 및 중력 활성화
            animator.SetTrigger("OpenEyes");
            rb.gravityScale = 3;
            Debug.Log("쿵! 플레이어 감지됨!");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.isKinematic = true; // 충돌 후 멈추기
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.isKinematic = true; // 충돌 후 멈추기
        }
    }
    private void OnDrawGizmosSelected()
    {
        // 감지 거리 시각화
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * detectionRange);
    }
}
