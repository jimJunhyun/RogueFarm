using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopCardImg : MonoBehaviour
{
    public Products info;

	Image Icon;
	TextMeshProUGUI price;

	private void Awake()
	{
		Image[] img = GetComponentsInChildren<Image>();
		for (int i = 0; i < img.Length; i++)
		{
			if(img[i].name == "Icon")
			{
				Icon = img[i];
				break;
			}
		}
		TextMeshProUGUI[] tmp = GetComponentsInChildren<TextMeshProUGUI>();
		for (int i = 0; i < tmp.Length; i++)
		{
			if (tmp[i].name == "Price")
			{
				price = tmp[i];
				break;
			}
		}

	}

	public void SetInfo(Products p)
	{
		info = p;
		Icon.sprite = p.icon;
		price.text = p.myPrice.ToString();
	}

	public void DisableThis()
	{
		gameObject.SetActive(false);
	}

	public void EnableThis()
	{
		gameObject.SetActive(true);
	}

	public void Purchase()
	{
		if(CoinManager.instance.CurMoney >= info.myPrice)
		{
			CoinManager.instance.CurMoney -= info.myPrice;
			CoinManager.instance.UIUpd();
			ItemAdder.AddProduct(info, 1);
		}
	}
}
