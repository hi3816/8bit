using System;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private MainController mainController;
    private Rigidbody2D rb;
    private SpriteRenderer renderer;
    
    [SerializeField] private float Speed = 5f;
    [SerializeField] private float JumpForce = 10f;

    private Vector2 moveDir = Vector2.zero;
    private bool isGrounded = false;
    private void Awake()
    {
        mainController = GetComponent<MainController>();
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponentInChildren<SpriteRenderer>();
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
    }

    private void FixedUpdate()
    {
        SetMovement(moveDir);
    }

    private void SetMovement(Vector2 direction)
    {
        direction = direction * Speed;
        direction.y = rb.velocity.y;
        rb.velocity = direction;
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name + " collision");
        if (other.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
