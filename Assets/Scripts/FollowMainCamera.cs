using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMainCamera : MonoBehaviour {

	private Camera mainCamera;

	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.rotation = mainCamera.transform.rotation;
		gameObject.transform.position = mainCamera.transform.position;
	}
}
