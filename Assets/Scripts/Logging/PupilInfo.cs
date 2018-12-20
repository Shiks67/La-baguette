using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PupilInfo : MonoBehaviour
{

    public Text lconf;
    public Text rconf;
    public float refreshTime;
    private float countDown;

    public static float confidence0, confidence1, gazeConfidence;


    // Use this for initialization
    void Start()
    {
        // PupilTools.OnConnected += StartPupilSubscription;
        // PupilTools.OnDisconnecting += StopPupilSubscription;
        PupilTools.SubscribeTo("pupil.");
        PupilTools.SubscribeTo("gaze");
        PupilTools.OnReceiveData += CustomReceiveData;
        countDown = refreshTime;
    }

    void StartPupilSubscription()
    {
        PupilTools.CalibrationMode = Calibration.Mode._2D;
        // PupilTools.SubscribeTo("pupil.");
        // PupilTools.SubscribeTo("gaze");
    }

    void StopPupilSubscription()
    {
        PupilTools.UnSubscribeFrom("pupil.");
        PupilTools.UnSubscribeFrom("gaze");
    }

    void CustomReceiveData(string topic, Dictionary<string, object> dictionary, byte[] thirdFrame = null)
    {
        if (topic.StartsWith("pupil.1"))
        {
            foreach (var item in dictionary)
            {
                switch (item.Key)
                {
                    case "confidence":
                        countDown -= Time.deltaTime;
                        if (countDown < 0)
                        {
                            confidence1 = PupilTools.FloatFromDictionary(dictionary, item.Key);
                            lconf.text = "Left conf\n" + (confidence1 * 100) + "%";
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        if (topic.StartsWith("pupil.0"))
        {
            foreach (var item in dictionary)
            {
                switch (item.Key)
                {
                    case "confidence":
                        if (countDown < 0)
                        {
                            confidence0 = PupilTools.FloatFromDictionary(dictionary, item.Key);
                            rconf.text = "Right conf\n" + (confidence0 * 100) + "%";
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        if (topic.StartsWith("gaze"))
        {
            foreach (var item in dictionary)
            {
                switch (item.Key)
                {
                    case "confidence":
                        if (countDown < 0)
                        {
                            gazeConfidence = PupilTools.FloatFromDictionary(dictionary, item.Key);
                            countDown = refreshTime;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }

    void OnDisable()
    {
        PupilTools.OnConnected -= StartPupilSubscription;
        PupilTools.OnDisconnecting -= StopPupilSubscription;

        PupilTools.OnReceiveData -= CustomReceiveData;
    }
}
