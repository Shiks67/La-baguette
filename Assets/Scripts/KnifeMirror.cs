using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMirror : MonoBehaviour
{

    public GameObject LeftKnife;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(LeftKnife.transform.position.x * -1, 
        LeftKnife.transform.position.y, LeftKnife.transform.position.z);
        
        gameObject.transform.rotation = new Quaternion(LeftKnife.transform.rotation.x,
        LeftKnife.transform.rotation.y * -1,
        LeftKnife.transform.rotation.z * -1,
        LeftKnife.transform.rotation.w);
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
