using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StrategyType { First, Second, Third }

public class DemandChooser : MonoBehaviour
{
	public StrategyType strategy;

	private DemandKeeper demandKeeper;
	private DemandUIManager UIManager;
	private Person person;

	private List<Demand> demands = new List<Demand>();
	private Demand currentDemand = null;

	private void Start()
	{
		GetComponents();
		SubscribeToComponents();

		UIManager.CreateSliders(demands.ToArray());
	}

	private void GetComponents()
	{
		person = FindObjectOfType<Person>();
		demands.AddRange(FindObjectsOfType<Demand>());

		demandKeeper = FindObjectOfType<DemandKeeper>();
		UIManager = FindObjectOfType<DemandUIManager>();
	}

	private void SubscribeToComponents()
	{
		person.needTarget += OnPersonNeedDemandHolder;
		person.reachedTarget += OnPersonReachedDemandHolder;

		FindObjectOfType<TimeManager>().tickTack += UpdateDemandsTime;

		foreach(var demand in demands)
		{
			demand.Overflowed += OnDemandOverflowed;
		}
	}

	private void OnDemandOverflowed(DemandType type)
	{
		/*foreach(var demand in demands)
		{
			if(demand.DemandType == type)
			{
				currentDemand = demand;
				break;
			}
		}

		SetTargetForPerson();*/
	}

	private void UpdateDemandsTime(float tick)
	{
		foreach (var item in demands)
		{
			item.TickTime(tick);
			UIManager.SetDemandValue(item.DemandType, item.GetRelativeLevel());
		}
	}

	private void OnPersonReachedDemandHolder(string targetID)
	{
		float resource = demandKeeper.GetDemandResource(targetID);
		currentDemand.SatisfyDemand(resource, currentDemand.DemandType);

		UIManager.SetDemandValue(currentDemand.DemandType, currentDemand.GetRelativeLevel());
		UIManager.HighlightOff(currentDemand.DemandType);
	}

	private void OnPersonNeedDemandHolder()
	{
		switch(strategy)
		{
			case StrategyType.First:
				currentDemand = GetHighestDemand();
				break;

			case StrategyType.Second:
				currentDemand = GetDemandByPriority();
				break;
		}
		SetTargetForPerson();
	}

	private Demand GetHighestDemand()
	{
		Demand highestDemand = null;
		foreach (var demand in demands)
		{
			if (highestDemand == null)
			{
				highestDemand = demand;
			}

			if (demand.GetRelativeLevel() > highestDemand.GetRelativeLevel())
			{
				highestDemand = demand;
			}
		}

		return highestDemand;
	}

	private Demand GetDemandByPriority()
	{
		var food = demands.Find((x) => x.DemandType == DemandType.Food);
		if (food.GetRelativeLevel() > 0.5f)
		{
			return food;
		}

		var sleep = demands.Find((x) => x.DemandType == DemandType.Sleep);
		if (sleep.GetRelativeLevel() > 0.5f)
		{
			return sleep;
		}

		var cat = demands.Find((x) => x.DemandType == DemandType.Cat);
		if (cat.GetRelativeLevel() > 0.5f)
		{
			return cat;
		}

		var relax = demands.Find((x) => x.DemandType == DemandType.Relax);
		if (relax.GetRelativeLevel() > 0.5f)
		{
			return relax;
		}

		var learn = demands.Find((x) => x.DemandType == DemandType.Learn);
		return learn;
	}

	private void SetTargetForPerson()
	{
		string targetID = demandKeeper.GetNearestDemandHolder
		(
			person.transform,
			currentDemand.DemandType
		);
		person.SetTarget(demandKeeper.GetDemandTransform(targetID), targetID);

		UIManager.HighlightOn(currentDemand.DemandType);
	}
}
