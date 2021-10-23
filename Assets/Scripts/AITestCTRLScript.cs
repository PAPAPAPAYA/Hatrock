using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITestCTRLScript : MonoBehaviour
{
    public Transform goal;
	NavMeshAgent agent;
	Enemy eS;
	public GameObject dropMeterUI;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		eS = GetComponent<Enemy>();
	}

	private void Update()
	{
		dropMeterUI.GetComponent<TextMesh>().text = eS.dropMeter.ToString();
		if (eS.walkable)
		{
			agent.isStopped = false;
			agent.destination = goal.position;
		}
		else
		{
			agent.isStopped = true;
		}
	}
}
