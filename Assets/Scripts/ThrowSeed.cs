using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSeed : MonoBehaviour
{
	public float powerChargeSpeed = 1f;
	public float throwDistance = 3f;

	float chargedPower;

	Vector3 p;

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			chargedPower += powerChargeSpeed * Time.deltaTime;
			chargedPower = Mathf.Clamp(chargedPower, 0, 1);
		}
		else if (Input.GetMouseButtonUp(0))
		{
			Throw();
			chargedPower = 0;
		}
			
	}

	public void Throw()
	{
		Vector3 targetPos = Camera.main.transform.position+ Camera.main.transform.forward * chargedPower * throwDistance;
		Ray r = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
		Debug.DrawRay(r.origin, r.direction * chargedPower * throwDistance, Color.cyan, 1000f);
		RaycastHit hit;
		if (Physics.Raycast(r, out hit, chargedPower * throwDistance, ~(1 << 10)))
		{
			targetPos = hit.point;
		}
		Debug.DrawRay(targetPos, Vector3.down * 100, Color.cyan, 1000f);
		if (Physics.Raycast(targetPos, Vector3.down, out hit, 100f, 1 << 8))
		{
			targetPos = hit.point;
		}
		p = targetPos;

		if (Inventory.instance.UseItem(Inventory.instance.curSel.seed, 1))
		{
			Debug.Log("Thow");
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawSphere(p, 1);
	}
}
