using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;
    
    public GameObject pauseMenu;  // Drag your pause menu UI here

    public void Pause()
    {
        pauseMenu.SetActive(true);  // Show the pause menu
        //get pauseText and set it to game Paused
        GameObject pauseText = GameObject.Find("PauseText");
        TextMeshProUGUI tmp = pauseText.GetComponent<TextMeshProUGUI>();
        tmp.SetText("GAME PAUSED.");
        Time.timeScale = 0f;         // Freeze the game time
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);   // Hide the pause menu
        Time.timeScale = 1f;         // Resume the game time
        isPaused = false;
    }
}
