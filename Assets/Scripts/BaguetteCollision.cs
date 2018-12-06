using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaguetteCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter(UnityEngine.Collision collision){
		print("Romain");
		BaguetteCut.Cut(collision.transform, transform.position);
	}
}
