using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemandChooser : MonoBehaviour
{
	private DemandKeeper demandKeeper;
	private DemandUIManager UIManager;
	private Person person;

	private Demand[] demands;
	private Demand currentDemand = null;

	private void Start()
	{
		GetComponents();
		SubscribeToComponents();

		UIManager.CreateSliders(demands);
	}

	private void GetComponents()
	{
		person = FindObjectOfType<Person>();
		demands = FindObjectsOfType<Demand>();
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

	private void OnPersonReachedDemandHolder(string targetID)
	{
		float resource = demandKeeper.GetDemandResource(targetID);
		currentDemand.SatisfyDemand(resource, currentDemand.DemandType);

		UIManager.SetDemandValue(currentDemand.DemandType, currentDemand.GetRelativeLevel());
		UIManager.HighlightOff(currentDemand.DemandType);
	}

	private void OnPersonNeedDemandHolder()
	{
		currentDemand = GetHighestDemand();
		SetTargetForPerson();
	}

	private void SetTargetForPerson()
	{
		string targetID = demandKeeper.GetNearestDemandHolder
		(
			person.transform,
			currentDemand.DemandType
		);
		person.SetTarget(demandKeeper.GetDemandTransform(targetID), targetID);

		UIManager.Highlight(currentDemand.DemandType);
	}
}
