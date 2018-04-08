using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Need : MonoBehaviour
{
	#region Fields

	[SerializeField]
	protected AnimationCurve curve;
	[SerializeField]
	protected float maxNeedValue = 10f;
	[SerializeField]
	protected float timeScale = 10f;
	[SerializeField]
	protected Color color = Color.white;

	public event Action<NeedType> Overflowed;

	protected float curLevel = 0f;
	protected float curTime = 0f;

	protected float[] reverseValuesMas;
	protected int timeStepCount = 21;
	protected NeedType needType = NeedType.Food;

	#endregion

	#region IDemand

	public NeedType NeedType
	{
		get
		{
			return needType;
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
		return curLevel / maxNeedValue;
	}

	public virtual void SatisfyNeed(float value, NeedType type)
	{
		if (curLevel > value)
		{
			curLevel -= value;
			curTime = GetTimeFromNeedCurve(curLevel);
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
		curLevel = maxNeedValue * curve.Evaluate(curTime);

		if(curLevel == maxNeedValue)
		{
			Overflowed(NeedType);
		}
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

	protected float GetTimeFromNeedCurve(float value)
	{
		value /= maxNeedValue;

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
