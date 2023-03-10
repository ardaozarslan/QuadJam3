using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Assignment", menuName = "Assignment")]
public class AssignmentSO : ScriptableObject
{
	public new string name;
	[TextArea(3, 10)]
	public string description;
	public bool completed;
	public bool active;
	public bool isMain;
	public bool isSide;

	public string itemRegisterName1;
	public int itemRegisterAmount1;
	public string itemRegisterName2;
	public int itemRegisterAmount2;

	public string unlocksPC;
}
