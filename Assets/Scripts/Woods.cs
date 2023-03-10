using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woods : MonoBehaviour, IPCInteractable
{
	public Sprite sprite;

	public GameObject GetGameObject()
	{
		return gameObject;
	}

	public void Interact()
	{
		Debug.Log("Interacting with woods");
		InventoryManager.Instance.RegisterItem("stick", 10, sprite);
		// ItemObtainCanvasManager.Instance.ShowItemObtain("+10", sprite);
		// AssignmentManager.Instance.RegisterItem("stick", 10);
	}

	
}
