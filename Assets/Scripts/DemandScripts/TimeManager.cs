using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
	[SerializeField]
	private float timeScale = 2;
	private float sendedTickTime = 0;

	public event Action<float> tickTack;

	void Update ()
	{
		tickTack(Time.deltaTime / timeScale);
	}
}
