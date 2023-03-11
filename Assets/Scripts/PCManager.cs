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
		player = Player.Instance;



	}

	private void Start()
	{
		controls = InputManager.Instance.controls;
		controls.PC1Player.PC1PlayerExit.performed += ExitAction;
		controls.PC2Player.PC2PlayerExit.performed += ExitAction;
		controls.PC3Player.PC3PlayerExit.performed += ExitAction;
		controls.PC4Player.PC4PlayerExit.performed += ExitAction;
		controls.PC5Player.PC5PlayerExit.performed += ExitAction;
	}

	private void OnEnable()
	{

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
		switch (context.action.name)
		{
			case "PC1PlayerExit":
				pcs[0].ExitedFromThis();
				break;
			case "PC2PlayerExit":
				pcs[1].ExitedFromThis();
				break;
			case "PC3PlayerExit":
				pcs[2].ExitedFromThis();
				break;
			case "PC4PlayerExit":
				pcs[3].ExitedFromThis();
				break;
			case "PC5PlayerExit":
				pcs[4].ExitedFromThis();
				break;
			default:
				break;
		}
		InputManager.Instance.SwitchActionMap(inputManager.GetActionMap("Player"));
	}

	public void UnlockPC(string pcName)
	{
		bool succeeded = int.TryParse(pcName, out int deneme);
		if (!succeeded)
		{
			pcs[pcs.Count - 1].isCompleted = true;
			return;
		}
		PC pc = pcs[deneme - 1];
		pc.isAvailable = true;
		PC previousPC = pcs[deneme - 2];
		// previousPC.ActivateLightsAndCogs();
		previousPC.isCompleted = true;
	}
}
