using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollClamp : MonoBehaviour
{
	RectTransform tr;
	private void Awake()
	{
		tr = GetComponent<RectTransform>();
	}
	public void ClampThis(Vector2 delta)
	{
		Vector3 p = tr.anchoredPosition;
		p.x = Mathf.Clamp(p.x, -1000, 1000);
		tr.anchoredPosition = p;
	}
}
