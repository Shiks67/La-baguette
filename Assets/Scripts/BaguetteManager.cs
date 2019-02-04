using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaguetteManager : MonoBehaviour
{
    public static bool isBaguette;
    public GameObject spawnObject;
    private Vector3 Center;

    // Use this for initialization
    void Start()
    {
        Center = gameObject.transform.localPosition;
        isBaguette = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if there is no baguette we create one on the table
        if (!isBaguette)
        {
            GameObject baguette = Instantiate(spawnObject);
            baguette.transform.SetParent(gameObject.transform);
            baguette.transform.localPosition =
            new Vector3(
                Random.Range(-0.5f, 0.5f),
                Random.Range(-0.44f, 0.44f),
                -0.0822f
            );
            baguette.transform.localEulerAngles = new Vector3(0, 0, 90);
            isBaguette = true;
        }
    }

    /// <summary>
    /// delete the tables first childs that will be either the 2 baguette parts 
    /// after the user cut it or the only 1 piece of baguette if he fails
    /// </summary>
    public void NextBaguette()
    {
        if (gameObject.transform.childCount > 1)
        {
            Destroy(gameObject.transform.GetChild(1).gameObject);
            Destroy(gameObject.transform.GetChild(0).gameObject);
        }
        if (gameObject.transform.childCount > 0)
            Destroy(gameObject.transform.GetChild(0).gameObject);
        isBaguette = false;
    }
}
