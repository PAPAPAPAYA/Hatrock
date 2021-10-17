using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateAttacking : AIStateBase
{
    public float AtkTimer;
    public override void StartState(Enemy myEnemy)
    {
        myEnemy.myTrigger.myMR.enabled = true;
    }

    public override void Update(Enemy myEnemy)
    {
        if (myEnemy.attackable)
        {
            AtkTimer += Time.fixedDeltaTime;//change to after animation is over
            myEnemy.KnowckBackAtk();
            if (AtkTimer > myEnemy.atkTime)
            {
                myEnemy.myAC.ChangeState(myEnemy.myAC.postAttackState);
            }
        }
        else if (!myEnemy.attackable)
        {
            myEnemy.myAC.ChangeState(myEnemy.myAC.idleState);
        }


    }

    public override void LeaveState(Enemy myEnemy)
    {
        AtkTimer = 0;
    }

}
