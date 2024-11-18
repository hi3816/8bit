using UnityEngine;

public class Movement : MonoBehaviour
{
    private MainController mainController;
    private Rigidbody2D rb;
    private SpriteRenderer renderer;
    
    [SerializeField] private float Speed = 5f;

    private Vector2 moveDir = Vector2.zero;
    private void Awake()
    {
        mainController = GetComponent<MainController>();
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        mainController.OnMoveEvent += UpdateMove;
    }
    
    private void OnDestroy()
    {
        mainController.OnMoveEvent -= UpdateMove;
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
        rb.velocity = direction;
    }
}
