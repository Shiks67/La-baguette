using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CalibrateTable : MonoBehaviour
{
    public GameObject prismCamera;
    public GameObject mainCamera;

    // Update is called once per frame
    public void Calibrate()
    {
        Vector3 cameraPos = prismCamera.transform.position;
        Vector3 cameraDirection = mainCamera.transform.forward;
        Vector3 cameraAngles = mainCamera.transform.eulerAngles;
        float spawnDistance = .51f;

        gameObject.transform.position = cameraPos + cameraDirection * spawnDistance;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x,
        .8f,
        gameObject.transform.position.z);
        gameObject.transform.eulerAngles = new Vector3(90, cameraAngles.y, cameraAngles.z);
    }
}
