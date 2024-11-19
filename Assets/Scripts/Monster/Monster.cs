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
        }

        SetMonster();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out Movement movement))
        {
            return;
        }

        Debug.Log("1");

        GameObject player = collision.gameObject;

        if (player.transform.position.y > transform.position.y)
        {
            Debug.Log("2");
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Die");
        
        monsterPoint.OnTrigger -= PlayTrigger;

        Destroy(gameObject);
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
