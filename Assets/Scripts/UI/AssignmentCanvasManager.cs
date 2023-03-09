using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AssignmentCanvasManager : Singleton<AssignmentCanvasManager>
{

	public TextMeshProUGUI assignmentObject;

	private void Awake() {
		assignmentObject = GetComponentInChildren<TextMeshProUGUI>();
	}

	public void ShowAssignment(string assignmentText)
	{
		StartCoroutine(ShowAssignmentCoroutine(assignmentText));
	}

	private IEnumerator ShowAssignmentCoroutine(string assignmentText)
	{
		assignmentObject.text = "";
		Debug.Log(assignmentText.Length);
		for (int i = 0; i < assignmentText.Length; i++)
		{
			assignmentObject.text += assignmentText[i];
			yield return new WaitForSeconds(0.05f);
		}
		yield return null;
	}

	public void CompleteAssignment() {
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
			yield return new WaitForSeconds(0.03f);
		}
		yield return new WaitForSeconds(4f);

		if (AssignmentManager.Instance.CurrentAssignment != null)
		{
			AssignmentCanvasManager.Instance.ShowAssignment(AssignmentManager.Instance.CurrentAssignment.name);
		}
	}
	
}
