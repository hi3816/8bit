using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMonster : Monster
{
    public List<Transform> patrolPointObjects; // 순찰 지점 오브젝트
    public List<Vector2> patrolPoints;         // 순찰 지점
    public float patrolDelay;                 // 순찰 지점 도착 후 대기 시간

    private WaitForSeconds patrolWait;
    private int patrolIndex;
    private int patrolDirection = 1;          // 1: 정방향, -1: 역방향

    [SerializeField] private bool IsMoveX;

    public override void Start()
    {
        base.Start();

        patrolPoints = new List<Vector2>();
        foreach (Transform t in patrolPointObjects)
        {
            patrolPoints.Add(t.position);
        }

        patrolWait = new WaitForSeconds(patrolDelay);

        StartCoroutine(Patrol());
    }

    private void Update()
    {
        
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            if (transform.position.x < patrolPoints[patrolIndex].x)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipY = false;
            }

            // 현재 순찰 지점으로 이동
            yield return StartCoroutine(MoveToPosition(patrolPoints[patrolIndex]));

            // 도착 후 대기
            yield return patrolWait;

            // 다음 순찰 지점으로 방향 전환
            patrolIndex += patrolDirection;

            // 방향 전환 로직 (리스트 끝에 도달하면 방향을 반대로 변경)
            if (patrolIndex >= patrolPoints.Count || patrolIndex < 0)
            {
                patrolDirection *= -1;  // 방향 반전
                patrolIndex += patrolDirection; // 범위 내로 복구
            }
        }
    }

    IEnumerator MoveToPosition(Vector2 targetPosition)
    {
        // 목표 지점까지 이동
        while (Mathf.Abs(rigidbody2D.position.x - targetPosition.x) > 0.1f)
        {
            Vector2 direction = (targetPosition - rigidbody2D.position).normalized;

            // X축만 이동
            if (IsMoveX)
            {
                direction.y = 0; // Y축 이동 제거
            }

            // Rigidbody의 velocity 설정
            rigidbody2D.velocity = direction * moveSpeed;

            yield return null;
        }

        /*// 이동 완료 후 정확히 타겟 위치에 맞추기 (오차 제거)
        GetComponent<Rigidbody2D>().velocity = Vector2.zero; // 속도 초기화
        GetComponent<Rigidbody2D>().position = new Vector2(
            IsMoveX ? targetPosition.x : GetComponent<Rigidbody2D>().position.x,
            IsMoveX ? GetComponent<Rigidbody2D>().position.y : targetPosition.y
        );*/
    }
}
