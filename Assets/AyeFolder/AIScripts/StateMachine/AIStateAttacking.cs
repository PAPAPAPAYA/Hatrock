using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateAttacking : AIStateBase
{
    public float AtkTimer;
    public override void StartState(Enemy myEnemy)
    {
        myEnemy.Attacking();
    }

    public override void Update(Enemy myEnemy)
    {
        AtkTimer += Time.fixedDeltaTime;//change to after animation is over
        if (myEnemy.InRange())
        {
            if (AtkTimer > myEnemy.atkTime)
            {
                myEnemy.myAC.ChangeState(myEnemy.myAC.postAttackState);
            }
        }
        else if (!myEnemy.InRange())
        {
            myEnemy.myAC.ChangeState(myEnemy.myAC.walkingState);
        }
    }

    public override void LeaveState(Enemy myEnemy)
    {
        AtkTimer = 0;
    }

}
