using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RayCaster : MonoBehaviour
{

    private Camera mainCamera;
    private Vector3 viewportPoint;
    private Vector2 gazePointCenter;
    private Ray gazeRay;
    private Ray headRay;

    public static RaycastHit[] headHits;
    public static RaycastHit[] gazeHits;

    // Use this for initialization
    void Start()
    {
        PupilData.calculateMovingAverage = true;
        mainCamera = Camera.main;

        InputTracking.disablePositionalTracking = true;
        transform.localPosition = new Vector3(0, 0, 0);

        if (PupilTools.IsConnected)
            PupilGazeTracker.Instance.StartVisualizingGaze();
    }

    // Update is called once per frame
    void Update()
    {
        viewportPoint = new Vector3(0.5f, 0.5f, 10);
        if (PupilTools.IsConnected && PupilTools.IsGazing)
        {
            gazePointCenter = PupilData._2D.GazePosition;
            viewportPoint = new Vector3(gazePointCenter.x, gazePointCenter.y, 1f);
        }
        gazeRay = mainCamera.ViewportPointToRay(viewportPoint);
        gazeHits = Physics.RaycastAll(gazeRay);

        Debug.DrawRay(gameObject.transform.position, gameObject.transform.rotation * Vector3.forward * 100.0f, Color.red);
        headRay = new Ray(gameObject.transform.position,
         gameObject.transform.rotation * Vector3.forward);
        headHits = Physics.RaycastAll(headRay);
    }
}
