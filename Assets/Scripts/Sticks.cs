using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticks : MonoBehaviour, IPCInteractable
{
	public Sprite sprite;

	public GameObject GetGameObject()
	{
		return gameObject;
	}

	public void Interact()
	{
		Debug.Log("Interacting with sticks");
		InventoryManager.Instance.RegisterItem("stick", 1, sprite);
		// ItemObtainCanvasManager.Instance.ShowItemObtain("+1", sprite);
		// AssignmentManager.Instance.RegisterItem("stick", 1);
	}

	
}
