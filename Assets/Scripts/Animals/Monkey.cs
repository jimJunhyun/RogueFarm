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
					if((found is Animal && (found as Animal).statSum <= statSum) || found is GrowSeed)
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
