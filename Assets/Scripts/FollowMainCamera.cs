using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMainCamera : MonoBehaviour
{

    private Camera mainCamera;

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //follow position and rotation of the camera with Z offset
        gameObject.transform.rotation = mainCamera.transform.rotation;
        gameObject.transform.position = new Vector3(mainCamera.transform.position.x,
        mainCamera.transform.position.y, mainCamera.transform.position.z + 1.2f);
    }
}
