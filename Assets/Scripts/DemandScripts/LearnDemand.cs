using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnDemand : Demand
{
	void Awake()
	{
		demandType = DemandType.Learn;
	}
}
