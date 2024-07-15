using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

public class GMSceneSwitcher : MonoBehaviour
{
    private List<string> sceneNames = new List<string>();
    private string currentSceneName;
    private Dictionary<string, bool> sceneState;

    private void Start()
    {
        sceneState = new Dictionary<string, bool>();

        // Initialize the scene states as inactive
        foreach (string sceneName in sceneNames)
        {
            sceneState[sceneName] = false;
        }

        currentSceneName = SceneManager.GetActiveScene().name;
        sceneState[currentSceneName] = true;
    }

    public void SwitchScene(string sceneName)
    {
        if (currentSceneName != sceneName)
        {
            StartCoroutine(SwitchSceneCoroutine(sceneName));
        }
    }

    private IEnumerator SwitchSceneCoroutine(string sceneName)
    {
        if (!sceneState.ContainsKey(sceneName) || !sceneState[sceneName])
        {
            // Load the new scene additively if it's not already loaded
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            // Wait until the new scene is loaded
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            sceneState[sceneName] = true;
        }

        // Deactivate all other scenes
        ActivateOnlyCurrScene(sceneName);

        currentSceneName = sceneName;
    }

    private void ActivateOnlyCurrScene(string activeSceneName)
    {
        foreach (Scene scene in SceneManager.GetAllScenes())
        {
            if (scene.name != activeSceneName)
            {
                foreach (GameObject obj in scene.GetRootGameObjects())
                {
                    obj.SetActive(false);
                }
            }
            else
            {
                foreach (GameObject obj in scene.GetRootGameObjects())
                {
                    obj.SetActive(true);
                }
            }
        }
    }


    public void UnloadScene(string sceneName)
    {
        if (sceneState.ContainsKey(sceneName) && sceneState[sceneName])
        {
            SceneManager.UnloadSceneAsync(sceneName);
            sceneState[sceneName] = false;
        }
    }
    public void UnloadCurrentScene()
    {
        SceneManager.UnloadSceneAsync(currentSceneName);
    }


    // Make GMSceneSwitcher a singleton object
    private static GMSceneSwitcher _instance;

    public static GMSceneSwitcher Instance
    {
        get {
            if(!_instance)
            {
                _instance = FindObjectOfType(typeof(GMSceneSwitcher)) as GMSceneSwitcher;
                if (_instance == null)
                    Debug.Log("no Singleton GMSceneSwitcher");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null) _instance = this;
        else if (_instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}