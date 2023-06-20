using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCursor : MonoBehaviour
{
	private void Awake()
	{

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		

	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			OnCursor();
			LoadShopData.instance.OnShop();
		}
	}

	public void OnCursor()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible= true;
	}

	public void OffCursor()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
}
