using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
	[Range(0f, 1f)]
	[SerializeField]
	private float timeSpeed = 0.2f;
	private float sendedTickTime = 0;

	public event Action<float> tickTack;

	void Update ()
	{
		tickTack(Time.deltaTime * timeSpeed);
	}
}
