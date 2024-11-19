using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightMonster : Monster
{
    [SerializeField] bool IsGoLeft = true;

    void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2((IsGoLeft ?  -moveSpeed : moveSpeed), rigidbody2D.velocity.y);
    }

    public override void SetMonster()
    {
        base.SetMonster();

        gameObject.SetActive(true);
    }
}
