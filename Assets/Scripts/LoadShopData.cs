using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadShopData : MonoBehaviour
{
	public static LoadShopData instance;

	List<ShopCardImg> cards = new List<ShopCardImg>();

	List<Products> plants;
	List<Products> animals;
	private void Awake()
	{
		instance = this;
		GetComponentsInChildren(cards);

		plants = new List<Products>(Resources.LoadAll<Products>("Products/Plant"));
		animals = new List<Products>(Resources.LoadAll<Products>("Products/Meat"));
	}

	private void Start()
	{
		OffShop();
	}

	public void OnShop()
	{
		gameObject.SetActive(true);
		ThrowSeed.instance.isThrowing = false;
		Time.timeScale = 0;
		LoadData(true);
	}

	public void OffShop()
	{
		gameObject.SetActive(false);
		ThrowSeed.instance.StartCoroutine(DelEnableThrow());
		Time.timeScale = 1;
	}

	public void LoadData(bool isPlant)
	{
		if (isPlant)
		{
			for (int i = 0; i < cards.Count; i++)
			{
				if(i >= plants.Count)
				{
					cards[i].DisableThis();
				}
				else
				{
					cards[i].EnableThis();
					cards[i].SetInfo(plants[i]);
				}
			}
		}
		else
		{
			for (int i = 0; i < cards.Count; i++)
			{
				if (i >= animals.Count)
				{
					cards[i].DisableThis();
				}
				else
				{
					cards[i].EnableThis();
					cards[i].SetInfo(animals[i]);
				}
			}
		}
	}

	IEnumerator DelEnableThrow()
	{
		yield	return null;
		ThrowSeed.instance.isThrowing = true;
	}
}
