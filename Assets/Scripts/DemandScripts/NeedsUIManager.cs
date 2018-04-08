using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeedsUIManager : MonoBehaviour
{
	[SerializeField]
	private RectTransform UIContainer;
	[SerializeField]
	private RectTransform sliderPrefab;

	private Dictionary<NeedType, SliderActor> sliderDict;
	private NeedType highlightedNeedType = NeedType.Food;

	public void CreateSliders(Need[] needs)
	{
		sliderDict = new Dictionary<NeedType, SliderActor>();

		float deltaY = sliderPrefab.GetComponent<RectTransform>().sizeDelta.y;
		var offset = sliderPrefab.GetComponent<RectTransform>().sizeDelta;
		offset.y = deltaY * needs.Length;
		UIContainer.sizeDelta = offset;

		for (int i = 0; i < needs.Length; i++)
		{
			var slider = Instantiate(sliderPrefab);
			slider.transform.SetParent(UIContainer.transform);
			slider.anchoredPosition = new Vector3(0, -i * deltaY, 0);

			SliderActor actor = slider.GetComponent<SliderActor>();
			actor.SetEdgeValues(0, 1);
			actor.SetColor(needs[i].GetColor());
			actor.SetText(needs[i].NeedType.ToString());

			sliderDict[needs[i].NeedType] = actor;
		}
	}

	public void SetNeedValue(NeedType type, float value)
	{
		sliderDict[type].SetCurrentValues(value);
	}

	public void HighlightOn(NeedType type)
	{
		highlightedNeedType = type;
		sliderDict[highlightedNeedType].HighlightSlider(true);
	}

	public void HighlightOff(NeedType type)
	{
		sliderDict[highlightedNeedType].HighlightSlider(false);
	}
}
