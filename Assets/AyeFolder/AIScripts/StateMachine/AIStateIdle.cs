using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateIdle : AIStateBase
{
    public float IdleTimer;
    public override void StartState(Enemy myEnemy)
    {
        myEnemy.Idleing();
    }

    public override void Update(Enemy myEnemy)
    {
        IdleTimer += Time.fixedDeltaTime;
        if (myEnemy.InRange())
        {
            if (IdleTimer > myEnemy.atkSpd)
            {
                myEnemy.myAC.ChangeState(myEnemy.myAC.preAttackState);
            }
        }
        else if (!myEnemy.InRange())
        {
            myEnemy.myAC.ChangeState(myEnemy.myAC.walkingState);
        }
    }

    public override void LeaveState(Enemy myEnemy)
    {
        IdleTimer = 0;
    }
}
