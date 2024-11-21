using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private MainController mainController;
    private Rigidbody2D rb;
    private SpriteRenderer renderer;
    private Animator animator;
    private BoxCollider2D collider;
    
    [SerializeField] private float speed = 2f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float doubleJumpForce = 1.5f;

    private AudioManager audioManager;

    private Vector2 moveDir = Vector2.zero;
    private bool isGrounded;
    private int jumpCnt;
    private int maxJumpCnt = 2;
    private bool isDead = false;

    private readonly string Ground = "Ground";
    private readonly int isRunning = Animator.StringToHash("isRunning");
    private readonly int isJumping = Animator.StringToHash("isJumping");
    private readonly int isDoubleJump = Animator.StringToHash("isDoubleJump");
    private void Awake()
    {
        mainController = GetComponent<MainController>();
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        mainController.OnMoveEvent += UpdateMove;
        mainController.OnJumpEvent += Jump;
        GameManager.Instance.OnDeath += PlayerDeath;
    }

    private void OnDestroy()
    {
        mainController.OnMoveEvent -= UpdateMove;
        mainController.OnJumpEvent -= Jump;
        GameManager.Instance.OnDeath -= PlayerDeath;
    }

    private void UpdateMove(Vector2 direction)
    {
        moveDir = direction;
        moveDir.y = 0;

        if (moveDir.x < 0) renderer.flipX = true;
        else if (moveDir.x > 0) renderer.flipX = false;
        animator.SetBool(isRunning, Mathf.Abs(moveDir.x) > 0.01f);
    }

    private void FixedUpdate()
    {
        SetMovement(moveDir);
    }

    private void SetMovement(Vector2 direction)
    {
        direction = direction * speed;
        direction.y = rb.velocity.y;
        rb.velocity = direction;
    }

    private void Jump()
    {
        if (jumpCnt < maxJumpCnt)
        {
            rb.velocity = Vector2.zero;
            switch (jumpCnt)
            {
                case 0:
                    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    animator.SetBool(isJumping, true);
                    break;
                case 1:
                    rb.AddForce(Vector2.up * jumpForce * doubleJumpForce, ForceMode2D.Impulse);
                    animator.SetTrigger(isDoubleJump);
                    break;
            }
            jumpCnt++;
            isGrounded = false;
            audioManager.PlayJumpSound();
        }
    }

    private void PlayerDeath()
    {
        animator.SetBool(isJumping, false);
        animator.ResetTrigger(isDoubleJump);
        animator.SetTrigger("isDamaged");

        isDead = true;
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);
        StartCoroutine(Dead());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag(Ground))
        {
            isGrounded = true;
            jumpCnt = 0;
            animator.SetBool(isJumping, false);
        }

        // Trap과 충돌하고 플레이어가 죽지않은 경우
        if (other.collider.CompareTag("Trap") && !isDead)
        {
            PlayerDeath();
        }
        // Monster랑 충돌하고 플레이어가 죽지않은 경우
        if (other.collider.CompareTag("Monster") && !isDead)
        {
            PlayerDeath();
        }
    }

    IEnumerator Dead()
    {
        audioManager.PlayPlayerDieSound();
        yield return new WaitForSeconds(0.1f); // 0.1초간 대기
        collider.enabled = false; // 하강(추락)을 위해 collider2D 비활성화
        rb.constraints =
            RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation; // 이동제한 x축 위치, 회전 고정
        yield return new WaitUntil(() => transform.position.y < -5f);
        GameManager.Instance.HandlePlayerDeath();
        // yield return new WaitUntil(() => transform.position.y < -5f); 특정좌표 이하일 경우 실행
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag(Ground))
        {
            isGrounded = false;
            animator.SetBool(isJumping, true);
        }
    }
}

