﻿using UnityEngine;
using System.Collections;

public class ExampleUseof_MeshCut : MonoBehaviour {

	public Material capMaterial;

	private float timer;

	// Use this for initialization
	void Start () {

		timer = 0.0f;
	}
	
	void Update(){
		//if(Input.GetMouseButtonDown(0)){
			RaycastHit hit;
			Vector3 DirZ = new Vector3(-transform.forward.z,transform.forward.y,transform.forward.x);
			print(transform.forward);
			if(Physics.Raycast(transform.position + (transform.up * 0.17f), transform.right, out hit, 0.05f)){
				
				GameObject victim = hit.collider.gameObject;

				GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, -transform.forward, capMaterial);

				if(!pieces[1].GetComponent<Rigidbody>())
					pieces[1].AddComponent<Rigidbody>();

				pieces[1].GetComponent<Rigidbody>().useGravity = false;
				//Destroy(pieces[1], 1);
			}

		//}
	}

	void OnDrawGizmosSelected() {

		Gizmos.color = Color.green;

		Gizmos.DrawLine(transform.position, transform.position + transform.right * 5.0f);
		Gizmos.DrawLine(transform.position + transform.up * 0.17f, transform.position + transform.up * 0.5f + transform.right * 5.0f);
		Gizmos.DrawLine(transform.position + -transform.up * 0.5f, transform.position + -transform.up * 0.5f + transform.right * 5.0f);

		Gizmos.DrawLine(transform.position, transform.position + transform.up * 0.5f);
		Gizmos.DrawLine(transform.position,  transform.position + -transform.up * 0.5f);

	}

}
