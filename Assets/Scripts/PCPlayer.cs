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

	private void Awake()
	{
		controls = InputManager.Instance.controls;
		rb = GetComponent<Rigidbody2D>();
		inputManager = InputManager.Instance;
		foreach (GameObject go in interactableGameObjects)
		{
			interactables.Add(go.GetComponent<IPCInteractable>());
		}
	}

	private void OnEnable()
	{
		controls.FindAction($"{pc.actionMapName}Interact").performed += Interact;
	}

	private void OnDisable()
	{
		controls.FindAction($"{pc.actionMapName}Interact").performed -= Interact;
	}

	// Start is called before the first frame update
	void Start()
	{

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

	private void TPBack() {
		transform.position = tpTransform.position;
	}


	private void Movement()
	{
		// Debug.Log("Movement: " + controls.PC1Player.Movement.ReadValue<Vector2>());
		Vector2 inputVector = controls.FindAction($"{pc.actionMapName}Movement").ReadValue<Vector2>();
		float speed = 1f;
		float newVelocityX = Mathf.Lerp(rb.velocity.x, inputVector.x * speed, 10f * Time.deltaTime);
		float newVelocityY = Mathf.Lerp(rb.velocity.y, inputVector.y * speed, 10f * Time.deltaTime);
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
