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

    // Update is called once per frame
    void Update()
    {
        //change the position with the headset tracked position
        transform.position = InputTracking.GetLocalPosition(XRNode.CenterEye);

        if (!isPrismON)
            return;

        //tryparse the number entered in the HUD
        if (float.TryParse(prismOffset.text, out offset))
        {
            //add the offset to the camera
            gameObject.transform.eulerAngles = new Vector3(0, offset, 0);
        }
        else
        {
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    /// <summary>
    /// Change current state of bool isPrimON
    /// if it's false, reset camera's rotation
    /// </summary>
    public void PrismONOFF()
    {
        isPrismON = true;
        if (!isPrismON)
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
