using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyePatchManager : MonoBehaviour
{
    public void EyePatchRight()
    {
        gameObject.transform.localPosition = new Vector3(0.1f, 0, 0);
    }

    public void EyePatchLeft()
    {
        gameObject.transform.localPosition = new Vector3(-0.1f, 0, 0);
    }
}
