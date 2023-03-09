using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berries : MonoBehaviour, IPCInteractable
{
	public Sprite sprite;

	public GameObject GetGameObject()
	{
		return gameObject;
	}

	public void Interact()
	{
		Debug.Log("Interacting with berries");
		ItemObtainCanvasManager.Instance.ShowItemObtain("+1", sprite);
		AssignmentManager.Instance.RegisterItem("berry", 1);
	}

	
}
