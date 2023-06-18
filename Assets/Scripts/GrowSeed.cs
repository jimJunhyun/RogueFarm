using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GrowStat
{
	Seed, //씨
	Bush, //작은 풀
	Plant, //큰 풀
	Tree, //관목
}

[Serializable]
public struct GrowSteps
{
	public GrowStat state;
	public float proceedTime;
}

public class GrowSeed : MonoBehaviour, IEatable
{
	public string entityName;

	[Tooltip("시간순으로 위 => 아래")]
	public List<GrowSteps> growSteps;
	

	public Vector3 position => transform.position;

	public int type => ((int)FoodType.Herb);

	public Action<Animal> onBeingSetTarget { get; set; }

	public void OnEaten(Animal by)
	{
		Debug.Log(by.entityName + "에게 먹힌 식물");
	}

	void Awake()
    {
        
    }
}
