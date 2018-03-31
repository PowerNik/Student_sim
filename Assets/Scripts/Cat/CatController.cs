using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CatStateType { DoAction, Walk, Crazy }

public class CatController : MonoBehaviour
{
	public Transform student;
	public Transform[] destinations;

	private float letsCrazyChance = 0.1f;
	private CatState currentState;
	private Dictionary<CatStateType, CatState> catStateDict;

	void Start()
	{
		catStateDict = new Dictionary<CatStateType, CatState>();
		InitializeAllStates();

		currentState = catStateDict[CatStateType.Walk];
		currentState.EnterState();
	}

	private void InitializeAllStates()
	{
		CatActionState CatActionState = gameObject.AddComponent<CatActionState>();
		InitializeState(CatActionState);

		CatWalkState CatWalkState = gameObject.AddComponent<CatWalkState>();
		InitializeState(CatWalkState);

		CatCrazyState CatCrazyState = gameObject.AddComponent<CatCrazyState>();
		InitializeState(CatCrazyState);
	}

	private void InitializeState(CatState state)
	{
		catStateDict[state.State] = state;
		state.StateEndedEvent += OnStateEnded;
		state.Initialize(student, destinations);
	}

	private void OnStateEnded(CatStateType state)
	{
		if(state == CatStateType.DoAction)
		{
			if (Random.Range(0f, 1f) < letsCrazyChance)
			{
				currentState = catStateDict[CatStateType.Crazy];
			}
			else
			{
				currentState = catStateDict[CatStateType.Walk];
			}
		}

		if (state == CatStateType.Walk || state == CatStateType.Crazy)
		{
			currentState = catStateDict[CatStateType.DoAction];
		}

		currentState.EnterState();
	}
}
