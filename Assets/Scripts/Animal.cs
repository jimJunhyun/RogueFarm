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

public enum States
{
	None = -1,

	Idle,
	Escape,
	Chase,
	Attack,
	Dead,
}

public class Animal : MonoBehaviour, IEatable, ITradable
{
    protected IEatable target;

	public string entityName;
    public int hp;
    public int atk;
    public float speed;
	public float sightRad;
	public float atkRad;

	public float aSpd;

	public int statSum { get => hp + atk;}
	public float metabolism { get => (hp + atk + speed) * 5;}
	public float curCalo;


	protected FoodType foodType = FoodType.Game;

	public int type => ((int)foodType);

	public Vector3 position => transform.position;

	public Action<Animal> onBeingSetTarget { get; set; }

	protected Dictionary<States, ParticleSystem> stateEffPair = new Dictionary<States, ParticleSystem>();

	[SerializeField]
	Predation predateType = Predation.Herb;

	//[SerializeField]
	//List<GrowStat> edibleStat = new List<GrowStat>();

	public int predate => ((int)predateType);

	public int price { get => curPrice; set => curPrice = value; }
	public float priceMult { get; set; } = 1;

	public System.Action OnDayChanged;

	protected States prevStat;
	protected States state;

	protected NavMeshAgent agent;

	protected float prevAtk;

	[SerializeField]
	protected int curPrice = 0;

	const float AREALENGTH = 10f;

	public virtual void Awake()
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
		

