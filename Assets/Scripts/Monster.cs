using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IMonster
{
    void OnCollisionWithPlayer(Collider other)
    {

    }
}

public class Monster : MonoBehaviour
{
    public Vector2 targetPosition;

    public float moveSpeed;

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

    }
}
