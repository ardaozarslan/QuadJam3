using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MaterialsCanvasManager : Instanceton<MaterialsCanvasManager>
{

	public GameObject berryText;
	public GameObject flaxText;
	public GameObject stoneText;
	public GameObject stickText;

	private void Awake()
	{
		berryText.GetComponent<CanvasGroup>().alpha = 0;
		flaxText.GetComponent<CanvasGroup>().alpha = 0;
		stoneText.GetComponent<CanvasGroup>().alpha = 0;
		stickText.GetComponent<CanvasGroup>().alpha = 0;
	}
	private void OnEnable()
	{
		InventoryManager.onRegisterItem += UpdateInventoryTexts;
	}

	private void OnDisable()
	{
		InventoryManager.onRegisterItem -= UpdateInventoryTexts;
	}

	public void UpdateInventoryTexts(string itemName, int itemCount, Sprite itemSprite = null)
	{
		switch (itemName)
		{
			case "berry":
				berryText.GetComponentInChildren<TextMeshProUGUI>().text = "x" + InventoryManager.Instance.inventory["berry"].ToString();
				break;
			case "flax":
				flaxText.GetComponentInChildren<TextMeshProUGUI>().text = "x" + InventoryManager.Instance.inventory["flax"].ToString();
				break;
			case "stone":
				stoneText.GetComponentInChildren<TextMeshProUGUI>().text = "x" + InventoryManager.Instance.inventory["stone"].ToString();
				break;
			case "stick":
				stickText.GetComponentInChildren<TextMeshProUGUI>().text = "x" + InventoryManager.Instance.inventory["stick"].ToString();
				break;
			default:
				break;
		}
	}



}
