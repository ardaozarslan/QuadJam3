using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Singleton<Player>
{
	public PlayerActionControls controls;
	private Rigidbody rb;
	private InputManager inputManager;
	private Animator animator;

	public Transform cameraHolderTransform;
	public Transform cameraPos;
	public Transform cameraTransform;
	public Transform orientation;
	public Transform rootObject;

	[Header("Movement")]
	private float xRotation;
	private float yRotation;

	public float sensX = 30f;
	public float sensY = 30f;
	private float moveSpeed;
	public float walkSpeed = 5f;
	public float sprintSpeed = 10f;
	private Vector3 moveDirection;
	private float movementInputValue = 0f;

	public MovementState movementState;
	public enum MovementState
	{
		Idle,
		Walking,
		Sprinting
	}

	private void Awake()
	{
		controls = InputManager.Instance.controls;
		rb = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
		animator.SetFloat("moveDirection", 1f);
		rb.freezeRotation = true;
		inputManager = InputManager.Instance;
		movementState = MovementState.Idle;
		moveSpeed = walkSpeed;
	}

	private void OnEnable()
	{
		controls.Player.PlayerSprint.started += SprintAction;
		controls.Player.PlayerSprint.canceled += SprintAction;
		controls.Player.PlayerInteract.performed += InteractAction;
	}

	private void OnDisable()
	{
		controls.Player.PlayerSprint.started -= SprintAction;
		controls.Player.PlayerSprint.canceled -= SprintAction;
		controls.Player.PlayerInteract.performed -= InteractAction;
	}

	private void SprintAction(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			movementState = MovementState.Sprinting;
			moveSpeed = sprintSpeed;
		}
		else if (context.canceled)
		{
			if (movementInputValue > 0) {
				movementState = MovementState.Walking;
			}
			else {
				movementState = MovementState.Idle;
			}
			moveSpeed = walkSpeed;
		}
	}


	private void InteractAction(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			// shoot raycast
			RaycastHit hit;
			int layerMask = 1 << 8;
			// shoot a raycast that only hits colliders with the "Interactable" layer and also ignore triggers
			if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 3f, layerMask, QueryTriggerInteraction.Collide))
			{
				Debug.Log("Hit " + hit.collider.name);
				// get the interactable component from the object we hit
				IInteractable interactable = hit.collider.GetComponent<IInteractable>();
				// if the object we hit has an interactable component, call the interact method
				if (interactable != null)
				{
					interactable.Interact();
				}
			}


		}
	}

	void Start()
	{

	}


	private void Movement()
	{
		Vector2 inputVector = controls.Player.PlayerMovement.ReadValue<Vector2>();

		if (inputVector != Vector2.zero && movementState != MovementState.Sprinting)
		{
			movementState = MovementState.Walking;
		}


		moveDirection = rootObject.forward * inputVector.y + rootObject.right * inputVector.x;
		moveDirection.y = 0;
		moveDirection.Normalize();
		if (moveDirection.z < 0)
		{
			animator.SetFloat("moveDirection", -1f);
		}
		else
		{
			animator.SetFloat("moveDirection", 1f);
		}
		movementInputValue = inputVector.magnitude;

		float newVelocityX = Mathf.Lerp(rb.velocity.x, moveDirection.x * moveSpeed, 10f * Time.deltaTime);
		float newVelocityZ = Mathf.Lerp(rb.velocity.z, moveDirection.z * moveSpeed, 10f * Time.deltaTime);
		rb.velocity = new Vector3(newVelocityX, rb.velocity.y, newVelocityZ);

	}

	private void Look()
	{
		Vector2 inputVector = controls.Player.PlayerLook.ReadValue<Vector2>();

		yRotation += inputVector.x * sensX * Time.deltaTime;
		xRotation -= inputVector.y * sensY * Time.deltaTime;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		rootObject.rotation = Quaternion.Slerp(rootObject.rotation, Quaternion.Euler(0f, yRotation, 0f), 20f * Time.deltaTime);
		cameraHolderTransform.position = cameraPos.position;
		// orientation.rotation = Quaternion.Slerp(orientation.rotation, Quaternion.Euler(0f, yRotation, 0f), 20f * Time.deltaTime);
		cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, Quaternion.Euler(xRotation, yRotation, 0f), 20f * Time.deltaTime);
	}

	void Update()
	{
		Movement();
		Look();

		if (movementInputValue > 0)
		{
			if (movementState == MovementState.Sprinting)
			{
				animator.SetBool("isSprinting", true);
				// animator.SetBool("isWalking", false);
			}
			else if (movementState == MovementState.Walking)
			{
				animator.SetBool("isWalking", true);
				animator.SetBool("isSprinting", false);
			}
		}
		else
		{
			// Debug.Log("NE ALAKA???");
			animator.SetBool("isWalking", false);
			animator.SetBool("isSprinting", false);
		}
	}
}
