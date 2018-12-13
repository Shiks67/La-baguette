using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionData : MonoBehaviour
{
    private List<float> headPosList = new List<float>();
    private List<float> gazePosList = new List<float>();
    public static List<List<float>> savedHeadPosList = new List<List<float>>();
    public static List<List<float>> savedGazePosList = new List<List<float>>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //save y pos on rotated baguette (which correspond to x after the rotation)
        for (int i = 0; i < RayCaster.headHits.Length; i++)
        {
            RaycastHit hit = RayCaster.headHits[i];
            if (hit.collider.gameObject.name == "BaguetteCollider")
            {
                headPosList.Add(hit.collider.gameObject.transform.parent.transform.InverseTransformPoint(hit.point).y);
                // print("head : " + hit.collider.gameObject.transform.parent.transform.InverseTransformPoint(hit.point));
            }
        }

        for (int i = 0; i < RayCaster.gazeHits.Length; i++)
        {
            RaycastHit hit = RayCaster.gazeHits[i];
            if (hit.collider.gameObject.name == "BaguetteCollider")
            {
                gazePosList.Add(hit.collider.gameObject.transform.parent.transform.InverseTransformPoint(hit.point).y);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            savedHeadPosList.Add(new List<float>(headPosList));
            savedGazePosList.Add(new List<float>(gazePosList));
            headPosList.Clear();
            gazePosList.Clear();
        }
    }
}
