using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using NaughtyAttributes;

public class GameManager : Singleton<GameManager>
{
	public int sceneNumber = 1;

	[ReorderableList]
	public List<string> playerJumpNegativeTexts = new List<string>();
	[ReorderableList]
	public List<string> playerJumpPositiveTexts = new List<string>();

	[ReorderableList]
	public List<string> narratorErrorTexts = new List<string>();

	public void LoadNextScene()
	{
		var pcManager = PCManager.Instance;
		foreach (var pc in pcManager.pcs)
		{
			if (pc.tweenToKill != null)
			{
				pc.tweenToKill.Kill();
			}
		}
		if (sceneNumber < 3) {
			sceneNumber++;
		}
		SceneManager.LoadScene("Scene1");
	}

	private void OnEnable()
	{
		SceneManager.sceneUnloaded += SceneManagerOnSceneUnloaded;
	}

	private void OnDisable()
	{
		SceneManager.sceneUnloaded -= SceneManagerOnSceneUnloaded;
	}

	private static void SceneManagerOnSceneUnloaded(Scene scene)
	{
		DOTween.KillAll();
	}
}
