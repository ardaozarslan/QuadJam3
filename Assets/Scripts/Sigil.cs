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

	private SigilTable sigilTable;

	public bool isActivated = false;
	public bool isPressed = false;

	private void Awake()
	{
		mainCollider.enabled = false;
		inhibitorCollider.enabled = false;
	}

	private void Start()
	{
		sigilTable = FindObjectOfType<SigilTable>();
	}

	public void ActivateSigil()
	{
		mainCollider.enabled = true;
		inhibitorCollider.enabled = true;
		isActivated = true;
	}

	public void Interact()
	{
		if (isActivated && !isPressed && sigilTable.CheckAllowSigilPress())
		{
			isPressed = true;
			gameObject.transform.DOLocalMoveZ(0.75f, 1f);
			gameObject.transform.DOLocalRotateQuaternion(Quaternion.Euler(-90, 0, -180), 1.0f);
			sigilTable.CheckCraft();
		}
	}
}
