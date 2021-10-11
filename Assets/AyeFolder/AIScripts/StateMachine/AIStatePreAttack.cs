using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStatePreAttack : AIStateBase
{
    public float preAtkTimer = 0;
    public override void StartState(Enemy myEnemy)
    {
        //Debug.Log("enter preattack");
        myEnemy.myTrigger.myMR.enabled = true;
    }

    public override void Update(Enemy myEnemy)
    {
        preAtkTimer += Time.fixedDeltaTime;//change to after animation is over
        myEnemy.TempPre(preAtkTimer);

        if (preAtkTimer > myEnemy.preAtkSpd)
        {
            myEnemy.myAC.ChangeState(myEnemy.myAC.attackState);
        }
        

        
        
    }

    public override void LeaveState(Enemy myEnemy)
    {
        preAtkTimer = 0;
        Debug.Log(preAtkTimer);
    }
}