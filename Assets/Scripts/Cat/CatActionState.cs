using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CatActionType { Eat = 0, Sound = 1, Jump = 2 }

public class CatActionState : CatState
{
	private Animator animator;

	private void Awake()
	{
		State = CatStateType.DoAction;
		animator = GetComponent<Animator>();
	}

	public override void EnterState()
	{
		int actionIndex = Random.Range(0, 3);
		string actionName = ((CatActionType)actionIndex).ToString();

		animator.SetTrigger(actionName);
		Debug.Log("Choosed action: " + actionName);
	}

	public void AnimationEnded(string name)
	{
		Debug.Log("Ended action: " + name);
		base.ExitState();
	}
}
