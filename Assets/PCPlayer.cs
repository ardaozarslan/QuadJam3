using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCPlayer : MonoBehaviour
{
	public PlayerActionControls controls;
	private Rigidbody2D rb;

	private void Awake()
	{
		controls = new PlayerActionControls();
		controls.PC1Player.Enable();
		rb = GetComponent<Rigidbody2D>();
	}

	// Start is called before the first frame update
	void Start()
	{

	}


	private void Movement()
	{
		Debug.Log("Movement: " + controls.PC1Player.Movement.ReadValue<Vector2>());

		Vector2 inputVector = controls.PC1Player.Movement.ReadValue<Vector2>();
		float speed = 1f;
		float newVelocityX = Mathf.Lerp(rb.velocity.x, inputVector.x * speed, 10f * Time.deltaTime);
		float newVelocityY = Mathf.Lerp(rb.velocity.y, inputVector.y * speed, 10f * Time.deltaTime);
		rb.velocity = new Vector2(newVelocityX, newVelocityY);
		// rb.velocity = Vector3.Lerp(rb.velocity, new Vector3(inputVector.x, rb.velocity.y, inputVector.y) * speed, 10f * Time.deltaTime);

		// if (inputVector != Vector2.zero)
		// {
		// 	transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(inputVector.x, 0f, inputVector.y)), 10f * Time.deltaTime);
		// }

		// lastInputVector = inputVector;
	}

	// Update is called once per frame
	void Update()
	{
		Movement();
	}
}
