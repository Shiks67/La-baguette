using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaguetteManager : MonoBehaviour
{
    public static bool isBaguette;

    public GameObject spawnObject;

    private Bounds spawnArea;
    private Vector3 Center;

    // Use this for initialization
    void Start()
    {
        Center = gameObject.transform.localPosition;
        spawnArea = gameObject.GetComponent<BoxCollider>().bounds;
        isBaguette = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBaguette)
        {
            GameObject baguette = Instantiate(spawnObject);
            baguette.transform.SetParent(gameObject.transform);
            baguette.transform.localPosition =
            new Vector3(
                Random.Range(spawnArea.min.x, spawnArea.max.x),
                Random.Range(spawnArea.min.z, spawnArea.max.z),
                -0.108f
            );
            baguette.transform.eulerAngles = new Vector3(90, 0, 90);
            isBaguette = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Destroy(gameObject.transform.GetChild(0).gameObject);
            Destroy(gameObject.transform.GetChild(1).gameObject);
            isBaguette = false;
        }
    }
}
