using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;


    public int CurMoney;

	private void Awake()
	{
		instance = this;
	}
}
