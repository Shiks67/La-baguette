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
        if (!isPrismON)
            return;

        if (float.TryParse(prismOffset.text, out offset))
        {
            transform.position = -InputTracking.GetLocalPosition(XRNode.Head);
            //transform.rotation = Quaternion.Inverse(InputTracking.GetLocalRotation(XRNode.CenterEye));
            transform.eulerAngles = new Vector3(InputTracking.GetLocalRotation(XRNode.Head).eulerAngles.x,
            InputTracking.GetLocalRotation(XRNode.Head).eulerAngles.y + offset,
            InputTracking.GetLocalRotation(XRNode.Head).eulerAngles.z);
        }
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
