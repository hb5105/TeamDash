using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UGameToggle : MonoBehaviour
{
    public GameObject modal;
    public GameObject startScreen;
    public GameObject pauseMenu;
    public GameObject countDown;
    public GameObject darkBg;
    public bool startClicked = false;
    public bool pauseClicked = false;
    public void onToggle()
    {
        //darkBg.SetActive(false);
        //countDown.SetActive(false);
        startScreen.SetActive(false);
        startClicked = true;
        pauseClicked = false;
        modal.SetActive(true);
    }
    public void onPauseToggle()
    {
        //darkBg.SetActive(false);
        //countDown.SetActive(false);
        pauseMenu.SetActive(false);
        startClicked = false;
        pauseClicked = true;
        
        modal.SetActive(true);

    }
    public void offToggle()
    {
       
        modal.SetActive(false);
        if (startClicked)
        {
            startScreen.SetActive(true);
        }
        if (pauseClicked)
        {
            pauseMenu.SetActive(true);
        }
        //countDown.SetActive(true);
        //startScreen.SetActive(true);
        //darkBg.SetActive(true);
    }
}
