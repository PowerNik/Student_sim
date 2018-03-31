using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CatState : MonoBehaviour
{
	public event System.Action<CatStateType> StateEndedEvent;

	public CatStateType State
	{
		get; protected set;
	}

	protected Transform student;
	protected Transform[] destinations;

	private bool isStateEntered = false;

	public void Initialize(Transform student, Transform[] destinations)
	{
		this.student = student;
		this.destinations = destinations;
	}

	public virtual void EnterState()
	{
		isStateEntered = true;
		Debug.Log("Entry to cat state:" + State);
	}

	private void Update()
	{
		if (isStateEntered)
		{
			OnState();
		}
	}

	protected virtual void OnState()
	{

	}

	protected virtual void ExitState()
	{
		isStateEntered = false;
		Debug.Log("Exit from cat state:" + State);
		StateEndedEvent(State);
	}
}