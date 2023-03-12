using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sigil : MonoBehaviour, IInteractable
{
	[SerializeField]
	private Collider mainCollider;
	[SerializeField]
	private Collider inhibitorCollider;

	private Chest chest;

	private SigilTable sigilTable;

	public bool isActivated = false;
	public bool isPressed = false;

	public Outline outline;

	private void Awake()
	{
		mainCollider.enabled = false;
		inhibitorCollider.enabled = false;

		outline = gameObject.AddComponent<Outline>();
		outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
		Color outlineColor = new Color();
		ColorUtility.TryParseHtmlString("#07FAFE", out outlineColor);
		outline.OutlineColor = outlineColor;
		outline.OutlineWidth = 5f;
		// DOTween.To(() => outline.OutlineWidth, x => outline.OutlineWidth = x, 1f, 3f).SetLoops(-1, LoopType.Yoyo).SetDelay(UnityEngine.Random.Range(0f, 3f));
		// Color tweenColor = new Color();
		// ColorUtility.TryParseHtmlString("#07FAFE", out tweenColor);
		// tweenColor.a = 0.5f;
		// DOTween.To(() => outline.OutlineColor, x => outline.OutlineColor = x, tweenColor, 2f).SetLoops(-1, LoopType.Yoyo).SetDelay(UnityEngine.Random.Range(0f, 3f));
		outline.enabled = false;
	}

	private void Start()
	{
		sigilTable = FindObjectOfType<SigilTable>();
		chest = FindObjectOfType<Chest>();
	}

	public void ActivateSigil()
	{
		mainCollider.enabled = true;
		inhibitorCollider.enabled = true;
		isActivated = true;
		if (sigilTable.CheckAllowSigilPress())
		{
			chest.OpenChest();
			sigilTable.ActivateSigilOutlines();
		}
	}

	public void ResetSigil()
	{
		isPressed = false;
		mainCollider.enabled = false;
		inhibitorCollider.enabled = false;
		gameObject.transform.DOLocalMoveZ(0.8f, 1f);
		gameObject.transform.DOLocalRotateQuaternion(Quaternion.Euler(90, 0, 0), 1.0f).OnComplete(() =>
		{
			outline.enabled = true;
			mainCollider.enabled = true;
			inhibitorCollider.enabled = true;
		});

	}

	public void Interact()
	{
		if (isActivated && !isPressed && sigilTable.CheckAllowSigilPress())
		{
			isPressed = true;
			outline.enabled = false;
			gameObject.transform.DOLocalMoveZ(0.75f, 1f);
			gameObject.transform.DOLocalRotateQuaternion(Quaternion.Euler(-90, 0, -180), 1.0f);
			sigilTable.CheckCraft();
		}
	}
}
