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
			hits = Physics.RaycastAll(r, Mathf.Infinity, ~((1 << 8) | (1 << 10)), QueryTriggerInteraction.Collide);
			Debug.Log(hits.Length);
			if (hits.Length > 0)
			{
				ITradable tr = null;
				for (int i = 0; i < hits.Length; i++)
				{
					Debug.Log(hits[i].transform.name);
					if(hits[i].transform.TryGetComponent<ITradable>(out tr))
					{
						if(tr as GrowSeed != null && !(tr as GrowSeed).interactable)
							continue;
						tr.OnSell();
						CoinManager.instance.UIUpd();
						break;
					}
				}
			}
		}
    }
}
