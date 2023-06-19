using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayInform : MonoBehaviour
{
    TextMeshProUGUI dayTxt;

	public float informTime = 1.0f;
	
	private void Awake()
	{
		dayTxt = GameObject.Find("DayInformer").GetComponent<TextMeshProUGUI>();
		dayTxt.text = $"DAY {DayManager.instance.dayCount}";
		OffUI();
	}

	private void Start()
	{
		DayManager.instance.AddDayUpdBhv(() =>
		{
			dayTxt.text = $"DAY {DayManager.instance.dayCount}";
			StartCoroutine(DelayOnOff());
		});
	}

	IEnumerator DelayOnOff()
	{
		OnUI();
		yield return new WaitForSeconds(informTime);
		OffUI();
	}

	void OnUI()
	{
		dayTxt.text = $"DAY {DayManager.instance.dayCount}";
	}
	void OffUI()
	{
		dayTxt.text = $"";
	} 
}
