using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderActor : MonoBehaviour
{
	[SerializeField]
	private Slider slider;
	[SerializeField]
	private Image fillArea;
	[SerializeField]
	private Text title;

	public void SetColor(Color color)
	{
		fillArea.color = color;
	}

	public void SetEdgeValues(float minValue, float maxValue)
	{
		slider.minValue = minValue;
		slider.maxValue = maxValue;
	}

	public void SetCurrentValues(float value)
	{
		slider.value = value;
	}

	public void SetText(string str)
	{
		title.text = str;
	}
}
