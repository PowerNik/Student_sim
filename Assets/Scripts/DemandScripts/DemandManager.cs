using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemandManager : MonoBehaviour
{
	[SerializeField]
	private RectTransform UIContainer;
	[SerializeField]
	private RectTransform sliderPrefab;

	private DemandKeeper demandKeeper;
	private Demand[] demands;
	private Dictionary<DemandType, SliderActor> sliderDict;

	private Demand currentDemand = null;

	void Start()
	{
		demandKeeper = FindObjectOfType<DemandKeeper>();
		FindObjectOfType<TimeManager>().tickTack += UpdateDemandsTime;

		demands = GetComponents<Demand>();
		CreateSliders();
	}

	private void CreateSliders()
	{
		sliderDict = new Dictionary<DemandType, SliderActor>();

		float deltaY = sliderPrefab.GetComponent<RectTransform>().sizeDelta.y;

		var offset = sliderPrefab.GetComponent<RectTransform>().sizeDelta;
		offset.y = deltaY * demands.Length;
		UIContainer.sizeDelta = offset;

		for (int i = 0; i < demands.Length; i++)
		{
			var slider = Instantiate(sliderPrefab);
			slider.transform.SetParent(UIContainer.transform);
			slider.anchoredPosition = new Vector3(0, -i * deltaY, 0);

			SliderActor actor = slider.GetComponent<SliderActor>();
			actor.SetEdgeValues(0, 1);
			actor.SetColor(demands[i].GetColor());
			actor.SetText(demands[i].DemandType.ToString());

			sliderDict[demands[i].DemandType] = actor;
		}
	}

	private void UpdateDemandsTime(float tick)
	{
		foreach(var item in demands)
		{
			item.TickTime(tick);
			sliderDict[item.DemandType].SetCurrentValues(item.GetRelativeLevel());
		}
	}

	public Demand GetHighestDemand()
	{
		Demand highestDemand = null;
		foreach(var demand in demands)
		{
			if(highestDemand == null)
			{
				highestDemand = demand;
			}

			if(demand.GetRelativeLevel() > highestDemand.GetRelativeLevel())
			{
				highestDemand = demand;
			}
		}

		return highestDemand;
	}

	public Transform GetDemandHolder(Demand demand)
	{
		currentDemand = demand;
		return demandKeeper.GetNearestDemandHolder(transform, demand.DemandType);
	}

	public void SatisfyDemandByHolder(Transform demandHolder)
	{
		float resource = demandKeeper.GetDemandResource(demandHolder);
		currentDemand.SatisfyDemand(resource, currentDemand.DemandType);
	}
}
