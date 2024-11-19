using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoint : MonoBehaviour
{
    public Action OnTrigger;

    private void Update()
    {
        //For Debug
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnTrigger?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Movement movement))
        {
            OnTrigger?.Invoke();
            Debug.Log("OnTriggerPoint");
        }
    }
}
