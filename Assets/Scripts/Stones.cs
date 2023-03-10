using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stones : MonoBehaviour, IPCInteractable
{
	public Sprite sprite;

	public GameObject GetGameObject()
	{
		return gameObject;
	}

	public void Interact()
	{
		Debug.Log("Interacting with stones");
		InventoryManager.Instance.RegisterItem("stone", 1, sprite);
		// ItemObtainCanvasManager.Instance.ShowItemObtain("+1", sprite);
		// AssignmentManager.Instance.RegisterItem("stone", 1);
	}

	
}
