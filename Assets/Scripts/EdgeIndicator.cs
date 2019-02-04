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
        // Create right and left circle GO from prefab to the edges of the baguette

        GameObject leftIndicator = Instantiate(indicatorGameObject);
        leftIndicator.transform.SetParent(gameObject.transform);
        leftIndicator.transform.localRotation = Quaternion.Euler(0, 45, -90);
        leftIndicator.transform.localPosition = new Vector3(-0.5f, 0.95f, -0.7f);
        leftIndicator.transform.localScale = new Vector3(2, 10, 10);

        GameObject rightIndicator = Instantiate(indicatorGameObject);
        rightIndicator.transform.SetParent(gameObject.transform);
        rightIndicator.transform.localRotation = Quaternion.Euler(0, 45, -90);
        rightIndicator.transform.localPosition = new Vector3(-0.5f, -0.95f, -0.7f);
        rightIndicator.transform.localScale = new Vector3(2, 10, 10);

        leftTimer = 2f;
        rightTimer = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        //meant to always be looking at the user
        //problem is that they just stretch

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
