using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PC : MonoBehaviour, IInteractable
{
	[SerializeField]
	private GameObject screenOff;
	[SerializeField]
	private GameObject screenOn;
	[SerializeField]
	public string actionMapName;
	[SerializeField]
	private List<GameObject> pillarObjects = new List<GameObject>();
	[SerializeField]
	private GameObject sigil;

	[SerializeField]
	private GameObject bigCog;
	[SerializeField]
	private GameObject smallCog;

	public Transform playerMoveTransform;
	public Transform playerExitTransform;

	private InputManager inputManager;
	private PlayerActionControls controls;
	private Player player;

	public PCPlayer pcPlayer;

	public bool isAvailable = false;
	private bool isUnlocked = false;

	public bool lightsAndCogsTriggered = false;
	public bool isCompleted = false;
	public bool loopTriggered = false;

	public Outline outline;

	public Material gearsMaterial;

	public Tween tweenToKill;

	private DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> playerPositionTween;

	private float pillarEmissionIntensity = 0f;

	private void Awake()
	{
		inputManager = InputManager.Instance;
		controls = InputManager.Instance.controls;
		// player = Player.Instance;
		player = Player.Instance;
		foreach (var pillar in pillarObjects)
		{
			var pillarMaterialColor = pillar.GetComponent<MeshRenderer>().material.GetColor("_EmissionColor");
			pillar.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(1f, 1f, 1f) * pillarEmissionIntensity);

		}
		sigil.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(1f, 1f, 1f) * pillarEmissionIntensity);
		bigCog.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
		smallCog.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);

		sigil.transform.DOLocalMoveZ(0.5f, 0.0f);

		outline = gameObject.AddComponent<Outline>();
		outline.OutlineMode = Outline.Mode.OutlineAll;
		outline.OutlineColor = Color.yellow;
		outline.OutlineWidth = 5f;
		outline.enabled = false;
	}

	void Start()
	{
		screenOff.SetActive(true);
		screenOn.SetActive(false);
		if (actionMapName == "PC1Player")
		{
			outline.enabled = true;
		}
	}

	public void ExitedFromThis()
	{
		Rigidbody rb = player.GetComponent<Rigidbody>();
		rb.isKinematic = true;
		if (playerPositionTween != null)
		{
			playerPositionTween.Kill();
		}
		playerPositionTween = rb.DOMove(new Vector3(playerExitTransform.position.x, rb.position.y, playerExitTransform.position.z), 0.2f);
		playerPositionTween.SetUpdate(UpdateType.Fixed);
		playerPositionTween.OnComplete(() =>
		{
			rb.isKinematic = false;

			if (!loopTriggered)
			{
				loopTriggered = true;
				ActivateLoop();
			}
		});

		if (isCompleted && !lightsAndCogsTriggered)
		{
			ActivateLightsAndCogs();
		}
		if (isCompleted)
		{
			isAvailable = false;
		}
	}

	public void ActivateLoop()
	{
		screenOn.GetComponent<MeshRenderer>().material = gearsMaterial;
	}

	public void Interact()
	{
		if (!isAvailable)
		{
			return;
		}
		if (!isUnlocked)
		{
			isUnlocked = true;
			outline.enabled = false;
			screenOff.SetActive(false);
			screenOn.SetActive(true);
			if (actionMapName == "PC1Player")
			{
				bool activatedNextAssignment = false;
				AssignmentManager.Instance.ActivateNextAssignment(ref activatedNextAssignment);
				AssignmentCanvasManager.Instance.ShowAssignment(AssignmentManager.Instance.CurrentAssignment.name);
			}

			switch (GameManager.Instance.sceneNumber)
			{
				case 2:
					break;
				case 3:
					if (PCManager.Instance.pcs.IndexOf(this) == PCManager.Instance.endGameTextIndex)
					{
						PlayerTextCanvasManager.Instance.ShowPlayerText(GameManager.Instance.playerJumpPositiveTexts[PCManager.Instance.endGameTextIndex]);
						PCManager.Instance.endGameTextIndex++;
					}
					break;
				default:
					break;
			}

			switch (actionMapName)
			{
				case "PC1Player":
					MaterialsCanvasManager.Instance.UpdateInventoryTexts("berry", 0);
					DOTween.To(() => MaterialsCanvasManager.Instance.berryText.GetComponent<CanvasGroup>().alpha, x => MaterialsCanvasManager.Instance.berryText.GetComponent<CanvasGroup>().alpha = x, 1, 0.5f);
					// MaterialsCanvasManager.Instance.berryText.GetComponent<CanvasGroup>().alpha = 1;
					break;
				case "PC2Player":
					MaterialsCanvasManager.Instance.UpdateInventoryTexts("flax", 0);
					DOTween.To(() => MaterialsCanvasManager.Instance.flaxText.GetComponent<CanvasGroup>().alpha, x => MaterialsCanvasManager.Instance.flaxText.GetComponent<CanvasGroup>().alpha = x, 1, 0.5f);
					// MaterialsCanvasManager.Instance.flaxText.GetComponent<CanvasGroup>().alpha = 1;
					break;
				case "PC3Player":
					MaterialsCanvasManager.Instance.UpdateInventoryTexts("stone", 0);
					MaterialsCanvasManager.Instance.UpdateInventoryTexts("stick", 0);
					DOTween.To(() => MaterialsCanvasManager.Instance.stoneText.GetComponent<CanvasGroup>().alpha, x => MaterialsCanvasManager.Instance.stoneText.GetComponent<CanvasGroup>().alpha = x, 1, 0.5f);
					DOTween.To(() => MaterialsCanvasManager.Instance.stickText.GetComponent<CanvasGroup>().alpha, x => MaterialsCanvasManager.Instance.stickText.GetComponent<CanvasGroup>().alpha = x, 1, 0.5f);
					// MaterialsCanvasManager.Instance.stoneText.GetComponent<CanvasGroup>().alpha = 1;
					// MaterialsCanvasManager.Instance.stickText.GetComponent<CanvasGroup>().alpha = 1;
					break;
				default:
					break;
			}
		}
		// DOTween.To(() => player.transform.position, x => player.transform.position = new Vector3(x.x, player.transform.position.y, x.z), playerMoveTransform.position, 0.5f);


		Rigidbody rb = player.GetComponent<Rigidbody>();
		rb.isKinematic = true;
		if (playerPositionTween != null)
		{
			playerPositionTween.Kill();
		}
		playerPositionTween = rb.DOMove(new Vector3(playerMoveTransform.position.x, rb.position.y, playerMoveTransform.position.z), 0.5f);
		playerPositionTween.SetUpdate(UpdateType.Fixed);

		playerPositionTween.OnUpdate(() =>
		{
			GameObject newGameObject = new GameObject();
			newGameObject.transform.position = new Vector3(player.eyesTransform.position.x, player.eyesTransform.position.y, player.eyesTransform.position.z);

			newGameObject.transform.LookAt(new Vector3(transform.position.x, transform.position.y, transform.position.z));
			player.xRotation = newGameObject.transform.rotation.eulerAngles.x;
			player.yRotation = newGameObject.transform.rotation.eulerAngles.y;
			Destroy(newGameObject);
		});
		playerPositionTween.OnComplete(() =>
		{
			rb.isKinematic = false;
		});



		if (GameManager.Instance.sceneNumber == 2)
		{
			if (actionMapName == "PC1Player")
			{
				pcPlayer.animator.enabled = true;
				InputManager.Instance.TemporarilyDisableCurrentActionMap();
				UIManager.Instance.crosshair.SetActive(false);
				Utils.WaitForSecondsAndInvoke(2f, () =>
				{
					pcPlayer.animator.SetTrigger("jump");
					Utils.WaitForSecondsAndInvoke(5f, () =>
					{

						NarratorTextCanvasManager.Instance.ShowNarratorText();
					});

				});
				return;
			}
		}
		InputManager.Instance.SwitchActionMap(inputManager.GetActionMap(actionMapName));
	}

	public void ActivateLightsAndCogs()
	{
		bigCog.GetComponent<Animator>().SetTrigger("playCogs");
		smallCog.GetComponent<Animator>().SetTrigger("playCogs");
		DOTween.To(() => pillarEmissionIntensity, x => pillarEmissionIntensity = x, 3f, 1f).OnUpdate(() =>
		{
			foreach (var pillar in pillarObjects)
			{
				pillar.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(1f, 1f, 1f) * pillarEmissionIntensity);
			}
			sigil.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(1f, 1f, 1f) * pillarEmissionIntensity);

			bigCog.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f) * pillarEmissionIntensity / 3f;
			smallCog.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f) * pillarEmissionIntensity / 3f;
		}).OnComplete(() =>
		{
			tweenToKill = DOTween.To(() => pillarEmissionIntensity, x => pillarEmissionIntensity = x, 1.75f, 1.75f).SetLoops(-1, LoopType.Yoyo).OnUpdate(() =>
			{
				foreach (var pillar in pillarObjects)
				{
					if (pillar != null)
					{
						pillar.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(1f, 1f, 1f) * pillarEmissionIntensity);
					}
				}
				if (sigil != null)
				{
					sigil.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(1f, 1f, 1f) * pillarEmissionIntensity);
				}
			});

		});
		sigil.transform.DOLocalMoveZ(0.8f, 3f).OnComplete(() =>
		{
			sigil.GetComponent<Sigil>().ActivateSigil();
		});
	}

}
