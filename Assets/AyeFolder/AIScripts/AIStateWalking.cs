using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIStateWalking : AIStateBase
{

    public override void StartState(Enemy myEnemy)
    {

    }

    public override void Update(Enemy myEnemy)
    {
        myEnemy.ChaseTarget();
        if (myEnemy.phase != Enemy.AIPhase.NotInBattle)
        {
            if(myEnemy.InRange())
            {
                myEnemy.myAC.ChangeState(myEnemy.myAC.preAttackState);
            }
        }
    }

    public override void LeaveState(Enemy myEnemy)
    {
        myEnemy.ghostRider.isStopped = true;
    }
}
