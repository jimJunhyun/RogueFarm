using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    public int CurMoney;

	TextMeshProUGUI coinTxt;

	private void Awake()
	{
		instance = this;
		coinTxt = GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>();

		coinTxt.text = CurMoney.ToString();
	}

	public void UIUpd()
	{
		coinTxt.text = CurMoney.ToString();
	}
}
