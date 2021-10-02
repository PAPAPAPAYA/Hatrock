using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public AIStateBase currentState;
    public AIStateWalking walkingState;
    public AIStateAttacking attackState;
    public AIStateHitted hittedState;


    public void ChangeState(AIStateBase newState)
    {
        if(currentState != null)
        {
            currentState.LeaveState(this);
        }

        currentState = newState;

        if(currentState != null)
        {
            currentState.StartState(this);
        }

    }

    void Start()
    {
        currentState = walkingState;
    }

    void Update()
    {
        currentState.Update(this);
    }
}
