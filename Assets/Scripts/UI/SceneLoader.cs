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
<<<<<<< HEAD:Assets/Scripts/SceneLoader.cs
    {   Debug.Log("Retry Called");
=======
    {
>>>>>>> main:Assets/Scripts/UI/SceneLoader.cs
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



}
