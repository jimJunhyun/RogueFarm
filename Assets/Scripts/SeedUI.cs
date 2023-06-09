using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SeedUI : MonoBehaviour
{
    Image curSdImg;
    TextMeshProUGUI curCntTxt;

    Image prevSdImg;
    TextMeshProUGUI prevCntTxt;

    Image postSdImg;
    TextMeshProUGUI postCntTxt;

	private void Awake()
	{
		Image[] images =  GetComponentsInChildren<Image>();
		TextMeshProUGUI[] texts =  GetComponentsInChildren<TextMeshProUGUI>();

		curSdImg = images[1];
		prevSdImg = images[3];
		postSdImg = images[5];

		curCntTxt = texts[0];
		prevCntTxt = texts[1];
		postCntTxt = texts[2];
	}

	private void Start()
	{
		Inventory.instance.AddUpdateBhv(UpdateUI);
		DisableContent();
	}

	public void UpdateUI()
	{
		if(Inventory.instance.curSel == null)
		{
			DisableContent();
		}
		else
		{
			curSdImg.sprite = Inventory.instance.curSel.seed.icon;
			curCntTxt.text = Inventory.instance.curSel.count.ToString();
			prevSdImg.sprite = Inventory.instance.prevSel.seed.icon;
			prevCntTxt.text = Inventory.instance.prevSel.count.ToString();
			postSdImg.sprite = Inventory.instance.postSel.seed.icon;
			postCntTxt.text = Inventory.instance.postSel.count.ToString();
		}
	}

	void DisableContent()
	{
		curSdImg.sprite = null;
		curCntTxt.text = "";
		prevSdImg.sprite = null;
		prevCntTxt.text = "";
		postSdImg.sprite = null;
		postCntTxt.text = "";
	}
}
