using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateAttacking : AIStateBase
{
    public override void StartState(Enemy myEnemy)
    {
        Debug.Log("Attacking");
    }

    public override void Update(Enemy myEnemy)
    {
        
    }

    public override void LeaveState(Enemy myEnemy)
    {
        
    }

}
