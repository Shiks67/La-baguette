using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaguetteCut : MonoBehaviour {
	public static bool Cut(Transform target, Vector3 _pos){
		Vector3 pos = new Vector3(_pos.x, target.position.y, target.position.z);
		Vector3 targetScale = target.localScale;
		Quaternion targetRotation = target.rotation;
		float distance = Vector3.Distance(target.position, pos);
		//if (distance >= victimScale.x/2) return false;
		
		Vector3 leftPoint = target.position - Vector3.right * targetScale.y/2;
		Vector3 rightPoint = target.position + Vector3.right * targetScale.y/2;
		Material mat = target.GetComponent<MeshRenderer>().material;
		Destroy(target.gameObject);
		
		GameObject rightSideObj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		rightSideObj.transform.position = (rightPoint + pos) /2;
		float rightWidth = Vector3.Distance(pos,rightPoint);
		rightSideObj.transform.localScale = new Vector3( targetScale.x ,rightWidth,targetScale.z );
        rightSideObj.GetComponent<MeshRenderer>().material = mat;
		rightSideObj.transform.rotation = targetRotation;

		GameObject leftSideObj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		leftSideObj.transform.position = (leftPoint + pos)/2;
		float leftWidth = Vector3.Distance(pos,leftPoint);
		leftSideObj.transform.localScale = new Vector3( targetScale.x ,leftWidth ,targetScale.z );
		leftSideObj.GetComponent<MeshRenderer>().material = mat;
		leftSideObj.transform.rotation = targetRotation;
		return true;
	} 
}
