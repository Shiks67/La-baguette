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

	public void percentageDisplay(float[] percentage){
		string leftPercentage = Math.Round(percentage[0],2).ToString() + " %";
		string rightPercentage = Math.Round(percentage[1],2).ToString() + " %";

		leftSide.text = leftPercentage;
		rightSide.text = rightPercentage;
		leftSide.enabled = true;
		rightSide.enabled = true;
	}
}
