using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatDemand : Demand
{
	private void Awake()
	{
		demandType = DemandType.Cat;
	}
}
