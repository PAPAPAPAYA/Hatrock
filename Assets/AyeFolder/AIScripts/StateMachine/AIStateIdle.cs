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

        if (myEnemy.InRange())
        {
            myEnemy.Idleing();
            if (myEnemy.attackable)
            {
                IdleTimer += Time.fixedDeltaTime;
                if (IdleTimer > myEnemy.atkSpd)
                {
                    myEnemy.myAC.ChangeState(myEnemy.myAC.preAttackState);
                }
            }
            else if (!myEnemy.attackable)
            {
                myEnemy.Idleing();
            }
        }
        else if (!myEnemy.InRange())
        {
            if (myEnemy.walkable)
            {
                myEnemy.myAC.ChangeState(myEnemy.myAC.walkingState);
            }
            else if (!myEnemy.walkable)
            {
                myEnemy.Idleing();
            }

        }
    }

    public override void LeaveState(Enemy myEnemy)
    {
        IdleTimer = 0;
    }
}
