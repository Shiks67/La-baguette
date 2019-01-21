using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastF : MonoBehaviour
{

    private Transform myCamera;
    public Ray ray;
    private LineRenderer heading;

    public static RaycastHit hitF;
    // Use this for initialization
    void Start()
    {
        myCamera = gameObject.transform;
        heading = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f, gameObject.transform.position.z);
        if (Input.GetKey(KeyCode.Q))
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - 0.2f, gameObject.transform.position.y, gameObject.transform.position.z);
        if (Input.GetKey(KeyCode.S))
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.2f, gameObject.transform.position.z);
        if (Input.GetKey(KeyCode.D))
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + 0.2f, gameObject.transform.position.y, gameObject.transform.position.z);
        
        ray = new Ray(myCamera.position,
        myCamera.rotation * Vector3.forward * 15);
        if (Physics.Raycast(ray, out hitF))
        {
            hitF.point = transform.InverseTransformPoint(hitF.point);
        }
        Debug.DrawRay(myCamera.position,
        myCamera.rotation * Vector3.forward * 15, Color.red);

        heading.SetPosition(0, myCamera.position);
        heading.SetPosition(1, ray.origin + ray.direction * 10f);
    }
}
