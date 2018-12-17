using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BaguettePercentage : MonoBehaviour {

	private Text leftSide;
	private Text rightSide;
	// Use this for initialization
	void Start () {
		leftSide = transform.GetChild(0).gameObject.GetComponent<Text>();
		rightSide = transform.GetChild(1).gameObject.GetComponent<Text>();
		leftSide.enabled = false;
		rightSide.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void percentageDisplay(float[] percentage){
		String leftPercentage = Math.Round(percentage[0],2).ToString() + " %";
		String rightPercentage = Math.Round(percentage[1],2).ToString() + " %";

		leftSide.text = leftPercentage;
		rightSide.text = rightPercentage;
		leftSide.enabled = true;
		rightSide.enabled = true;
	}
}
