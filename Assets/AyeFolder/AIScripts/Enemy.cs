using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public int moveSpeed;
    public int atkSpd;
    public int attack;
    public float atkRange;
    public int preAtkSpd;
    public int postAtkSpd;

    public bool hitted;
    public bool interrupted;

    public NavMeshAgent ghostRider;
    public GameObject target;

    public AIController myAC;

    public enum AIPhase { NotInBattle, InBattle1, InBattle2 };
    public AIPhase phase;

    private void Awake()
    {
        ghostRider = GetComponent<NavMeshAgent>();
        myAC = GetComponent<AIController>();
    }

    public void LoseHealth(int hurtAmt)
    {
        if(health - hurtAmt >=0)
        {
            health -= hurtAmt;
        }
        else
        {
            health = 0;
        }
    }

    public void ChangeSpd(int ChangeAmt)
    {
        moveSpeed += ChangeAmt;
    }

    public void ChaseTarget()
    {
        ghostRider.SetDestination(target.transform.position);
    }

    public bool InRange()
    {
        if (!ghostRider.pathPending && ghostRider.remainingDistance <= atkRange)
        {
            return true;
        }
        else
            return false;
    }

}
