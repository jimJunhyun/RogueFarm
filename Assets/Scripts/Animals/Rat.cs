using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Rat : Animal
{
	public Products self;
    public new void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		onBeingSetTarget += FightOrFlight;
		state = States.Idle;
		prevStat = state;

		prevAtk = 0;
		curCalo = 0;

		agent.speed = speed;

		ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();
		for (int i = 0; i < particleSystems.Length; i++)
		{
			particleSystems[i].Stop();
		}
		stateEffPair.Add(States.Attack, particleSystems[0]);
		stateEffPair.Add(States.Chase, particleSystems[1]);
		stateEffPair.Add(States.Dead, particleSystems[2]);

		OnDayChanged += SelfReplicate;
	}

	private void SelfReplicate()
	{
		StartCoroutine(DelReplicate());
	}

	IEnumerator DelReplicate()
	{
		yield return null;
		ItemAdder.AddProduct(self, 1);
	}
}
