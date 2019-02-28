using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;


public class BaguetteManager : MonoBehaviour
{

    public struct BaguetteObjectSizes {
        public Vector3 large;
        public Vector3 medium;
        public Vector3 small;
    }
    public enum BaguetteSize {
        Small,
        Medium,
        Large
    }

    public enum EyePatch {
        Left,
        Right,
        Off
    }

     public enum MirrorEffect {
        On,
        Off
    }

    [SerializeField]
    private BaguetteObjectSizes baguetteObjectSizes;
    public static bool isBaguette;
    public GameObject spawnObject;

    private Vector3 Center;

    [SerializeField]
    private LoggingManager loggingManager;

    [SerializeField]
    private InputField emailInputField;
	private string email = "";

    [SerializeField]
    private InputField participantInputField;
    private string participantNumber = "1";

    [SerializeField]
    private Text trialNumberText;
    private string trialNumberTextTemplate;
    private int trialNumber = 0; 
    private Cutter cutter;
    public float cutPercentage = -1f;

    private float timer = 0f; // timeToCut

    private BaguetteSize baguetteSize = BaguetteSize.Medium;

    private MirrorEffect mirrorEffect = MirrorEffect.Off;
    private float prismOffset = 0f; // TODO: Make  Prism offset work again.

    private EyePatch eyePatch = EyePatch.Off; // TODO: Hook up with control for eye patch.

    private bool baguetteIsLogged = false;
    private bool shouldLog = false;

    GameObject currentBaguette;

    [SerializeField]
    private Text loggingNotice;

    [SerializeField]
    private InputField prismOffsetInputField;

    private DateTime testStart;

    [SerializeField]
    private Text testTimerText;

    private bool testRunning = false;

    private string testTimerTextTemplate;

    bool countDown = false;
    private float preTestCountdown = 3f;

    [SerializeField]
    private GameObject testCountdownHolder;
    [SerializeField]
    private Text preTestCountdownText;
    private string preTestCountdownTextTemplate;

    private bool hasUploaded = false;

