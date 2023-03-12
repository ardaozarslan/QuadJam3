using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
	private void Start()
	{
		Camera.main.transform.LookAt(transform);
	}
	
	void Update()
	{
		transform.Rotate(Vector3.up * Time.deltaTime * 10f);
	}
}
