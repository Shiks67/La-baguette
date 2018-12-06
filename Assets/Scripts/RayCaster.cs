using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour
{

    private Camera mainCamera;
    private Vector3 viewportPoint;
    private Vector2 gazePointCenter;
    private RaycastHit[] headHits;

    private RaycastHit[] gazeHits;
    private Ray gazeRay;
    private Ray headRay;

    // Use this for initialization
    void Start()
    {
        // PupilData.calculateMovingAverage = true;
        // mainCamera = Camera.main;

        // if (PupilTools.IsConnected)
        //     PupilGazeTracker.Instance.StartVisualizingGaze();
    }

    // Update is called once per frame
    void Update()
    {
        // viewportPoint = new Vector3(0.5f, 0.5f, 10);
        // if (PupilTools.IsConnected && PupilTools.IsGazing)
        // {

        //     gazePointCenter = PupilData._2D.GazePosition;
        //     viewportPoint = new Vector3(gazePointCenter.x, gazePointCenter.y, 1f);
        // }
        // gazeRay = mainCamera.ViewportPointToRay(viewportPoint);

        // headRay = new Ray(mainCamera.transform.position, mainCamera.transform.rotation * Vector3.forward);
        // headHits = Physics.RaycastAll(headRay);
        // for (int i = 0; i < headHits.Length; i++)
        // {
        //     RaycastHit hit = headHits[i];
        //     if (hit.transform.name == "Table")
        //     {
        //         var headPosLocal = hit.transform.InverseTransformPoint(hit.point);
        //         // print(headPosLocal);
        //     }
        // }

        // gazeHits = Physics.RaycastAll(gazeRay);
        // for (int i = 0; i < gazeHits.Length; i++)
        // {
        //     RaycastHit hit = gazeHits[i];
        //     if (hit.transform.name == "Baguette")
        //     {
        //         var gazePosLocal = hit.transform.InverseTransformPoint(hit.point);
        //         // print(gazePosLocal);
        //     }
        // }
    }
}
