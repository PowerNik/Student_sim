using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StrategyType { First, Second, Third }

public class NeedsChooser : MonoBehaviour
{
	public StrategyType strategy;

	private DemandKeeper demandKeeper;
	private NeedsUIManager UIManager;
	private Person person;

	private List<Need> needs = new List<Need>();
	private Need currentNeed = null;

	private void Start()
	{
		GetComponents();
		SubscribeToComponents();

		UIManager.CreateSliders(needs.ToArray());
	}

	private void GetComponents()
	{
		person = FindObjectOfType<Person>();
		needs.AddRange(GetComponents<Need>());

		demandKeeper = FindObjectOfType<DemandKeeper>();
		UIManager = FindObjectOfType<NeedsUIManager>();
	}

	private void SubscribeToComponents()
	{
		person.chooseNextNeed += OnPersonNeedDemandHolder;
		person.reachedTarget += OnPersonReachedDemandHolder;

		FindObjectOfType<TimeManager>().tickTack += UpdateDemandsTime;

		foreach(var demand in needs)
		{
			demand.Overflowed += OnDemandOverflowed;
		}
	}

	private void OnDemandOverflowed(NeedType type)
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
		foreach (var item in needs)
		{
			item.TickTime(tick);
			UIManager.SetNeedValue(item.NeedType, item.GetRelativeLevel());
		}
	}

	private void OnPersonReachedDemandHolder(string targetID)
	{
		float resource = demandKeeper.GetDemandResource(targetID);
		currentNeed.SatisfyNeed(resource, currentNeed.NeedType);

		UIManager.SetNeedValue(currentNeed.NeedType, currentNeed.GetRelativeLevel());
		UIManager.HighlightOff(currentNeed.NeedType);
	}

	private void OnPersonNeedDemandHolder()
	{
		switch(strategy)
		{
			case StrategyType.First:
				currentNeed = GetHighestDemand();
				break;

			case StrategyType.Second:
				currentNeed = GetDemandByPriority();
				break;
		}
		SetTargetForPerson();
	}

	private Need GetHighestDemand()
	{
		Need highestDemand = null;
		foreach (var demand in needs)
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

	private Need GetDemandByPriority()
	{
		var food = needs.Find((x) => x.NeedType == NeedType.Food);
		if (food.GetRelativeLevel() > 0.5f)
		{
			return food;
		}

		var sleep = needs.Find((x) => x.NeedType == NeedType.Sleep);
		if (sleep.GetRelativeLevel() > 0.5f)
		{
			return sleep;
		}

		var cat = needs.Find((x) => x.NeedType == NeedType.Cat);
		if (cat.GetRelativeLevel() > 0.5f)
		{
			return cat;
		}

		var relax = needs.Find((x) => x.NeedType == NeedType.Relax);
		if (relax.GetRelativeLevel() > 0.5f)
		{
			return relax;
		}

		var learn = needs.Find((x) => x.NeedType == NeedType.Learn);
		return learn;
	}

	private void SetTargetForPerson()
	{
		string targetID = demandKeeper.GetNearestDemandHolder
		(
			person.transform,
			currentNeed.NeedType
		);
		person.SetTarget(demandKeeper.GetDemandTransform(targetID), targetID);

		UIManager.HighlightOn(currentNeed.NeedType);
	}
}
