using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMirror : MonoBehaviour
{

    public GameObject LeftKnife;

    // Update is called once per frame
    void Update()
    {
        var mirrorX = LeftKnife.transform.position.x - 
        ((LeftKnife.transform.position.x - Camera.main.transform.position.x) * 2);
        gameObject.transform.position = new Vector3(mirrorX,
        LeftKnife.transform.position.y, LeftKnife.transform.position.z);

        //mirroring rotation
        gameObject.transform.eulerAngles = LeftKnife.transform.eulerAngles;
    }

    void OnEnable()
    {
        LeftKnife.transform.GetComponent<Cutter>().enabled =
        false;
        LeftKnife.transform.GetComponent<MeshRenderer>().enabled =
        false;
    }

    void OnDisable()
    {
        LeftKnife.transform.GetComponent<Cutter>().enabled =
        true;
        LeftKnife.transform.GetComponent<MeshRenderer>().enabled =
        true;
    }
}
