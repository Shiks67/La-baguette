using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeFollow : MonoBehaviour
{
    public GameObject controller;
    public GameObject table;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = controller.transform.position;
        gameObject.transform.eulerAngles = new Vector3(180, 90 + table.transform.eulerAngles.y, 90);
    }
}
