using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;
    public string currentSceneName;

    public void LoadTheScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void RetryButton()
    {   Debug.Log("Retry Called");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



}
