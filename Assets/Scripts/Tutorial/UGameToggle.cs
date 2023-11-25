using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UGameToggle : MonoBehaviour
{
    public GameObject modal;
    public GameObject startScreen;
    public GameObject countDown;
    public GameObject darkBg;

    public void onToggle()
    {
        darkBg.SetActive(false);
        countDown.SetActive(false);
        startScreen.SetActive(false);
        modal.SetActive(true);
    }
    public void offToggle()
    {
        modal.SetActive(false);
        countDown.SetActive(true);
        startScreen.SetActive(true);
        darkBg.SetActive(true);
    }
}
