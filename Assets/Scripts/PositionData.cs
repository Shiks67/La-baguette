using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionData : MonoBehaviour
{
    private List<Vector3> headPosList = new List<Vector3>();
    private List<Vector3> gazePosList = new List<Vector3>();
    public static List<List<Vector3>> savedHeadPosList = new List<List<Vector3>>();
    public static List<List<Vector3>> savedGazePosList = new List<List<Vector3>>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < RayCaster.headHits.Length; i++)
        {
            RaycastHit hit = RayCaster.headHits[i];
            if (hit.collider.gameObject.name == "BaguetteCollider")
            {
                headPosList.Add(hit.collider.gameObject.transform.InverseTransformPoint(hit.point));
                // print("head : " + hit.collider.gameObject.transform.parent.transform.InverseTransformPoint(hit.point));
            }
        }

        for (int i = 0; i < RayCaster.gazeHits.Length; i++)
        {
            RaycastHit hit = RayCaster.gazeHits[i];
            if (hit.collider.gameObject.name == "BaguetteCollider")
            {
                gazePosList.Add(hit.collider.gameObject.transform.InverseTransformPoint(hit.point));
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            savedHeadPosList.Add(new List<Vector3>(headPosList));
            savedGazePosList.Add(new List<Vector3>(gazePosList));
            headPosList.Clear();
            gazePosList.Clear();
        }
    }
}
