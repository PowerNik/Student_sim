using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Demand : MonoBehaviour
{
	#region Fields

	[SerializeField]
	protected AnimationCurve curve;
	[SerializeField]
	protected float maxDemandValue = 10f;
	[SerializeField]
	protected float timeScale = 10f;
	[SerializeField]
	protected Color color = Color.white;

	protected float curLevel = 0f;
	protected float curTime = 0f;

	protected float[] reverseValuesMas;
	protected int timeStepCount = 21;
	protected DemandType demandType = DemandType.Food;

	#endregion

	#region IDemand

	public DemandType DemandType
	{
		get
		{
			return demandType;
		}
	}

	public Color GetColor()
	{
		return color;
	}

	public float GetCurrentLevel()
	{
		return curLevel;
	}

	public float GetRelativeLevel()
	{
		return curLevel / maxDemandValue;
	}

	public virtual void SatisfyDemand(float value, DemandType type)
	{
		if (curLevel > value)
		{
			curLevel -= value;
			curTime = GetTimeFromDemandCurve(curLevel);
		}
		else
		{
			curLevel = 0;
			curTime = 0;
		}
	}

	public virtual void TickTime(float tick)
	{
		curTime += tick / timeScale;
		curLevel = maxDemandValue * curve.Evaluate(curTime);
	}

	#endregion

	void Start()
	{
		CalculateReverseFunction();
	}

	protected void CalculateReverseFunction()
	{
		reverseValuesMas = new float[timeStepCount];
		float timeStep = 1.0f / (timeStepCount - 1);

		for (int i = 0; i < timeStepCount; i++)
		{
			reverseValuesMas[i] = curve.Evaluate(i * timeStep);
		}
	}

	protected float GetTimeFromDemandCurve(float value)
	{
		value /= maxDemandValue;

		if(value <= 0)
		{
			return 0f;
		}

		for (int i = 0; i < timeStepCount - 1; i++)
		{
			if(reverseValuesMas[i] <= value && value < reverseValuesMas[i + 1])
			{
				return i / (float)(timeStepCount - 1);
			}
		}

		return 1f;
	}
}
