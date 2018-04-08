using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedForFood : Need
{
	void Awake()
	{
		needType = NeedType.Food;
	}
}
