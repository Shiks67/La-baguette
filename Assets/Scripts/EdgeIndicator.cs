using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeIndicator : MonoBehaviour
{

    public GameObject indicatorGameObject;
    // private GameObject leftIndicator, rightIndicator;

    private float leftTimer, rightTimer;

    // Use this for initialization
    void Start()
    {
        GameObject leftIndicator = Instantiate(indicatorGameObject);
        leftIndicator.transform.SetParent(gameObject.transform);
		leftIndicator.transform.localRotation = Quaternion.Euler(0,90,-90);
        leftIndicator.transform.localPosition = new Vector3(0, 1, 0);
        leftIndicator.transform.localScale = new Vector3(2, 10, 10);

        GameObject rightIndicator = Instantiate(indicatorGameObject);
        rightIndicator.transform.SetParent(gameObject.transform);
		rightIndicator.transform.localRotation = Quaternion.Euler(0,90,-90);
        rightIndicator.transform.localPosition = new Vector3(0, -1, 0);
        rightIndicator.transform.localScale = new Vector3(2, 10, 10);

        leftTimer = 2f;
        rightTimer = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        // for (int i = 0; i < RayCaster.gazeHits.Length; i++)
        // {
        // 	RaycastHit hit = RayCaster.gazeHits[i];
        //     GameObject hitObject = hit.collider.gameObject;

        // 	if(hitObject == leftIndicator.transform.GetChild(0))
        // 	{
        // 		// left.transform.localScale = new Vector3(left.transform.localScale.x - 5f * Time.deltaTime,
        // 		// left.transform.localScale.y - 5f * Time.deltaTime,
        // 		// left.transform.localScale.z - 5f * Time.deltaTime);
        // 		leftTimer -= Time.deltaTime;
        // 	}

        // 	if(hitObject == rightIndicator.transform.GetChild(0))
        // 	{
        // 		// right.transform.localScale = new Vector3(right.transform.localScale.x - 5f * Time.deltaTime,
        // 		// right.transform.localScale.y - 5f * Time.deltaTime,
        // 		// right.transform.localScale.z - 5f * Time.deltaTime);
        // 		rightTimer -= Time.deltaTime;
        // 	}
        // }

        // if(leftTimer >= 0)
        // {
        // 	Destroy(leftIndicator);
        // }

        // if(rightTimer >= 0)
        // {
        // 	Destroy(rightIndicator);
        // }
    }
}
