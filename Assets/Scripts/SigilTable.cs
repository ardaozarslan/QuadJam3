using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;

public class SigilTable : MonoBehaviour
{
	[ReorderableList]
	public List<Sigil> sigils = new List<Sigil>();

	public Ornament ornament;

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
		Utils.WaitForSecondsAndInvoke(0.1f, () =>
		{
			ornament.Summon();
		});


	}

	public void ActivateSigilOutlines() {
		foreach (Sigil sigil in sigils)
		{
			sigil.outline.enabled = true;
		}
	}

	public void ResetSigilTable()
	{
		foreach (Sigil sigil in sigils)
		{
			sigil.ResetSigil();
		}
	}
}
