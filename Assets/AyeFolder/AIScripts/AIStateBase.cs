using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIStateBase
{
 
    public abstract void StartState(AIController AC);

    public abstract void Update(AIController AC);

    public abstract void LeaveState(AIController AC);
}
