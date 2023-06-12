using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHairUI : MonoBehaviour
{
    Image fill;
	private void Awake()
	{
		fill = GameObject.Find("Charge").GetComponent<Image>();
	}

	private void Start()
	{
		ThrowSeed.instance.AddCharged(FillChange);
	}

	void FillChange(float val)
	{
		fill.fillAmount = val;
	}
}
