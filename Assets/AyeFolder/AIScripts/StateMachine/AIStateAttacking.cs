using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateAttacking : AIStateBase
{
    public float AtkTimer;
    public override void StartState(Enemy myEnemy)
    {
        
    }

    public override void Update(Enemy myEnemy)
    {
        AtkTimer += Time.fixedDeltaTime;//change to after animation is over
        myEnemy.Attacking();
        if (AtkTimer > myEnemy.atkTime)
        {
            myEnemy.myAC.ChangeState(myEnemy.myAC.postAttackState);
        }
        

    }

    public override void LeaveState(Enemy myEnemy)
    {
        AtkTimer = 0;
    }

}
