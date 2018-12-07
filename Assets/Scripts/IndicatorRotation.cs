using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorRotation : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		gameObject.transform.LookAt(Camera.main.transform);
	}
}
