using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Enemy : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public int moveSpeed;
    public int atkSpd;
    public int attack;
    public float atkRange;
    public int preAtkSpd;
    public int atkTime;
    public int postAtkSpd;

    public NavMeshAgent ghostRider;
    public GameObject target;

    public AIController myAC;

    public AtkTrigger myTrigger;

    public Color Origin = new Color(1, 0.5f, 0.5f, 0.3f);
    public Color TempAtkColor = new Color(1, 0, 0, 0.3f);

    public enum AIPhase { NotInBattle, InBattle1, InBattle2 };
    public AIPhase phase;

    public bool attackable;
    public bool walkable;

    public TextMeshProUGUI hittedStates;

    private void Awake()
    {
        ghostRider = GetComponent<NavMeshAgent>();
        myAC = GetComponent<AIController>();
        myTrigger = GetComponentInChildren<AtkTrigger>();
        health = maxHealth;
    }

    private void Update()
    {
        Debug.Log(InRange());
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

    public void Idleing()
    {
        if(InRange())
        {
            myTrigger.myMR.material.color = Origin;
        }
        if(!walkable || !attackable)
        {
            if(!walkable)
            { 
                hittedStates.text = "cant walk"; 
            }
            if (!attackable)
            {
                hittedStates.text = "cant attack";
            }

        }
        if(!walkable && !attackable)
        {
            hittedStates.text = "cant anything";
        }

        if(walkable && attackable)
        {
            hittedStates.text = "";
        }

    }
    public void TempPre(float time)
    {
        myTrigger.myMR.material.color = Color.Lerp(Origin, TempAtkColor, time);
    }
    public void Attacking()
    {
        myTrigger.myMR.material.color = new Color(1, 1, 1, 1);
        /*deal damage here*/
        if (InRange())
        {
            EffectManager.me.KnockBack(2, gameObject, PlayerScript.me.gameObject);
        }
    }
    public void TempPost(float time)
    {
        myTrigger.myMR.material.color = Color.Lerp(TempAtkColor, Origin, time);
    }
    public bool InRange()
    {
        
        if (myTrigger.onAtkTrigger)
        {
            return true;
        }
        else
            return false;
    }

}
