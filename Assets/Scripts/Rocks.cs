using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocks : MonoBehaviour, IPCInteractable
{
	public Sprite sprite;

	public GameObject GetGameObject()
	{
		return gameObject;
	}

	public void Interact()
	{
		Debug.Log("Interacting with rocks");
		InventoryManager.Instance.RegisterItem("stone", 10, sprite);
		// ItemObtainCanvasManager.Instance.ShowItemObtain("+10", sprite);
		// AssignmentManager.Instance.RegisterItem("stone", 10);
	}

	
}
