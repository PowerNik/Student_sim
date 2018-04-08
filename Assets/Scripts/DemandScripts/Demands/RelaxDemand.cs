using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelaxDemand : Demand
{
	private void Awake()
	{
		demandType = DemandType.Relax;
	}
}
