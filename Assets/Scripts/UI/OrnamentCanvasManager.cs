using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OrnamentCanvasManager : Singleton<OrnamentCanvasManager>
{
	public CanvasGroup ornamentCanvasGroup;

    public void ShowOrnamentCanvas()
	{
		ornamentCanvasGroup.DOFade(1, 0.2f);
	}

	public void HideOrnamentCanvas()
	{
		ornamentCanvasGroup.DOFade(0, 0.2f);
	}
}
