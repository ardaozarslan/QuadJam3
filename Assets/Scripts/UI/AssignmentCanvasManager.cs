using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AssignmentCanvasManager : Instanceton<AssignmentCanvasManager>
{

	public TextMeshProUGUI assignmentObject;

	private void Awake()
	{
		assignmentObject = GetComponentInChildren<TextMeshProUGUI>();
	}

	public void ShowAssignment(string assignmentText)
	{
		StartCoroutine(ShowAssignmentCoroutine(assignmentText));
		int soundPlayCount = Mathf.CeilToInt(assignmentText.Length * 2 / 3);
		StartCoroutine(PlaySoundCoroutine(soundPlayCount));
	}

	private IEnumerator ShowAssignmentCoroutine(string assignmentText)
	{
		assignmentObject.text = "";
		// bool playSound = true;
		for (int i = 0; i < assignmentText.Length; i++)
		{
			assignmentObject.text += assignmentText[i];
			// if (playSound) AudioManager.Instance.PlayRandomNarratorAudio();
			// playSound = !playSound;
			yield return new WaitForSeconds(0.05f);
		}
		yield return null;
	}

	private IEnumerator PlaySoundCoroutine(int soundPlayCount)
	{
		for (int i = 0; i < soundPlayCount; i++)
		{
			AudioManager.Instance.PlayRandomNarratorAudio();
			yield return new WaitForSeconds(0.075f);
		}
		yield return null;
	}

	public void CompleteAssignment()
	{
		StartCoroutine(CompleteAssignmentCoroutine());
	}

	private IEnumerator CompleteAssignmentCoroutine()
	{
		string completedAssignmentText = assignmentObject.text;
		int characterIndex = 0;
		while (characterIndex < completedAssignmentText.Length)
		{
			assignmentObject.text = "<s>" + completedAssignmentText.Substring(0, characterIndex + 1) + "</s>" + completedAssignmentText.Substring(characterIndex + 1);
			characterIndex++;
			yield return new WaitForSeconds(0.015f);
		}
		yield return new WaitForSeconds(1.0f);

		if (AssignmentManager.Instance.CurrentAssignment != null)
		{
			AssignmentCanvasManager.Instance.ShowAssignment(AssignmentManager.Instance.CurrentAssignment.name);
		}
	}

}
