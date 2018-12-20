using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < RayCaster.gazeHits.Length; i++)
        {
            RaycastHit hit = RayCaster.gazeHits[i];
            if (hit.collider.gameObject == gameObject.transform.GetChild(0).gameObject)
            {
                gameObject.transform.localScale =
                new Vector3(
                    gameObject.transform.localScale.x - 2f * Time.deltaTime,
                    gameObject.transform.localScale.y - 10f * Time.deltaTime,
                       gameObject.transform.localScale.z - 10f * Time.deltaTime
                );
            }
        }
        if (gameObject.transform.localScale.y < 5)
            Destroy(gameObject);
    }
}
