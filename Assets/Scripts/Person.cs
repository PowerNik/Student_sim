using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Person : MonoBehaviour {

	public event Action<Person> needGoal;

	private NavMeshAgent navMeshAgent;
	private DemandManager demandManager;
	private Transform goal = null;

	private float satisfactionTime = 1f;
	private float startSatisfactionTime = -3f;

	void Start ()
	{
		navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
		demandManager = gameObject.GetComponent<DemandManager>();
	}
	

	void Update ()
	{
		if(goal == null)
		{
			ChooseDemand();
		}
		else
		{
			navMeshAgent.SetDestination(goal.position);
			float delta = (transform.position - goal.position).magnitude;
			if(delta <= navMeshAgent.stoppingDistance)
			{
				startSatisfactionTime = Time.time;
				demandManager.SatisfyDemandByHolder(goal);
				goal = null;
			}
		}
	}

	private void ChooseDemand()
	{
		if(Time.time < startSatisfactionTime + satisfactionTime)
		{
			return;
		}

		Demand demand = demandManager.GetHighestDemand();
		goal = demandManager.GetDemandHolder(demand);
	}

	public void SetGoal(Transform goal)
	{
		this.goal = goal;
	}
}
