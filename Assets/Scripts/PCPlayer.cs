using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PCPlayer : MonoBehaviour
{
	public PlayerActionControls controls;
	private Rigidbody2D rb;
	private InputManager inputManager;

	public PC pc;
	public Transform tpTransform;

	[SerializeField]
	private List<GameObject> interactableGameObjects = new List<GameObject>();
	private List<IPCInteractable> interactables = new List<IPCInteractable>();

	private List<IPCInteractable> closeInteractables = new List<IPCInteractable>();
	private IPCInteractable closestInteractable;
	private IPCInteractable lastClosestInteractable;

	public float walkSpeed = 1f;
	public float sprintSpeed = 2f;
	private float moveSpeed;
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
		rb = GetComponent<Rigidbody2D>();
		inputManager = InputManager.Instance;
		foreach (GameObject go in interactableGameObjects)
		{
			interactables.Add(go.GetComponent<IPCInteractable>());
		}
		movementState = MovementState.Idle;
		moveSpeed = walkSpeed;
	}

	private void Start()
	{
		controls = InputManager.Instance.controls;
		controls.FindAction($"{pc.actionMapName}Interact").performed += Interact;
		controls.FindAction($"{pc.actionMapName}Sprint").started += SprintAction;
		controls.FindAction($"{pc.actionMapName}Sprint").canceled += SprintAction;
	}

	private void OnEnable()
	{

	}

	private void OnDisable()
	{
		controls.FindAction($"{pc.actionMapName}Interact").performed -= Interact;
		controls.FindAction($"{pc.actionMapName}Sprint").started -= SprintAction;
		controls.FindAction($"{pc.actionMapName}Sprint").canceled -= SprintAction;
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
			if (movementInputValue > 0)
			{
				movementState = MovementState.Walking;
			}
			else
			{
				movementState = MovementState.Idle;
			}
			moveSpeed = walkSpeed;
		}
	}


	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("PCInteractable"))
		{
			IPCInteractable interactable = other.GetComponent<IPCInteractable>();
			AddPointOfInterest(interactable);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("PCInteractable"))
		{
			IPCInteractable interactable = other.GetComponent<IPCInteractable>();
			RemovePointOfInterest(interactable);
		}
	}

	public void AddPointOfInterest(IPCInteractable interactable)
	{
		if (!closeInteractables.Contains(interactable))
		{
			closeInteractables.Add(interactable);
		}
	}

	public void RemovePointOfInterest(IPCInteractable interactable)
	{
		if (closeInteractables.Contains(interactable))
		{
			closeInteractables.Remove(interactable);
		}
	}

	private void Interact(InputAction.CallbackContext context)
	{
		if (closestInteractable != null)
		{
			closestInteractable.Interact();
			TPBack();
		}
		// inputManager.SwitchActionMap(inputManager.GetActionMap("Player"));
	}

	private void TPBack()
	{
		transform.position = tpTransform.position;
	}


	private void Movement()
	{
		// Debug.Log("Movement: " + controls.PC1Player.Movement.ReadValue<Vector2>());
		Vector2 inputVector = controls.FindAction($"{pc.actionMapName}Movement").ReadValue<Vector2>();
		movementInputValue = inputVector.magnitude;
		float newVelocityX = Mathf.Lerp(rb.velocity.x, inputVector.x * moveSpeed, 10f * Time.deltaTime);
		float newVelocityY = Mathf.Lerp(rb.velocity.y, inputVector.y * moveSpeed, 10f * Time.deltaTime);
		rb.velocity = new Vector2(newVelocityX, newVelocityY);
	}

	// Update is called once per frame
	void Update()
	{
		if (closestInteractable != null)
		{
			lastClosestInteractable = closestInteractable;
		}
		if (closeInteractables.Count > 0)
		{
			closestInteractable = closeInteractables[0];
			foreach (IPCInteractable interactable in closeInteractables)
			{
				if (Vector2.Distance(transform.position, interactable.GetGameObject().transform.position) < Vector2.Distance(transform.position, closestInteractable.GetGameObject().transform.position))
				{
					closestInteractable = interactable;
				}
			}
		}
		else
		{
			closestInteractable = null;
		}
		Movement();
	}
}
