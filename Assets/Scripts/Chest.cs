using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Chest : MonoBehaviour, IInteractable
{
	[SerializeField]
	private GameObject lid;

	public Collider interactTriggerCollider;
	public Ornament ornament;
	public SigilTable sigilTable;

	public Outline outline;

	public int ornamentCount = 0;
	private int assignmentOrnamentCount = 3;

	private void Awake()
	{
		interactTriggerCollider.enabled = false;

		outline = gameObject.AddComponent<Outline>();
		outline.OutlineMode = Outline.Mode.OutlineVisible;
		outline.OutlineColor = Color.yellow;
		outline.OutlineWidth = 5f;
		outline.enabled = false;
	}

	private void Start()
	{
		ornament = FindObjectOfType<Ornament>();
		sigilTable = FindObjectOfType<SigilTable>();
	}

	public void OpenChest()
	{
		lid.transform.DOLocalRotateQuaternion(Quaternion.Euler(-120, 0, 0), 1.0f).OnComplete(() =>
		{
			interactTriggerCollider.enabled = true;
		});
	}

	public void CloseChest()
	{
		interactTriggerCollider.enabled = false;
		lid.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), 1.0f).SetEase(Ease.InQuad).OnComplete(() =>
		{
			if (ornamentCount < assignmentOrnamentCount)
			{
				Utils.WaitForSecondsAndInvoke(1f, () =>
				{
					OpenChest();
				});
			}

		});
	}

	public void Interact()
	{
		if (!Player.Instance.isHoldingOrnament)
		{
			return;
		}
		outline.enabled = false;
		Debug.Log("Chest Interact " + UnityEngine.Random.Range(0, 100));
		OrnamentCanvasManager.Instance.HideOrnamentCanvas();
		Player.Instance.isHoldingOrnament = false;
		ornament.Dispose();
		if (ornamentCount < assignmentOrnamentCount)
		{
			InventoryManager.Instance.RegisterItem("ornament", 1);

		}
		ornamentCount++;
		CloseChest();
		if (ornamentCount < assignmentOrnamentCount)
		{
			sigilTable.ResetSigilTable();

		}
		else
		{
			Debug.Log("Game Over");
			NarratorTextCanvasManager.Instance.ShowSuccessText();
		}
	}
}
