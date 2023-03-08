using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PCManager : Singleton<PCManager>
{
	public List<PC> pcs = new List<PC>();

	private InputManager inputManager;
	private PlayerActionControls controls;
	private Player player;

	private void Awake()
	{
		inputManager = InputManager.Instance;
		controls = InputManager.Instance.controls;
		player = Player.Instance;
	}

	private void OnEnable()
	{
		controls.PC1Player.PC1PlayerExit.performed += ExitAction;
		controls.PC2Player.PC2PlayerExit.performed += ExitAction;
		controls.PC3Player.PC3PlayerExit.performed += ExitAction;
		controls.PC4Player.PC4PlayerExit.performed += ExitAction;
		controls.PC5Player.PC5PlayerExit.performed += ExitAction;
	}

	private void OnDisable()
	{
		controls.PC1Player.PC1PlayerExit.performed -= ExitAction;
		controls.PC2Player.PC2PlayerExit.performed -= ExitAction;
		controls.PC3Player.PC3PlayerExit.performed -= ExitAction;
		controls.PC4Player.PC4PlayerExit.performed -= ExitAction;
		controls.PC5Player.PC5PlayerExit.performed -= ExitAction;
	}

	private void ExitAction(InputAction.CallbackContext context)
	{
		InputManager.Instance.SwitchActionMap(inputManager.GetActionMap("Player"));
	}
}
