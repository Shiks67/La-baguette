using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Cutter : MonoBehaviour
{

    public Material capMaterial;
    private float[] infoBaguette = new float[2];
    private Vector3 cutPosition;

    void Update()
    {
        //if we are using the left knife, change the cuting transform
        if (gameObject.name == "LeftKninfe")
            cutPosition = transform.parent.transform.position;
        else
            cutPosition = transform.position;
//&& SteamVR_Input._default.inActions.GrabPinch.GetState(SteamVR_Input_Sources.LeftHand)
        RaycastHit hit;
        Vector3 DirZ = new Vector3(-transform.forward.z, transform.forward.y, transform.forward.x);
        if (Physics.Raycast(transform.position + (transform.up * 0.17f), transform.right, out hit, 0.05f)
        )
        {
            GameObject victim = hit.collider.gameObject;
            if (victim.name == "Baguette(Clone)")
            {
                
                print(victim.GetComponent<Renderer>().bounds.size);
                float baguetteBoundsSize = victim.GetComponent<Renderer>().bounds.size.x;
                GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, cutPosition, -transform.forward, capMaterial);
                print(pieces[0].GetComponent<Renderer>().bounds.size);
                float leftBoundsSize = pieces[0].GetComponent<Renderer>().bounds.size.x;
                infoBaguette = InfoBaguette(baguetteBoundsSize, leftBoundsSize);
                pieces[0].transform.GetChild(0).gameObject.GetComponent<BaguettePercentage>().percentageDisplay(infoBaguette);
                if (!pieces[1].GetComponent<Rigidbody>())
                    pieces[1].AddComponent<Rigidbody>();

                pieces[1].GetComponent<Rigidbody>().useGravity = false;
            }
        }
    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, transform.position + transform.right * 5.0f);
        Gizmos.DrawLine(transform.position + transform.up * 0.17f, transform.position + transform.up * 0.5f + transform.right * 5.0f);
        Gizmos.DrawLine(transform.position + -transform.up * 0.5f, transform.position + -transform.up * 0.5f + transform.right * 5.0f);

        Gizmos.DrawLine(transform.position, transform.position + transform.up * 0.5f);
        Gizmos.DrawLine(transform.position, transform.position + -transform.up * 0.5f);

    }

    public float[] InfoBaguette(float baguetteBoundsSizeX, float leftBoundsSizeX)
    {
        float[] leftAndRight = new float[2];
        leftAndRight[0] = (leftBoundsSizeX / baguetteBoundsSizeX) * 100 ;
        leftAndRight[1] = 100 - leftAndRight[0];
        return leftAndRight;
    }
}
