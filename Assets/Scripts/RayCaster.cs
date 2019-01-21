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

    //can access to the hitted objects from the gaze point or with the head from anywhere
    public static RaycastHit[] headHits;
    public static RaycastHit[] gazeHits;

    /// <summary>
    /// basic method used by pupil lab to start the eye tracking
    /// </summary>
    void Start()
    {
        PupilData.calculateMovingAverage = true;
        mainCamera = Camera.main;
        heading = gameObject.GetComponent<LineRenderer>();

        InputTracking.disablePositionalTracking = true;
        transform.localPosition = new Vector3(0, 0, 0);
    }

    /// <summary>
    /// basic method used by pupil lab to start the eye tracking
    /// </summary>
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
        //Vector3 point where the raycast will go trough
        viewportPoint = new Vector3(0.5f, 0.5f, 10);
        //if pupil lab is connected and the calibration is done (so IsGazing will be true)
        //take the x and y position of the gaze point to update the viewport point
        if (PupilTools.IsConnected && PupilTools.IsGazing)
        {
            gazePointCenter = PupilData._2D.GazePosition;
            viewportPoint = new Vector3(gazePointCenter.x, gazePointCenter.y, 1f);
        }
        //ray going from the camera through the viewport point
        gazeRay = mainCamera.ViewportPointToRay(viewportPoint);
        //ray all to gaze point
        gazeHits = Physics.RaycastAll(gazeRay);

        //move the linerenderer to see the ray going from the camera through the viewport point in game window
        if (gameObject.GetComponent<LineRenderer>().enabled)
        {
            RaycastHit hit;
            if (Physics.Raycast(gazeRay, out hit))
            {
                //draw line from camera to the hit point
                heading.SetPosition(1, hit.point);
                //position of the hited object
                hit.point = transform.InverseTransformPoint(hit.point);
            }
            else //if nothing is hit by the raycast
            {
                heading.SetPosition(1, gazeRay.origin + gazeRay.direction * 50f);
            }
        }
        //ray all to forward from the camera
        headRay = new Ray(gameObject.transform.position,
         gameObject.transform.rotation * Vector3.forward);
        headHits = Physics.RaycastAll(headRay);
    }

}
