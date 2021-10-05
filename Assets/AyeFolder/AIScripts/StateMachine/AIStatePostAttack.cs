using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStatePostAttack : AIStateBase
{
    public float postAtkTimer;
    public override void StartState(Enemy myEnemy)
    {
       
    }

    public override void Update(Enemy myEnemy)
    {
        postAtkTimer += Time.fixedDeltaTime;//change to after animation is over
        myEnemy.TempPost(postAtkTimer);
        if (myEnemy.InRange())
        {
            if (postAtkTimer > myEnemy.postAtkSpd)
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
        postAtkTimer = 0;
    }
}
