using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;
    public string currentSceneName;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void LoadTheScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



}
