using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*public interface IMonster
{
    public void PlayTrigger();
}*/

public class Monster : MonoBehaviour
{
    [SerializeField] bool isPlayTriggerOntime;
    [SerializeField] bool activeAfterPlayerTrigger;

    public float moveSpeed;
    public Vector2 targetPosition;
    public MonsterPoint monsterPoint;

    protected Rigidbody2D rigidbody2D;
    protected SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void Start()
    {
        if (monsterPoint != null)
        {
            monsterPoint.OnTrigger += PlayTrigger;
        }

        SetMonster();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Movement movement))
        {
            return;
        }

        GameObject player = collision.gameObject;

        if (player.transform.position.y > transform.position.y)
        {
            Die();
        }
    }

    public void Die()
    {
        if (monsterPoint != null)
        {
            monsterPoint.OnTrigger -= PlayTrigger;
        }

        Destroy(gameObject, 0.1f);
    }

    public virtual void SetMonster()
    {

    }

    public virtual void PlayTrigger()
    {
        if (isPlayTriggerOntime)
        {
            //한번만 발동하게 하고싶음
            monsterPoint.OnTrigger -= PlayTrigger;
        }

        if(activeAfterPlayerTrigger)
        {
            gameObject.SetActive(true);
        }
    }
}
