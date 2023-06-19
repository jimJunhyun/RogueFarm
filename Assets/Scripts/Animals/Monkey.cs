using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : Animal
{
	public override bool SetTarget()
	{
		List<Collider> col = new List<Collider>(Physics.OverlapSphere(transform.position, sightRad, Physics.AllLayers, QueryTriggerInteraction.Collide));
		if (col.Count <= 1)
			return false;
		float minDist = sightRad * sightRad;
		bool foundSomething = false;
		for (int i = 0; i < col.Count; i++)
		{
			IEatable found = null;
			if (col[i].TryGetComponent<IEatable>(out found))
			{
				if (found as Animal == this)
					continue;
				if ((predate == ((int)Predation.Omni) || predate == found.type))
				{
					if (found.type == ((int)FoodType.Herb) && !(found as GrowSeed).interactable)
						continue;
					if ((found.type == ((int)FoodType.Game) && (found as Animal).statSum <= statSum) || ((found.type == ((int)FoodType.Herb)) && (found as GrowSeed).interactable))
					{
						float dist = (col[i].transform.position - transform.position).sqrMagnitude;
						if (dist < minDist)
						{
							minDist = dist;
							target = found;
							foundSomething = true;
						}
					}
				}
			}
		}
		if (foundSomething)
		{
			target.onBeingSetTarget?.Invoke(this);
			return true;
		}
		return false;
	}
}
