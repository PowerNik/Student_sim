using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepDemand : Demand
{
	void Awake()
	{
		demandType = DemandType.Sleep;
	}
}
