using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ornament : MonoBehaviour, IInteractable
{
	public SigilTable sigilTable;
	public Collider interactTriggerCollider;
	public Chest chest;

	public Outline outline;

	private void Awake()
	{
		outline = gameObject.AddComponent<Outline>();
		outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
		outline.OutlineColor = Color.yellow;
		outline.OutlineWidth = 5f;
		outline.enabled = false;
	}

	private void Start()
	{
		sigilTable = FindObjectOfType<SigilTable>();
		chest = FindObjectOfType<Chest>();
		interactTriggerCollider = GetComponent<Collider>();
		interactTriggerCollider.enabled = false;
	}

	public void Interact()
	{
		Equip();
	}

	public void ResetTransform()
	{
		outline.enabled = false;
		transform.DOKill();
		transform.localPosition = new Vector3(0, 1, 0);
		transform.localRotation = Quaternion.identity;
		interactTriggerCollider.enabled = false;
	}

	public void Summon()
	{
		interactTriggerCollider.enabled = true;
		transform.DOLocalRotate(new Vector3(0, 180, 0), 3f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
		outline.enabled = true;
		transform.DOLocalMoveY(1.58f, 1.5f).OnComplete(() =>
		{
			transform.DOLocalMoveY(1.38f, 1.5f).SetLoops(-1, LoopType.Yoyo);
		});
	}

	public void Equip()
	{
		ResetTransform();
		Player.Instance.isHoldingOrnament = true;
		OrnamentCanvasManager.Instance.ShowOrnamentCanvas();
		chest.outline.enabled = true;
	}

	public void Dispose()
	{
		sigilTable.isCrafting = false;
	}
}
