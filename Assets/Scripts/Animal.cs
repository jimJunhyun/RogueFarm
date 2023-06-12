using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Predation
{
	None = -1,
	Herb,
	Carn,
	Omni,
}

public class Animal : MonoBehaviour, IEatable
{
    protected IEatable target;
    public int hp;
    public int atk;
    public float speed;
	public float sightRad;

	public int statSum { get => hp + atk;}

	protected int foodType;

	public int type => foodType;

	public Vector3 position => transform.position;

	public Action<Animal> onBeingSetTarget { get; set; }

	public int predate = ((int)Predation.None);

	

	NavMeshAgent agent;

	private void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	public void OnEaten(Animal by)
	{
		Debug.Log("ธิศ๛");
	}

	public void Damage(Animal attacker)
	{
		hp -= attacker.atk;
		if(hp <= 0)
			OnEaten(attacker);
	}

	public void Predate()
	{
		agent.SetDestination(target.position);
	}
	
	public void FightOrFlight(Animal animal)
	{
		if(animal.statSum > statSum)
		{
			//flight
		}
		else
		{
			//fight
		}

	}

	public void SetTarget()
	{
		List<Collider> col = new List<Collider>(Physics.OverlapSphere(transform.position, sightRad));
		float minDist = sightRad * sightRad;
		for (int i = 0; i < col.Count; i++)
		{
			IEatable found = col[i].GetComponent<IEatable>();
			if(found != null)
			{
				if(predate == ((int)Predation.Omni) || predate == found.type)
				{
					float dist = (col[i].transform.position - transform.position).sqrMagnitude;
					if (dist < minDist)
					{
						minDist = dist;
						target = found;
					}
				}
			}
		}
		target.onBeingSetTarget.Invoke(this);
	}
}
