using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStatePreAttack : AIStateBase
{
    public float timer = 0;
    public override void StartState(Enemy myEnemy)
    {
        Debug.Log("enter preattack");
    }

    public override void Update(Enemy myEnemy)
    {
        timer += Time.fixedDeltaTime;
        if(timer > myEnemy.preAtkSpd)
        {
            myEnemy.myAC.ChangeState(myEnemy.myAC.attackState);
        }
    }

    public override void LeaveState(Enemy myEnemy)
    {
        Debug.Log(timer);
    }
}
