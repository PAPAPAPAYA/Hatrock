using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateIdle : AIStateBase
{
    public float IdleTimer;
    public override void StartState(Enemy myEnemy)
    {
        
    }

    public override void Update(Enemy myEnemy)
    {
        
        myEnemy.Idleing();
        if (myEnemy.InRange())
        {
            IdleTimer += Time.fixedDeltaTime;
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
