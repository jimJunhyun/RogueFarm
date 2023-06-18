using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seller : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(1))
		{
            Ray r = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            RaycastHit[] hits;
            if((hits = Physics.RaycastAll(r, Mathf.Infinity, ~((1 << 8) | (1 << 10)))).Length > 0)
			{
				ITradable tr = null;
				for (int i = 0; i < hits.Length; i++)
				{
					Debug.Log(hits[i].transform.name);
					if(hits[i].transform.TryGetComponent<ITradable>(out tr))
					{
						tr.OnSell();
						break;
					}
				}
			}
		}
    }
}
