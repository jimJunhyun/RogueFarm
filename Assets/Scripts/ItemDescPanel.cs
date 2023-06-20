using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemDescPanel : MonoBehaviour
{
	public static ItemDescPanel instance;
    TextMeshProUGUI informat;

	private void Awake()
	{
		instance = this;
		informat = GameObject.Find("InfoDescTxt").GetComponent<TextMeshProUGUI>();
		OffUI();
	}

	public void OnUI(string txt)
	{
		gameObject.SetActive(true);
		informat.text = txt;
	}

	public void OffUI()
	{
		gameObject.SetActive(false);
	}
}
