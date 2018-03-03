using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Person : MonoBehaviour {

	public event Action<Person> needGoal;

	private Animator anim;
	private NavMeshAgent navMeshAgent;
	private DemandManager demandManager;
	private Transform goal = null;

	private float satisfactionTime = 3f;
	private float startSatisfactionTime = -3f;

	void Start ()
	{
		anim = gameObject.GetComponent<Animator>();
		navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
		demandManager = gameObject.GetComponentInChildren<DemandManager>();
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
			anim.SetFloat("MoveSpeed", navMeshAgent.velocity.magnitude);

			float delta = (transform.position - goal.position).magnitude;
			if(delta <= navMeshAgent.stoppingDistance)
			{
				navMeshAgent.velocity = Vector3.zero;
				anim.SetFloat("MoveSpeed", 0);
				anim.SetTrigger("Pickup");

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
