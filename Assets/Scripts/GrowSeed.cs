using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GrowStat
{
	Seed, //씨
	Sapling, //묘목
	Plant, //큰 풀
	Tree, //관목
}

[Serializable]
public struct GrowSteps
{
	public GrowStat state;
	public float proceedTime;
	public int calo;
}

public class GrowSeed : MonoBehaviour, IEatable
{
	//public string entityName;

	public GrowStat curState;

	[Tooltip("시간순으로 위 => 아래, 최종 상태는 시간 0")]
	public List<GrowSteps> growSteps;
	int growIdx = 0;

	List<Transform> growVisuals = new List<Transform>();

	public Vector3 position => transform.position;

	public int type => ((int)FoodType.Herb);

	public Action<Animal> onBeingSetTarget { get; set; }

	public void OnEaten(Animal by)
	{
		Debug.Log(by.entityName + "에게 먹힌 식물");

		by.curCalo += growSteps[growIdx].calo;
		Destroy(gameObject);
	}

	void Awake()
    {
		curState = GrowStat.Seed;
		growIdx = 0;
		gameObject.GetComponentsInChildren(growVisuals);
		growVisuals.Remove(transform);
		ChangeVisual();
        StartCoroutine(Grower());
    }

	IEnumerator Grower()
	{
		for (; growIdx < growSteps.Count - 1; )
		{
			yield return new WaitForSeconds(growSteps[growIdx].proceedTime);
			growIdx += 1;
			ChangeVisual();
		}
	}

	void ChangeVisual()
	{
		for (int i = 0; i < growVisuals.Count; i++)
		{
			if(i == growIdx)
			{
				growVisuals[i].gameObject.SetActive(true);
			}
			else
			{
				growVisuals[i].gameObject.SetActive(false);
			}
		}
	}
}
