using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CalibrateTable : MonoBehaviour
{

    public GameObject table;
    public GameObject tablePlane;
    private Bounds tablePlaneBounds;
    // Use this for initialization
    void Start()
    {
        tablePlaneBounds = tablePlane.GetComponent<BoxCollider>().bounds;
    }

    // Update is called once per frame
    void Update()
    {
        if (SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            table.transform.position = new Vector3(gameObject.transform.position.x,
            gameObject.transform.position.y, gameObject.transform.position.z + tablePlaneBounds.max.y);
        }
    }
}
