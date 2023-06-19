using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GrowStat
{
	Seed, //��
	Sapling, //����
	Plant, //ū Ǯ
	Tree, //����
}

[Serializable]
public struct GrowSteps
{
	public GrowStat state;
	public float proceedTime;
	public int calo;
	public int price;
}

public class GrowSeed : MonoBehaviour, IEatable, ITradable
{
	//public string entityName;

	public GrowStat curState;

	[Tooltip("�ð������� �� => �Ʒ�, ���� ���´� �ð� 0")]
	public List<GrowSteps> growSteps;
	int growIdx = 0;

	List<Transform> growVisuals = new List<Transform>();

	public Vector3 position => transform.position;

	public int type => ((int)FoodType.Herb);

	public Action<Animal> onBeingSetTarget { get; set; }
	public int price { get => curPrice; set => curPrice = value; }
	public float priceMult { get; set; } = 1;

	[SerializeField]
	int curPrice = 100;

	[HideInInspector]
	public bool interactable = true;

	public void OnEaten(Animal by)
	{
		Debug.Log(by.entityName + "���� ���� �Ĺ�");

		by.curCalo += growSteps[growIdx].calo;
		if(curState == GrowStat.Tree)
		{
			StartCoroutine(ReInterer());
		}
		else
		{
			Destroy(gameObject);
		}
		
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
			curState = growSteps[growIdx].state;
			curPrice = growSteps[growIdx].price;
			ChangeVisual();
		}
	}

	IEnumerator ReInterer()
	{
		interactable = false;
		gameObject.layer = 13;
		yield return new WaitForSeconds(growSteps[growIdx].proceedTime);
		gameObject.layer = 1;
		interactable = true;
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

	public void OnSell()
	{
		CoinManager.instance.CurMoney += (int)(curPrice * priceMult);

		if (curState == GrowStat.Tree)
		{
			StartCoroutine(ReInterer());
		}
		else
		{
			Destroy(gameObject);
		}
	}
		public void AddMult()
	{
		priceMult += 1;
	}
}
