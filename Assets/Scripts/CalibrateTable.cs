using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CalibrateTable : MonoBehaviour
{

    public GameObject table;
    private Bounds tablePlaneBounds;

    // Update is called once per frame
    void Update()
    {
        if (SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            table.transform.position = gameObject.transform.position;
            table.transform.eulerAngles = new Vector3(90, gameObject.transform.eulerAngles.y, 0);
        }
    }
}
