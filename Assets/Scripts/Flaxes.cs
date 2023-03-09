using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flaxes : MonoBehaviour, IPCInteractable
{
	public Sprite sprite;

	public GameObject GetGameObject()
	{
		return gameObject;
	}

	public void Interact()
	{
		Debug.Log("Interacting with flaxes");
		ItemObtainCanvasManager.Instance.ShowItemObtain("+1", sprite);
		AssignmentManager.Instance.RegisterItem("flax", 1);
	}

	
}
