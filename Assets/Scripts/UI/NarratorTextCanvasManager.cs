using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class NarratorTextCanvasManager : Instanceton<NarratorTextCanvasManager>
{
	public TextMeshProUGUI narratorTextObject;
	public CanvasGroup narratorTextCanvasGroup;

	private void Awake() {
		transform.localScale = Vector3.zero;
	}

	void Start()
	{
		narratorTextObject = GetComponentInChildren<TextMeshProUGUI>();
		narratorTextCanvasGroup = GetComponentInChildren<CanvasGroup>();
	}


	public void ShowNarratorText()
	{
		StartCoroutine(ShowNarratorTextCoroutine());
	}

	private IEnumerator ShowNarratorTextCoroutine()
	{
		narratorTextObject.text = "";
		transform.DOScale(1, 0.1f).SetEase(Ease.InQuad);

		// yield return new WaitForSeconds(2f);

		for (int i = 0; i < GameManager.Instance.narratorErrorTexts.Count; i++)
		{
			string currentNarratorText = GameManager.Instance.narratorErrorTexts[i];
			narratorTextObject.text = "";
			int soundPlayCount = Mathf.CeilToInt(currentNarratorText.Length * 2 / 3);
			StartCoroutine(PlaySoundCoroutine(soundPlayCount));

			// bool playSound = true;
			for (int j = 0; j < currentNarratorText.Length; j++)
			{
				narratorTextObject.text += currentNarratorText[j];
				yield return new WaitForSeconds(0.05f);
			}
			yield return new WaitForSeconds(2.5f);
		}

		// DOTween.To(() => narratorTextCanvasGroup.alpha, x => narratorTextCanvasGroup.alpha = x, 0, 0.2f).SetEase(Ease.InQuad);
		narratorTextCanvasGroup.alpha = 0;
		transform.DOScale(10, 1f).SetEase(Ease.Linear).OnComplete(() => GameManager.Instance.LoadNextScene());

	}

	private IEnumerator PlaySoundCoroutine(int soundPlayCount)
	{
		for (int i = 0; i < soundPlayCount; i++)
		{
			AudioManager.Instance.PlayRandomNarratorAudio(UnityEngine.Random.Range(0.5f, 1.2f));
			yield return new WaitForSeconds(0.075f);
		}
		yield return null;
	}

}
