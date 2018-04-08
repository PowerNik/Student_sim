using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatWalkState : CatState
{ 
	private Transform currentDestination;

	private NavMeshAgent navMeshAgent;
	private Animator animator;

	private float goToStudentChance = 0.2f;

	private void Awake()
	{
		State = CatStateType.Walk;
		navMeshAgent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
	}

	public override void EnterState()
	{
		base.EnterState();
		navMeshAgent.stoppingDistance = 1.5f;
		ChooseDestination();
	}

	protected override void OnState()
	{
		navMeshAgent.SetDestination(currentDestination.position);

		float distance = (transform.position - currentDestination.position).magnitude;
		if (distance <= navMeshAgent.stoppingDistance)
		{
			animator.SetTrigger("Idle");
			base.ExitState();
		}
	}

	private void ChooseDestination()
	{
		if (Random.Range(0f, 1f) < goToStudentChance)
		{
			currentDestination = student;
			Debug.Log("Go to student");
		}
		else
		{
			int index = Random.Range(0, destinations.Length);
			currentDestination = destinations[index];
			Debug.Log("Go to point");
		}

		animator.SetTrigger("Walk");
	}
}
