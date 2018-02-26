using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemandKeeper : MonoBehaviour
{
	private Dictionary<DemandType, List<DemandHolder>> demandDict = 
		new Dictionary<DemandType, List<DemandHolder>>();

	public void AddDemand(DemandHolder demand, DemandType type)
	{
		if(!demandDict.ContainsKey(type))
		{
			demandDict[type] = new List<DemandHolder>();
		}

		demandDict[type].Add(demand);
	}

	public Transform GetNearestDemandHolder(Transform personPos, DemandType type)
	{
		float delta = 999999;
		Transform demandTransform = personPos;
		Transform temp;

		for (int i = 0; i < demandDict[type].Count; i++)
		{
			temp = demandDict[type][i].Transform;
			if ((temp.position - transform.position).sqrMagnitude < delta)
			{
				demandTransform = temp;
				delta = (demandTransform.position - transform.position).sqrMagnitude;
			}
		}

		return demandTransform;
	}

	public float GetDemandResource(Transform demandHolder)
	{
		return demandHolder.GetComponent<DemandHolder>().GetDemandResource();
	}

}
