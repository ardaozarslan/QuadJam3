using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Assignment
{

	public string name;
	public string description;
	public bool completed;
	public bool active;
	public bool isMain;
	public bool isSide;
	public string itemRegisterName1;
	public int itemRegisterAmount1;
	public string itemRegisterName2;
	public int itemRegisterAmount2;

	public Assignment(string name, string description, bool completed, bool active, bool isMain, bool isSide, string itemRegisterName1, string itemRegisterName2, int itemRegisterAmount1, int itemRegisterAmount2)
	{
		this.name = name;
		this.description = description;
		this.completed = completed;
		this.active = active;
		this.isMain = isMain;
		this.isSide = isSide;
		this.itemRegisterName1 = itemRegisterName1;
		this.itemRegisterName2 = itemRegisterName2;
		this.itemRegisterAmount1 = itemRegisterAmount1;
		this.itemRegisterAmount2 = itemRegisterAmount2;
	}
}

public class AssignmentManager : Singleton<AssignmentManager>
{
	[ReorderableList]
	public List<AssignmentSO> assignmentSOs = new List<AssignmentSO>();
	public List<Assignment> assignments = new List<Assignment>();

	public Assignment CurrentAssignment
	{
		get
		{
			for (int i = 0; i < assignments.Count; i++)
			{
				if (assignments[i].active == true)
				{
					return assignments[i];
				}
			}
			return null;
		}
	}

	private void Awake()
	{
		for (int i = 0; i < assignmentSOs.Count; i++)
		{
			assignments.Add(new Assignment(assignmentSOs[i].name, assignmentSOs[i].description, assignmentSOs[i].completed, assignmentSOs[i].active, assignmentSOs[i].isMain, assignmentSOs[i].isSide, assignmentSOs[i].itemRegisterName1, assignmentSOs[i].itemRegisterName2, assignmentSOs[i].itemRegisterAmount1, assignmentSOs[i].itemRegisterAmount2));
		}
	}

	private void Start()
	{
		// bool activatedNextAssignment = false;
		// ActivateNextAssignment(ref activatedNextAssignment);
	}

	public void ActivateNextAssignment(ref bool activatedNext)
	{
		for (int i = 0; i < assignments.Count; i++)
		{
			if (assignments[i].active == false && assignments[i].completed == false)
			{
				assignments[i].active = true;
				activatedNext = true;
				return;
			}
		}
		activatedNext = false;
		return;
	}

	public void CompleteAssignment(Assignment assignment)
	{
		assignment.completed = true;
		assignment.active = false;
		bool activatedNextAssignment = false;
		Debug.Log("Completed assignment: " + assignment.name);
		ActivateNextAssignment(ref activatedNextAssignment);
		AssignmentCanvasManager.Instance.CompleteAssignment();
		if (!activatedNextAssignment)
		{
			Debug.Log("No more assignments");
		}
	}

	public void RegisterItem(string itemName, int itemCount)
	{
		if (CurrentAssignment != null)
		{
			if (CurrentAssignment.itemRegisterName1 == itemName)
			{
				CurrentAssignment.itemRegisterAmount1 -= itemCount;
			}
			if (CurrentAssignment.itemRegisterName2 == itemName)
			{
				CurrentAssignment.itemRegisterAmount2 -= itemCount;
			}
			if (CurrentAssignment.itemRegisterAmount1 <= 0 && CurrentAssignment.itemRegisterAmount2 <= 0)
			{
				CompleteAssignment(CurrentAssignment);
			}
		}
	}


}