    // Use this for initialization
    void Start()
    {
        preTestCountdownTextTemplate = preTestCountdownText.text;
        testTimerTextTemplate = testTimerText.text;

        baguetteObjectSizes = new BaguetteObjectSizes();
        baguetteObjectSizes.small = new Vector3(0.05f, 0.25f, 0.05f);
        baguetteObjectSizes.medium = new Vector3(0.1f, 0.5f, 0.1f);
        baguetteObjectSizes.large = new Vector3(0.15f, 1f, 0.15f);
        Center = gameObject.transform.localPosition;
        isBaguette = false;
        trialNumberTextTemplate = trialNumberText.text;
        trialNumberText.text = string.Format(trialNumberTextTemplate, trialNumber.ToString());
        
        if (mirrorEffect == MirrorEffect.Off) {
                cutter = GameObject.Find("LeftKnife").GetComponent<Cutter>();
        } else{
            cutter = GameObject.Find("MirrorKnife").GetComponent<Cutter>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //time to cut the baguette
        timer += Time.deltaTime;

        if (countDown) {
            preTestCountdown  -= Time.deltaTime;
            preTestCountdownText.text = preTestCountdown.ToString("0");
        }

        if (preTestCountdown <= 0) {
            countDown = false;
            testCountdownHolder.SetActive(false);
            preTestCountdown = 3f;
            testStart = DateTime.Now;
            NextBaguette();
            testRunning = true;
        }

        if (testRunning) {
            // if test is happening, update the test timer
            testTimerText.text = String.Format(testTimerTextTemplate, (DateTime.Now-testStart).Minutes.ToString("00"),(DateTime.Now-testStart).Seconds.ToString("00"));
        } else {
            testTimerText.text = String.Format(testTimerTextTemplate, TimeSpan.Zero.Minutes.ToString("00"), TimeSpan.Zero.Seconds.ToString("00"));
        }

        //if there is no baguette we create one on the table
        if (!isBaguette)
        {
            currentBaguette = Instantiate(spawnObject);
            currentBaguette.transform.SetParent(gameObject.transform);
            currentBaguette.transform.localPosition =
            new Vector3(
                UnityEngine.Random.Range(-0.3f, 0.3f),
                UnityEngine.Random.Range(-0.34f, 0.24f),
                -0.0822f
            );
            currentBaguette.transform.localEulerAngles = new Vector3(0, 0, 90);
            isBaguette = true;
            changeBaguetteSize();
            timer = 0;
        }
    }

    public void LogBaguetteCut() {
        if (!testRunning) {
            return;
        }

        if (!shouldLog) {
            return;
        }

        if (baguetteIsLogged) {
            return;
        }

        loggingManager.WriteToLog("Email", email);

        string date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        loggingManager.WriteToLog("DateAdded", date);

        loggingManager.WriteToLog("ParticipantNumber", participantNumber);

        loggingManager.WriteToLog("TrialNumber", trialNumber.ToString());
        loggingManager.WriteToLog("CutPosition", cutPercentage.ToString());
        loggingManager.WriteToLog("TimeToCut", timer.ToString());
        loggingManager.WriteToLog("BaguetteSize", Enum.GetName(typeof(BaguetteSize), baguetteSize));
        loggingManager.WriteToLog("Mirror", Enum.GetName(typeof(MirrorEffect), mirrorEffect));
        loggingManager.WriteToLog("EyePatch", Enum.GetName(typeof(EyePatch), eyePatch));
        loggingManager.WriteToLog("PrismOffset", prismOffset.ToString());
        baguetteIsLogged = true;
    }

    /// <summary>
    /// delete the tables first childs that will be either the 2 baguette parts 
    /// after the user cut it or the only 1 piece of baguette if he fails
    /// </summary>
    public void NextBaguette()
    {
        if(testRunning) {
        trialNumber++;
        trialNumberText.text = string.Format(trialNumberTextTemplate, trialNumber.ToString());
        }

        if (gameObject.transform.childCount > 1)
        {
            Destroy(gameObject.transform.GetChild(1).gameObject);
            Destroy(gameObject.transform.GetChild(0).gameObject);
        }
        if (gameObject.transform.childCount > 0)
            Destroy(gameObject.transform.GetChild(0).gameObject);
        isBaguette = false;
        baguetteIsLogged = false;

        GameObject leftKnife = GameObject.Find("LeftKnife");

        if (mirrorEffect == MirrorEffect.Off) {
                cutter = GameObject.Find("LeftKnife").GetComponent<Cutter>();
        } else{
            cutter = GameObject.Find("MirrorKnife").GetComponent<Cutter>();
        }

        cutter.ResetTriggerRegister();
    }

    public void EmailInputField_OnValueChanged() {
        string regex = @"(^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$)";
        var match = Regex.Match(emailInputField.text, regex, RegexOptions.IgnoreCase);
        if (!match.Success)
        {
            loggingNotice.text = "Logging is Disabled";
            loggingNotice.color = Color.red;
            shouldLog = false;
        } else {
            loggingNotice.text = "Logging is Enabled";
            loggingNotice.color = Color.blue;
            shouldLog = true;
        }
        email = emailInputField.text;
    }

    public void ParticipantNumber_OnValueChanged() {
        Debug.Log("ParticipantInputField called!");
        participantNumber = participantInputField.text;
        trialNumber = 0;
        trialNumberText.text = string.Format(trialNumberTextTemplate, trialNumber.ToString());
    }

    public void StartTest_Toggle() {
        countDown = true;
        hasUploaded = false;
    }

    public void StopTest_Toggle() {
        trialNumber = 0;
        countDown = false;
        testCountdownHolder.SetActive(false);
        preTestCountdown = 3f;
        testRunning = false;
        if (shouldLog && baguetteIsLogged && !hasUploaded) {
            Debug.Log("Uploading Baguette Cut Logs");
            loggingManager.UploadLogs();
            hasUploaded = true;
        }
    }

    public void OnApplicationQuit() {
        if (shouldLog && baguetteIsLogged && !hasUploaded) {
            Debug.Log("Uploading Baguette Cut Logs");
            loggingManager.UploadLogs();
        }
    }

    public void Mirror_ToggleOn() {
        mirrorEffect = MirrorEffect.On;
    }

    public void Mirror_ToggleOff() {
        mirrorEffect = MirrorEffect.Off;
    }

    private void changeBaguetteSize() {
        if (baguetteSize == BaguetteSize.Small) {
            currentBaguette.transform.localScale = baguetteObjectSizes.small;
        } else if (baguetteSize == BaguetteSize.Medium) {
            currentBaguette.transform.localScale = baguetteObjectSizes.medium;
        } else {
            currentBaguette.transform.localScale = baguetteObjectSizes.large;
        }
    }

    public void BaguetteSize_ToggleMedium() {
        baguetteSize = BaguetteSize.Medium;
        changeBaguetteSize();
    }

    public void BaguetteSize_ToggleSmall() {
        baguetteSize = BaguetteSize.Small;
        changeBaguetteSize();
    }

    public void BaguetteSize_ToggleLarge() {
        baguetteSize = BaguetteSize.Large;
        changeBaguetteSize();
    }

    public void EyePatch_ToggleLeft() {
        eyePatch  = EyePatch.Left;
    }

    public void EyePatch_ToggleRight() {
        eyePatch  = EyePatch.Right;
    }
    public void EyePatch_ToggleOff() {
        eyePatch  = EyePatch.Off;
    }

    public void PrismOffset_TogglePlus15() {
        prismOffset = 15;
    }

    public void PrismOffset_ToggleMinus15() {
        prismOffset = -15;
    }

    public void PrismOffset_ToggleZero() {
        prismOffset = 0;
    }

    public void PrismOffset_OnValueChanged() {
        float number;
        
        if (Single.TryParse(prismOffsetInputField.text, out number)) {
            prismOffset = number;
        } else if (prismOffsetInputField.text == "") {
            prismOffset = 0f;
        } else {
            prismOffsetInputField.text = prismOffset.ToString();
        }
        
    }
}