		OnDayChanged += AddMult;
	}

	public virtual void OnEaten(Animal by)
	{
		Debug.Log(by.entityName + "에게 먹힘");
		by.curCalo += metabolism + curCalo;
	}

	public virtual void OnDead()
	{
		Debug.Log(entityName + "가 죽음");
		DayManager.instance.RemoveAnimal(this);
		Destroy(gameObject);
	}

	public virtual void Update()
	{
		if(state != States.Dead)
		{
			if (state == States.Escape)
			{
				//Debug.Log(entityName + " 가" + (target as Animal).entityName + "로부터 도망치는중");
				if (target is null || target == null || target.ToString() != "null")
				{
					Vector3 dest = transform.position - target.position;
					dest = transform.position + dest.normalized * AREALENGTH;
					NavMeshHit hit;
					NavMesh.SamplePosition(dest, out hit, AREALENGTH, NavMesh.AllAreas);
					agent.SetDestination(hit.position);
					if (dest.sqrMagnitude <= atkRad * atkRad)
					{
						ChangeState(States.Attack);
					}
				}
				else
					ChangeState(States.Idle);
			}
			else if (state == States.Chase)
			{
				//Debug.Log(entityName + " 가" + (target as Animal).entityName + "를 쫓는중");
				//Debug.Log((target is not null) + " || " + (target != null) + " || " + (target?.ToString()));
				if (target is not null && target != null && target?.ToString() != "null")
				{
					Vector3 dest = target.position - transform.position;
					NavMeshHit hit;
					NavMesh.SamplePosition(target.position, out hit, AREALENGTH, NavMesh.AllAreas);
					agent.SetDestination(hit.position);
					if (dest.sqrMagnitude <= atkRad * atkRad)
					{
						ChangeState(States.Attack);
					}
				}
				else
					ChangeState(States.Idle);
			}
			else if (state == States.Attack)
			{
				//Debug.Log("target : " + target + ", valid :" + (target.ToString() != "null"));
				if(target is null || target == null || target.ToString() == "null")
				{
					//Debug.Log(entityName + "이 사냥감을 먹었다.");
					agent.destination = transform.position;
					agent.isStopped = false;
					ChangeState(prevStat);
				}
				else
				{
					
					agent.isStopped = true;
					if (target.type == ((int)FoodType.Game))
					{
						Vector3 dest = target.position - transform.position;
						if (dest.sqrMagnitude <= atkRad * atkRad)
						{
							if ((Time.time - prevAtk) >= (1 / aSpd))
							{
								prevAtk = Time.time;

								Debug.Log(entityName + "가 " + (target as Animal).entityName + " 공격");
								(target as Animal).Damage(this);
							}
						}
						else
						{
							agent.isStopped = false;
							ChangeState(prevStat);
						}
					}
					else if (target.type == ((int)FoodType.Herb))
					{
						target.OnEaten(this);
						if(!(target as GrowSeed).interactable)
							target = null;
						agent.isStopped = false;
						ChangeState(prevStat);
					}
					else
					{
						agent.isStopped = false;
						ChangeState(prevStat);
					}
				}
			}
			else if (state == States.Idle)
			{
				if (SetTarget())
				{
					//Debug.Log(entityName + "이 찾았다 : " + (target as GrowSeed).entityName);
					ChangeState(States.Chase);
				}
				else if (agent.destination == transform.position || (agent.destination != transform.position && Arrived()))
				{
					Vector3 dir = UnityEngine.Random.insideUnitSphere;
					dir.y = 0;
					Vector3 dest = transform.position + dir.normalized * AREALENGTH;
					NavMeshHit hit;
					NavMesh.SamplePosition(dest, out hit, AREALENGTH, NavMesh.AllAreas);
					agent.SetDestination(hit.position);
				}
			}

		}
		else
		{
			OnDead();
		}
		
	}

	public virtual void Damage(Animal attacker)
	{
		hp -= attacker.atk;
		if(hp <= 0)
		{
			ChangeState(States.Dead);
			if ((attacker.predate == ((int)Predation.Omni) || type == attacker.predate))
			{
				OnEaten(attacker);
			}
		}
		
	}

	public virtual void FightOrFlight(Animal animal)
	{
		target = animal;
		if(animal.statSum > statSum)
		{
			ChangeState(States.Escape);
		}
		else
		{
			ChangeState(States.Chase);
		}
	}

	public virtual bool SetTarget()
	{
		List<Collider> col = new List<Collider>(Physics.OverlapSphere(transform.position, sightRad, ~(1 << 13), QueryTriggerInteraction.Collide));
		if(col.Count <= 1)
			return false;
		float minDist = sightRad * sightRad;
		bool foundSomething = false;
		for (int i = 0; i < col.Count; i++)
		{
			IEatable found = null;
			if(col[i].TryGetComponent<IEatable>(out found))
			{
				if (found as Animal == this)
					continue;
				if (predate == ((int)Predation.Omni) || predate == found.type)
				{
					if(found.type == ((int)FoodType.Herb) && !(found as GrowSeed).interactable)
						continue;

					float dist = (col[i].transform.position - transform.position).sqrMagnitude;
					if (dist < minDist)
					{
						minDist = dist;
						target = found;
						foundSomething = true;
					}
					//Debug.Log("PLANT");
				}	
			}
		}
		if(foundSomething)
		{
			target.onBeingSetTarget?.Invoke(this);
			return true;
		}
		return false;
	}

	public virtual bool CheckHunger()
	{
		return curCalo < metabolism;
	}

	public virtual void ResetCalorie()
	{
		curCalo = 0;
	}

	void ChangeState(States s)
	{
		prevStat = state;
		state = s;
		if (stateEffPair.ContainsKey(state))
		{
			if(prevStat == States.Attack && state == States.Chase)
			{

			}
			else
			{
				foreach (var item in stateEffPair.Keys)
				{
					if (item == state)
					{
						stateEffPair[item].Play();
					}
					else
					{
						stateEffPair[item].Stop();
					}
				}
			}
		}
	}

	bool Arrived()
	{
		if (!agent.pathPending)
		{
			if (agent.remainingDistance <= agent.stoppingDistance)
			{
				if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
				{
					return true;
				}
			}
		}
		return false;
	}

	public virtual void AddMult()
	{
		priceMult += 1;
	}

	public void OnSell()
	{
		CoinManager.instance.CurMoney += (int)(curPrice * priceMult);

		Destroy(gameObject);
	}
}
