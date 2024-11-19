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

    public float moveSpeed;
    public Vector2 targetPosition;
    public MonsterPoint monsterPoint;

    public void Start()
    {
        if (monsterPoint != null)
        {
            monsterPoint.OnTrigger += PlayTrigger;
            Debug.Log("등록");
        }
        else
        {
            Debug.Log("등록안함");
        }

        SetMonster();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
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
        monsterPoint.OnTrigger -= PlayTrigger;
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
    }
}
