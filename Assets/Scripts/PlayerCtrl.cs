using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCtrl : MonoBehaviour
{
	public float speed =  5f;
	public float rotSpeed =  150f;

	public float camYSpeed = 15f;
	public float camYUpperBound = 3f;
	public float camYLowerBound = -3f;

	public Transform lookPos;

    CharacterController ctrl;

	float inpX;
	float inpY;
	float mouseX;
	float mouseY;

	private void Awake()
	{
		ctrl = GetComponent<CharacterController>();
	}
	// Update is called once per frame
	void Update()
    {
        inpX = Input.GetAxis("Horizontal");
		inpY = Input.GetAxis("Vertical");
		mouseX = Input.GetAxis("Mouse X");
		mouseY = Input.GetAxis("Mouse Y");

		transform.Rotate(Vector3.up * rotSpeed * mouseX * Time.deltaTime);
		ctrl.Move((transform.forward * inpY + transform.right * inpX) * speed * Time.deltaTime);
		
		float offsetY = Mathf.Clamp(lookPos.position.y + (camYSpeed * mouseY * Time.deltaTime), camYLowerBound, camYUpperBound);

		lookPos.position = new Vector3(lookPos.position.x, offsetY, lookPos.position.z);
    }
}
