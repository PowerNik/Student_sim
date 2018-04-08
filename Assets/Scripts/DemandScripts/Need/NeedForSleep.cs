using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedForSleep : Need
{
	void Awake()
	{
		needType = NeedType.Sleep;
	}
}
