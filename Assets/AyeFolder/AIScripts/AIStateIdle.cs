using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateIdle : AIStateBase
{
    public override void StartState(Enemy myEnemy)
    {

    }

    public override void Update(Enemy myEnemy)
    {
        if(myEnemy.target != null)
        {
            myEnemy.myAC.ChangeState(myEnemy.myAC.walkingState);
        }
    }

    public override void LeaveState(Enemy myEnemy)
    {

    }
}
