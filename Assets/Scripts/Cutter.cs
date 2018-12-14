using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter : MonoBehaviour {

	public Material capMaterial;

	public BaguettePercentage baguettePercentage;

	public float[] infoBaguette = new float[2];
	// Use this for initialization
	void Start () {

	}
	
	void Update(){
		//if(Input.GetMouseButtonDown(0)){
		RaycastHit hit;
		Vector3 DirZ = new Vector3(-transform.forward.z,transform.forward.y,transform.forward.x);
        if (Physics.Raycast(transform.position + (transform.up * 0.17f), transform.right, out hit, 0.05f))
        {

            GameObject victim = hit.collider.gameObject;
            if (victim.name == "Baguette")
            {
                infoBaguette = InfoBaguette(transform.position, victim.transform.position);
                baguettePercentage.percentageDisplay(infoBaguette);
                GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, -transform.forward, capMaterial);

                if (!pieces[1].GetComponent<Rigidbody>())
                    pieces[1].AddComponent<Rigidbody>();

                pieces[1].GetComponent<Rigidbody>().useGravity = false;
            }

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

	public float[] InfoBaguette(Vector3 knifePosistion, Vector3 baguettePosition){
		float[] leftAndRight = new float[2];
		float x = knifePosistion.x - baguettePosition.x;
		leftAndRight[0] = (0.5f + (0.5f * x/0.6f)) * 100;
		leftAndRight[1] = 100 - leftAndRight[0];
		return leftAndRight;
	}
}
