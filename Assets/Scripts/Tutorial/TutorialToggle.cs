using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialToggle : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject[] tutorialParts;
    public GameObject countDown;
    private GameObject currTutorial;

    void Start()
    {
        for(int i = 0; i < tutorialParts.Length; i++)
        {
            tutorialParts[i].SetActive(false);
        }
    }
    public void StartScreenOff()
    {
        startScreen.SetActive(false);
        StartTutorial();
    }

    public void StartTutorial()
    {
        countDown.SetActive(false);
        tutorialParts[0].SetActive(true);
        currTutorial = tutorialParts[0];

    }

    public void NextTutorial()
    {
        currTutorial.SetActive(false);
        IncrementCurrentTutorial();
        if (currTutorial == null)
        {
            return;
        }
        else
        {
            currTutorial.SetActive(true);
        }
    }

    void IncrementCurrentTutorial()
    {
        int currIndex = Array.IndexOf(tutorialParts, currTutorial);

        if (currIndex >= 0 && currIndex < tutorialParts.Length - 1)
        {
            currTutorial = tutorialParts[currIndex + 1];
        }
        else
        {
            currTutorial = null;
        }
    }

    public void StartGame()
    {
        countDown.SetActive(true);
        currTutorial.SetActive(false);
        Time.timeScale = 1f;
        
    }
}
