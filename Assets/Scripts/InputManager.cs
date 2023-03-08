using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
	public PlayerActionControls controls;
	public event Action<InputActionMap> actionMapChange;

	public InputActionMap currentActionMap;

	private void OnEnable()
	{
		
	}

	private void OnDisable()
	{

	}


	private void Awake()
	{
		controls = new PlayerActionControls();
	}

	private void Start()
	{
		SwitchActionMap(controls.Player);
	}

	public InputActionMap GetActionMap(string actionMapName)
	{
		switch (actionMapName)
		{
			case "Player":
				return controls.Player;
			case "PC1Player":
				return controls.PC1Player;
			case "PC2Player":
				return controls.PC2Player;
			case "PC3Player":
				return controls.PC3Player;
			case "PC4Player":
				return controls.PC4Player;
			case "PC5Player":
				return controls.PC5Player;
			default:
				return controls.Player;
		}
	}

	public void SwitchActionMap(InputActionMap actionMap)
	{
		if (actionMap.enabled)
		{
			return;
		}
		controls.Disable();
		actionMapChange?.Invoke(actionMap);
		actionMap.Enable();
		currentActionMap = actionMap;
		if (currentActionMap == controls.Player.Get())
		{
			Debug.Log("Player action map enabled");
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		else if (currentActionMap == controls.PC1Player.Get())
		{
			Debug.Log("PC1Player action map enabled");
		}
		else if (controls.PC2Player.Equals(currentActionMap))
		{
			Debug.Log("PC2Player action map enabled");
		}
		else if (controls.PC3Player.Equals(currentActionMap))
		{
			Debug.Log("PC3Player action map enabled");
		}
		else if (controls.PC4Player.Equals(currentActionMap))
		{
			Debug.Log("PC4Player action map enabled");
		}
		else if (controls.PC5Player.Equals(currentActionMap))
		{
			Debug.Log("PC5Player action map enabled");
		}
	}

	public void TemporarilyDisableCurrentActionMap()
	{
		currentActionMap.Disable();
	}

	public void ReenableCurrentActionMap()
	{
		currentActionMap.Enable();
	}
}
