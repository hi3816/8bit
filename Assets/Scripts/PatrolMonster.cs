using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMonster : Monster
{
    public List<Transform> patrolPointObjects;  // 순찰 지점 오브젝트
    private List<Vector2> patrolPoints;  // 순찰 지점
    public float patrolDelay;          // 순찰 지점 도착 후 대기 시간

    private WaitForSeconds patrolWait;
    private int patrolIndex;
    private int patrolDirection = 1;   // 1: 정방향, -1: 역방향

    void Start()
    {
        patrolPoints = new List<Vector2>();

        foreach (Transform t in patrolPointObjects)
        {
            patrolPoints.Add(t.position);
        }

        patrolWait = new WaitForSeconds(patrolDelay);

        StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        while (true)
        {
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
        while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
        {
            Vector3 direction = (Vector3)targetPosition - transform.position;
            direction.Normalize();

            transform.position += direction * moveSpeed * Time.deltaTime;

            yield return null;
        }
    }
}
