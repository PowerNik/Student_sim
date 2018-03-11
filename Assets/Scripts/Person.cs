using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Person : MonoBehaviour
{
	public event Action needTarget;
	public event Action<string> reachedTarget;

	private Animator animator;
	private NavMeshAgent navMeshAgent;

	private Transform target = null;
	private string targetID = "";

	private float switchTargetDelay = 2f;

	void Start ()
	{
		animator = gameObject.GetComponent<Animator>();
		navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
		StartCoroutine(NeedNewTarget());
	}
	
	void Update ()
	{
		if(target != null)
		{
			MoveToTarget();
		}
	}

	private void MoveToTarget()
	{
		navMeshAgent.SetDestination(target.position);
		animator.SetFloat("MoveSpeed", navMeshAgent.velocity.magnitude);

		float delta = (transform.position - target.position).magnitude;
		if (delta <= navMeshAgent.stoppingDistance)
		{
			navMeshAgent.velocity = Vector3.zero;

			animator.SetFloat("MoveSpeed", 0);
			animator.SetTrigger("Pickup");

			target = null;
		}
	}

	public void Grab()
	{
		reachedTarget(targetID);
		StartCoroutine(NeedNewTarget());
	}

	private IEnumerator NeedNewTarget()
	{
		yield return new WaitForSeconds(switchTargetDelay);
		needTarget();
	}

	public void SetTarget(Transform target, string targetID)
	{
		this.target = target;
		this.targetID = targetID;
	}
}
