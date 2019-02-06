﻿using System.Collections;
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
                infoBaguette = InfoBaguette(hit.point, victim.transform.position);
                victim.transform.GetChild(0).gameObject.GetComponent<BaguettePercentage>().percentageDisplay(infoBaguette);
                GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, cutPosition, -transform.forward, capMaterial);

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

    public float[] InfoBaguette(Vector3 knifePosition, Vector3 baguettePosition)
    {
        float[] leftAndRight = new float[2];
        float x = knifePosition.x - baguettePosition.x;
        float y =0 ;// knifePosition.y - baguettePosition.y;
        leftAndRight[0] = (0.5f + (0.5f * (x + y) / 0.6f)) * 100;
        leftAndRight[1] = 100 - leftAndRight[0];
        return leftAndRight;
    }
}
