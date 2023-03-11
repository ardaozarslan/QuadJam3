using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sigil : MonoBehaviour, IInteractable
{
	[SerializeField]
	private Collider mainCollider;
	[SerializeField]
	private Collider inhibitorCollider;

	private void Awake() {
		mainCollider.enabled = false;
		inhibitorCollider.enabled = false;
	}

	public void ActivateColliders()
	{
		mainCollider.enabled = true;
		inhibitorCollider.enabled = true;
	}

	public void Interact()
	{
		Debug.Log("Interacting with Sigil");
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
