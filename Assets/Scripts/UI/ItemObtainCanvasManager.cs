using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemObtainCanvasManager : Instanceton<ItemObtainCanvasManager>
{

	public GameObject itemObtainPrefab;

	private void OnEnable() {
		InventoryManager.onRegisterItem += ShowItemObtain;
	}

	private void OnDisable() {
		InventoryManager.onRegisterItem -= ShowItemObtain;
	}

	public void ShowItemObtain(string itemName, int itemCount, Sprite itemSprite)
	{
		if (itemName == "ornament") return;
		string shownItemText = "+" + itemCount.ToString();
		GameObject itemObtainObject = Instantiate(itemObtainPrefab, transform);
		itemObtainObject.GetComponentInChildren<TextMeshProUGUI>().text = shownItemText;
		itemObtainObject.GetComponentInChildren<Image>().sprite = itemSprite;
		StartCoroutine(DestroyItemObtain(itemObtainObject));
		
	}

	private IEnumerator DestroyItemObtain(GameObject itemObtainObject)
	{
		float moveTime = 1.5f;
		float moveAmount = 150f;
		float startPosition = itemObtainObject.transform.position.y;
		CanvasGroup canvasGroup = itemObtainObject.GetComponent<CanvasGroup>();
		float timer = 0;
		while (timer < moveTime)
		{
			timer += Time.deltaTime;
			itemObtainObject.transform.position = new Vector3(itemObtainObject.transform.position.x, startPosition + moveAmount * (timer / moveTime), itemObtainObject.transform.position.z);
			canvasGroup.alpha = 1 - (timer / moveTime);
			yield return null;
		}
		Destroy(itemObtainObject);
	}
	
}
