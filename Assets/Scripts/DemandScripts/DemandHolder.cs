using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DemandType
{
	Food,
	Sleep,
	Learn,
	Relax,
	Cat
}

public class DemandHolder : MonoBehaviour
{
	[SerializeField]
	private DemandType type = DemandType.Food;

	[SerializeField]
	[Range(0.1f, 10)]
	private float demandResource = 1;

	private Transform tr;

	void Start()
	{
		tr = GetComponent<Transform>();
		GameObject.FindObjectOfType<DemandKeeper>().AddDemand(this, type);

		GetComponent<TextShowner>().text = type.ToString();
	}

	public Transform Transform
	{
		get { return tr; }
	}

	public float GetDemandResource()
	{
		return demandResource;
	}
}

