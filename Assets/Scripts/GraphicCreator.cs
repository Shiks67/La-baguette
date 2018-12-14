using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicCreator : MonoBehaviour {
	private float timer = 0.0f;

	public Transform posPlane;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer >= 1.0f){
			timer = 0.0f;

			print(posPlane.InverseTransformPoint(RayCaster.headPoint.point));
			print(posPlane.InverseTransformPoint(RayCaster.gazePoint.point));
		}
	}
}
