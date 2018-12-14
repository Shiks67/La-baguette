using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightControllerPointer : MonoBehaviour
{

    private LineRenderer pointer;

    // Use this for initialization
    void Start()
    {
        pointer = gameObject.GetComponent<LineRenderer>();
        pointer.SetPosition(0, gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        pointer.SetPosition(0, gameObject.transform.position);
        pointer.SetPosition(1, gameObject.transform.rotation * new Vector3(0,0,20));
    }
}
