using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemandKeeper : MonoBehaviour
{
	private Dictionary<string, DemandHolder> demandDict = 
		new Dictionary<string, DemandHolder>();

	private Dictionary<DemandType, int> demandCountDict =
	new Dictionary<DemandType, int>();

	public void RegisterDemand(DemandHolder demand, DemandType type)
	{
		if(!demandCountDict.ContainsKey(type))
		{
			demandCountDict[type] = 0;
		}

		string demandID = type.ToString() + "_" + demandCountDict[type];
		demandDict[demandID] = demand;

		demandCountDict[type]++;
	}

	public string GetNearestDemandHolder(Transform personPos, DemandType type)
	{
		float delta = 999999;
		string demandID = "";

		foreach (var pair in demandDict)
		{
			if(pair.Value.GetDemandType != type)
			{
				continue;
			}

			float tempDelta = (pair.Value.GetTransform.position - personPos.position).sqrMagnitude;
			if (tempDelta < delta)
			{
				delta = tempDelta;
				demandID = pair.Key;
			}
		}

		return demandID;
	}

	public Transform GetDemandTransform(string demandID)
	{
		return demandDict[demandID].GetTransform;
	}

	public float GetDemandResource(string demandID)
	{
		return demandDict[demandID].GetDemandResource;
	}

	public DemandType GetDemandType(string demandID)
	{
		return demandDict[demandID].GetDemandType;
	}
}
