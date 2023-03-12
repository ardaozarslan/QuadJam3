using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PlayerTextCanvasManager : Instanceton<PlayerTextCanvasManager>
{
	public TextMeshProUGUI playerTextObject;
	public CanvasGroup playerTextCanvasGroup;
	private IEnumerator newCoroutine;
	private IEnumerator soundCoroutine;

	private void Awake()
	{
		transform.localScale = Vector3.zero;
	}

	void Start()
	{
		playerTextObject = GetComponentInChildren<TextMeshProUGUI>();
		playerTextCanvasGroup = GetComponentInChildren<CanvasGroup>();
	}


	public void ShowPlayerText(string playerText)
	{
		if (newCoroutine != null) {
			StopCoroutine(newCoroutine);
		}
		newCoroutine = ShowPlayerTextCoroutine(playerText);
		StartCoroutine(newCoroutine);
	}

	private IEnumerator ShowPlayerTextCoroutine(string playerText)
	{
		playerTextObject.text = "";
		transform.DOKill();
		transform.DOScale(1, 0.1f).SetEase(Ease.InQuad);

		int soundPlayCount = Mathf.CeilToInt(playerText.Length * 2 / 3);
		if (soundCoroutine != null) {
			StopCoroutine(soundCoroutine);
		}
		soundCoroutine = PlaySoundCoroutine(soundPlayCount);
		StartCoroutine(soundCoroutine);

		// bool playSound = true;
		for (int j = 0; j < playerText.Length; j++)
		{
			playerTextObject.text += playerText[j];
			yield return new WaitForSeconds(0.05f);
		}
		yield return new WaitForSeconds(2.5f);
		transform.DOScale(0, 0.2f).SetEase(Ease.Linear);

	}

	private IEnumerator PlaySoundCoroutine(int soundPlayCount)
	{
		for (int i = 0; i < soundPlayCount; i++)
		{
			AudioManager.Instance.PlayRandomNarratorAudio(1f);
			yield return new WaitForSeconds(0.075f);
		}
		yield return null;
	}

}
