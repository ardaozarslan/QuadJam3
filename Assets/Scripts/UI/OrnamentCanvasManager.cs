using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OrnamentCanvasManager : Instanceton<OrnamentCanvasManager>
{
	public CanvasGroup ornamentCanvasGroup;

	private void Start() {
		ornamentCanvasGroup = GetComponent<CanvasGroup>();
	}

    public void ShowOrnamentCanvas()
	{
		ornamentCanvasGroup.DOFade(1, 0.2f);
	}

	public void HideOrnamentCanvas()
	{
		ornamentCanvasGroup.DOFade(0, 0.2f);
	}
}
