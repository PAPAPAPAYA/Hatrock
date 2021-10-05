using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStatePreAttack : AIStateBase
{
    public float preAtkTimer = 0;
    public override void StartState(Enemy myEnemy)
    {
        //Debug.Log("enter preattack");
    }

    public override void Update(Enemy myEnemy)
    {
        preAtkTimer += Time.fixedDeltaTime;//change to after animation is over
        myEnemy.TempPre(preAtkTimer);
        if (myEnemy.InRange())
        {
            if (preAtkTimer > myEnemy.preAtkSpd)
            {
                myEnemy.myAC.ChangeState(myEnemy.myAC.attackState);
            }
        }
        else if (!myEnemy.InRange())
        {
            myEnemy.myAC.ChangeState(myEnemy.myAC.walkingState);
        }
        
        
    }

    public override void LeaveState(Enemy myEnemy)
    {
        preAtkTimer = 0;
        Debug.Log(preAtkTimer);
    }
}
