using UnityEngine;

public class Movement : MonoBehaviour
{
    private MainController mainController;
    private Rigidbody2D rb;
    private SpriteRenderer renderer;
    private Animator animator;
    
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float doubleJumpForce = 1.5f;

    private AudioManager audioManager;

    private Vector2 moveDir = Vector2.zero;
    private bool isGrounded;
    private int jumpCnt;
    private int maxJumpCnt = 2;

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
    }

    private void Start()
    {
        mainController.OnMoveEvent += UpdateMove;
        mainController.OnJumpEvent += Jump;
    }

    private void OnDestroy()
    {
        mainController.OnMoveEvent -= UpdateMove;
        mainController.OnJumpEvent -= Jump;
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag(Ground))
        {
            isGrounded = true;
            jumpCnt = 0;
            animator.SetBool(isJumping, false);
        }
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

