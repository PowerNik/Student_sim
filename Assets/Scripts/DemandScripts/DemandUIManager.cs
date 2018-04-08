using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemandUIManager : MonoBehaviour
{
	[SerializeField]
	private RectTransform UIContainer;
	[SerializeField]
	private RectTransform sliderPrefab;

	private Dictionary<DemandType, SliderActor> sliderDict;
	private DemandType highlightedDemandType = DemandType.Food;

	public void CreateSliders(Demand[] demands)
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

	public void SetDemandValue(DemandType type, float value)
	{
		sliderDict[type].SetCurrentValues(value);
	}

	public void HighlightOn(DemandType type)
	{
		highlightedDemandType = type;
		sliderDict[highlightedDemandType].HighlightSlider(true);
	}

	public void HighlightOff(DemandType type)
	{
		sliderDict[highlightedDemandType].HighlightSlider(false);
	}
}
