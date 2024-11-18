using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightPatrolMonster : Monster
{
    public float partolDelay;

    WaitForSeconds patrolWait;

    // Start is called before the first frame update
    void Start()
    {
        patrolWait = new WaitForSeconds(partolDelay);
    }

    IEnumerator Patrol()
    {
        while(true)
        {

        }
    }
}
