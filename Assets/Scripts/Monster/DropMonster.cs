using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMonster : Monster
{
    public override void SetMonster()
    {
        gameObject.SetActive(false);
    }
    public override void PlayTrigger()
    {
        Debug.Log("DropMonsterTrigger");

        base.PlayTrigger();
        gameObject.SetActive(true);
    }
}
