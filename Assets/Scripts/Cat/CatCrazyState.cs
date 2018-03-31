using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatCrazyState : CatState
{
	private Transform currentDestination;

	private float goToStudentChance = 0.5f;

	private float speedScale = 5;
	private float crazyTime = 10;
	private float endCrazyTime;

	private NavMeshAgent navMeshAgent;
	private Animator animator;

	private void Awake()
	{
		State = CatStateType.Crazy;
		navMeshAgent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
	}

	public override void EnterState()
	{
		base.EnterState();

		navMeshAgent.speed *= speedScale;
		navMeshAgent.acceleration *= speedScale;
		navMeshAgent.angularSpeed *= speedScale;

		animator.SetTrigger("Walk");
		endCrazyTime = Time.time + crazyTime;

		ChooseDestination();
	}

	protected override void OnState()
	{
		if(Time.time > endCrazyTime)
		{
			ExitState();
		}

		navMeshAgent.SetDestination(currentDestination.position);

		float distance = (transform.position - currentDestination.position).magnitude;
		if (distance <= navMeshAgent.stoppingDistance)
		{
			ChooseDestination();
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
	}

	protected override void ExitState()
	{
		navMeshAgent.speed /= speedScale;
		navMeshAgent.acceleration /= speedScale;
		navMeshAgent.angularSpeed /= speedScale;

		animator.SetTrigger("Idle");
		base.ExitState();
	}
}
