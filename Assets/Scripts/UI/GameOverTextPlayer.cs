using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOverTextPlayer : Instanceton<GameOverTextPlayer>
{
	[ReorderableList]
	public List<string> startingPlayerTexts = new List<string>();

	public TextMeshProUGUI playerTextObject;
	public CanvasGroup playerTextCanvasGroup;

	public float waitSeconds = 0.5f;

	private void Awake()
	{
		// transform.localScale = Vector3.zero;
	}

	void Start()
	{
		playerTextObject = GetComponent<TextMeshProUGUI>();
		playerTextCanvasGroup = GetComponent<CanvasGroup>();

		Utils.WaitForSecondsAndInvoke(waitSeconds, () =>
		{
			ShowPlayerText();
		});
	}


	public void ShowPlayerText()
	{
		StartCoroutine(ShowPlayerTextCoroutine());
	}

	private IEnumerator ShowPlayerTextCoroutine()
	{
		playerTextObject.text = "";
		// transform.DOScale(1, 0.1f).SetEase(Ease.InQuad);

		// yield return new WaitForSeconds(2f);

		for (int i = 0; i < startingPlayerTexts.Count; i++)
		{
			string currentPlayerText = startingPlayerTexts[i];
			playerTextObject.text = "";
			int soundPlayCount = currentPlayerText.Length;
			if (currentPlayerText.IndexOf("<sprite=0>") != -1)
			{
				soundPlayCount -= 9;
			}
			if (currentPlayerText.IndexOf("/n") != -1)
			{
				soundPlayCount -= 2;
			}
			soundPlayCount = Mathf.CeilToInt(soundPlayCount * 2 / 3);

			StartCoroutine(PlaySoundCoroutine(soundPlayCount));

			// bool playSound = true;
			for (int j = 0; j < currentPlayerText.Length; j++)
			{
				if (currentPlayerText.IndexOf("<sprite=0>") == j)
				{
					j += 9;
					playerTextObject.text += "<sprite=0>";
				}
				else if (currentPlayerText.IndexOf("/n") == j)
				{
					j += 2;
					playerTextObject.text += "\n";
				}
				else
				{
					playerTextObject.text += currentPlayerText[j];
				}
				// playerTextObject.text += currentPlayerText[j];
				yield return new WaitForSeconds(0.05f);
			}
			yield return new WaitForSeconds(2.0f);
		}

		DOTween.To(() => playerTextCanvasGroup.alpha, x => playerTextCanvasGroup.alpha = x, 0, 1f).SetEase(Ease.InQuad).OnComplete(() => SceneManager.LoadScene("MainMenu"));
		// playerTextCanvasGroup.alpha = 0;
		// transform.DOScale(10, 1f).SetEase(Ease.Linear).OnComplete(() => GameManager.Instance.LoadNextScene());

	}

	private IEnumerator PlaySoundCoroutine(int soundPlayCount)
	{
		for (int i = 0; i < soundPlayCount; i++)
		{
			AudioManager.Instance.PlayNarratorAudio(1, 1f);
			yield return new WaitForSeconds(0.075f);
		}
		yield return null;
	}



}
