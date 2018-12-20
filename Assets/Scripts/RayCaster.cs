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
    private LineRenderer heading;

    public static RaycastHit[] headHits;
    public static RaycastHit[] gazeHits;

    // Use this for initialization
    void Start()
    {
        PupilData.calculateMovingAverage = true;
        mainCamera = Camera.main;
        heading = gameObject.GetComponent<LineRenderer>();

        InputTracking.disablePositionalTracking = true;
        transform.localPosition = new Vector3(0, 0, 0);
    }

    void OnEnable()
    {
        if (PupilTools.IsConnected)
        {
            PupilGazeTracker.Instance.StartVisualizingGaze();
            //PupilTools.IsGazing = true;
            //PupilTools.SubscribeTo ("gaze");
        }
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

        if (gameObject.GetComponent<LineRenderer>().enabled)
        {
            RaycastHit hit;
            if (Physics.Raycast(gazeRay, out hit))
            {
                heading.SetPosition(1, hit.point);
                hit.point = transform.InverseTransformPoint(hit.point);
            }
            else
            {
                heading.SetPosition(1, gazeRay.origin + gazeRay.direction * 50f);
            }
        }

        Debug.DrawRay(gameObject.transform.position, gameObject.transform.rotation * Vector3.forward * 100.0f, Color.red);
        headRay = new Ray(gameObject.transform.position,
         gameObject.transform.rotation * Vector3.forward);
        headHits = Physics.RaycastAll(headRay);
    }

}
