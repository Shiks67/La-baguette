using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
public class PrismV2 : MonoBehaviour
{
    public Text prismOffset;
    private bool isPrismON = false;
    private float offset;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = InputTracking.GetLocalPosition(XRNode.CenterEye);

        if (!isPrismON)
            return;

        if (float.TryParse(prismOffset.text, out offset))
        {
            gameObject.transform.eulerAngles = new Vector3(0, offset, 0);
        }
        else
        {
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void PrismON()
    {
        isPrismON = true;
    }

    public void PrismOFF()
    {
        isPrismON = false;
        gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
