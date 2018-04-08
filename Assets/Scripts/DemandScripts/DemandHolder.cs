using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NeedType
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
	private NeedType type = NeedType.Food;

	[SerializeField]
	[Range(0.1f, 10)]
	private float demandResource = 1;

	private Transform tr;

	void Start()
	{
		GetComponent<TextShowner>().text = type.ToString();
		tr = GetComponent<Transform>();
		FindObjectOfType<DemandKeeper>().RegisterDemand(this, type);
	}

	public Transform GetTransform
	{
		get { return tr; }
	}

	public NeedType GetDemandType
	{
		get { return type; }
	}

	public float GetDemandResource
	{
		get { return demandResource; }
	}
}

