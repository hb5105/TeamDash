using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;
    public string currentSceneName;
    public GameObject startMenu;

     private void Start() {
        if(startMenu != null){
            Time.timeScale = 0f;
        startMenu.SetActive(true);
        }
    }
    public void LoadTheScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame(){
        Time.timeScale = 1f;
        startMenu.SetActive(false);
        // SceneManager.LoadScene(sceneName);

    }


}
