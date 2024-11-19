using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigidbody2D;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(rigidbody2D.velocity.x) > 0.05f)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }
}
