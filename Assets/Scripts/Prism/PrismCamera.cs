using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrismCamera : MonoBehaviour
{

    private GameObject cameraParent;
    private float offset;

    public Text prismOffset;
    private bool isPrismON;

    // Use this for initialization
    void Start()
    {
        isPrismON = false;
        cameraParent = new GameObject();
        cameraParent.transform.SetParent(gameObject.transform.parent.transform);
        cameraParent.transform.position = new Vector3(0, 0, 0);
        gameObject.transform.SetParent(cameraParent.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPrismON)
        {
            cameraParent.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            return;
        }


        if (float.TryParse(prismOffset.text, out offset))
            cameraParent.transform.rotation = Quaternion.Euler(new Vector3(0, offset, 0));
    }

    public void PrismON()
    {
        isPrismON = true;
    }

    public void PrismOFF()
    {
        isPrismON = false;
    }
}
