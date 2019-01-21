using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public GameObject menu;
    private string accuracyTest = "CircleTest";
    private string fovCalibration = "Field of view";
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            menu.gameObject.SetActive(!menu.activeSelf);
    }

    public void StartAccuTest()
    {
        if (SceneManager.GetActiveScene().name == "Field of view")
            StopFovCalibration();
        mainCamera.enabled = false;
        StartCoroutine(LoadCurrentScene(accuracyTest));

        menu.gameObject.SetActive(!menu.activeSelf);
    }

    public void StopAccuTest()
    {
        SceneManager.UnloadSceneAsync(accuracyTest);
        menu.gameObject.SetActive(!menu.activeSelf);
        mainCamera.enabled = true;
    }

    public void StartFovCalibration()
    {
        if (SceneManager.GetActiveScene().name == "CircleTest")
            StopAccuTest();
        mainCamera.enabled = false;
        StartCoroutine(LoadCurrentScene(fovCalibration));
        menu.gameObject.SetActive(!menu.activeSelf);
    }

    public void StopFovCalibration()
    {
        SceneManager.UnloadSceneAsync(fovCalibration);
        menu.gameObject.SetActive(!menu.activeSelf);
        mainCamera.enabled = true;
    }

    IEnumerator LoadCurrentScene(string sceneName)
    {
        AsyncOperation asyncScene = SceneManager.LoadSceneAsync(sceneName
            , LoadSceneMode.Additive);

        while (!asyncScene.isDone)
        {
            yield return null;
        }
    }
}
