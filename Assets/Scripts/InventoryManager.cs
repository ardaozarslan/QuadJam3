using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : Instanceton<InventoryManager>
{
	// add an action event
	public delegate void OnRegisterItem(string itemName, int itemCount, Sprite sprite = null);
	public static event OnRegisterItem onRegisterItem;

	public Dictionary<string, int> inventory = new Dictionary<string, int>() { { "berry", 0 }, { "flax", 0 }, { "stone", 0 }, { "stick", 0 }, {"ornament", 0} };


	// add a method to call the event
	public void RegisterItem(string itemName, int itemCount, Sprite sprite = null)
	{
		int oldCount = inventory[itemName];
		inventory[itemName] = Mathf.Min(inventory[itemName] + itemCount, 99999);

		if (inventory[itemName] == oldCount) return;
		onRegisterItem?.Invoke(itemName, itemCount, sprite);
	}

}