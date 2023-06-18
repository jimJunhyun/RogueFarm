using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ThrowSeed : MonoBehaviour
{
	public static ThrowSeed instance;

	public float powerChargeSpeed = 1f;
	public float throwDistance = 3f;

	public GameObject seedSown;

	System.Action<float> onCharged;

	float chargedPower;

	private void Awake()
	{
		instance = this;
	}

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			chargedPower += powerChargeSpeed * Time.deltaTime;
			chargedPower = Mathf.Clamp(chargedPower, 0, 1);
			onCharged.Invoke(chargedPower);
		}
		else if (Input.GetMouseButtonUp(0))
		{
			Throw();
			chargedPower = 0;
			onCharged.Invoke(chargedPower);
		}
			
	}

	public void Throw()
	{
		Vector3 targetPos = Camera.main.transform.position+ Camera.main.transform.forward * chargedPower * throwDistance;
		Ray r = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
		RaycastHit hit;
		if (Physics.Raycast(r, out hit, chargedPower * throwDistance, ~(1 << 10)))
		{
			targetPos = hit.point;
		}
		if (Physics.Raycast(targetPos, Vector3.down, out hit, 100f, 1 << 8))
		{
			targetPos = hit.point;
		}
		targetPos.y += 0.5f;
		Inventory.instance.UseItem(Inventory.instance.curSel?.seed, 1, ()=>{

			seedSown = Inventory.instance.curSel?.seed.prefab;
			Instantiate(seedSown, targetPos, Quaternion.Euler(90, 0, 0));

		});
			
	}

	public void AddCharged(System.Action<float> onUpd)
	{
		onCharged += onUpd;
	}

}
