using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PC : MonoBehaviour, Interactable
{
	[SerializeField]
	private GameObject screenOff;
	[SerializeField]
	private GameObject screenOn;
	[SerializeField]
	public string actionMapName;

	private InputManager inputManager;
	private PlayerActionControls controls;
	private Player player;

	private bool isUnlocked = false;

	private void Awake()
	{
		inputManager = InputManager.Instance;
		controls = InputManager.Instance.controls;
		player = Player.Instance;
	}

	void Start()
	{
		screenOff.SetActive(true);
		screenOn.SetActive(false);
	}

	public void Interact()
	{
		if (!isUnlocked)
		{
			isUnlocked = true;
			screenOff.SetActive(false);
			screenOn.SetActive(true);
		}
		InputManager.Instance.SwitchActionMap(inputManager.GetActionMap(actionMapName));
	}

}
