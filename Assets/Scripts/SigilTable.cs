using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;

public class SigilTable : MonoBehaviour
{
	[ReorderableList]
	public List<Sigil> sigils = new List<Sigil>();

	public GameObject ornament;

	public bool isCrafting = false;

	public bool CheckAllowSigilPress()
	{
		bool allowSigilPress = true;
		foreach (Sigil sigil in sigils)
		{
			if (!sigil.isActivated)
			{
				return false;
			}
		}
		return allowSigilPress;
	}

	public void CheckCraft()
	{
		if (isCrafting)
		{
			return;
		}
		foreach (Sigil sigil in sigils)
		{
			if (!sigil.isPressed)
			{
				return;
			}
		}

		Craft();
	}

	private void Craft()
	{
		isCrafting = true;
		Utils.WaitForSecondsAndInvoke(0.5f, () =>
		{
			ornament.transform.DOLocalRotate(new Vector3(0, 180, 0), 3f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
			ornament.transform.DOLocalMoveY(1.58f, 3f).OnComplete(() =>
			{
				ornament.transform.DOLocalMoveY(1.38f, 1.5f).SetLoops(-1, LoopType.Yoyo);

			});
		});


	}
}
