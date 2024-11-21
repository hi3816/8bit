using System.Collections;
using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    public Transform exitPoint; // 출구 토관 위치
    public AudioClip warpSound; // 워프 사운드
    public float yOffset = 1.0f; // Y 좌표 조정 값
    public float warpDuration = 1.0f; // 워프 애니메이션 지속 시간
    public float centralThreshold = 1.5f; // 토관 중앙 범위 설정 (조금 더 증가)

    private bool isWarping = false; // 현재 워프 중인지 여부
    private bool playerInPipe = false; // 플레이어가 토관에 있는지 여부

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (CanWarp(collision))
        {
            StartCoroutine(Warp(collision.transform));
        }
    }

    private bool CanWarp(Collision2D collision)
    {
        return collision.collider.CompareTag("Player") && !isWarping && !playerInPipe && Input.GetKeyDown(KeyCode.S) && IsPlayerInCenter(collision.transform);
    }

    private bool IsPlayerInCenter(Transform player)
    {
        float playerX = player.position.x;
        float pipeCenterX = transform.position.x;
        float pipeWidth = GetComponent<Collider2D>().bounds.size.x;
        return Mathf.Abs(playerX - pipeCenterX) < centralThreshold * pipeWidth;
    }

    private IEnumerator Warp(Transform player)
    {
        isWarping = true;
        playerInPipe = true;
        SetRigidbodyKinematic(player, true);
        AudioSource.PlayClipAtPoint(warpSound, transform.position);

        yield return AnimateWarp(player, transform.position, -1);

        player.position = new Vector3(exitPoint.position.x, exitPoint.position.y - exitPoint.GetComponent<Collider2D>().bounds.extents.y - yOffset, player.position.z);

        yield return new WaitForSeconds(0.5f);

        yield return AnimateWarp(player, exitPoint.position, 1);

        SetRigidbodyKinematic(player, false);
        isWarping = false;
        playerInPipe = false;
    }

    private void SetRigidbodyKinematic(Transform player, bool state)
    {
        var rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = state;
        }
    }

    private IEnumerator AnimateWarp(Transform player, Vector3 targetPosition, int direction)
    {
        Vector3 startPosition = player.position;
        targetPosition.y += direction * (player.GetComponent<Collider2D>().bounds.extents.y + yOffset);
        for (float elapsedTime = 0; elapsedTime < warpDuration; elapsedTime += Time.deltaTime)
        {
            player.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / warpDuration);
            yield return null;
        }
        player.position = targetPosition;
    }
}
