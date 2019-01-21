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
        //by pressing the pinch from the right controller change table's position to the controller's one, 
        //and rotate it so it's in front of the user
        if (SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            table.transform.position = gameObject.transform.position;
            table.transform.eulerAngles = new Vector3(90, gameObject.transform.eulerAngles.y, 0);
        }
    }
}
