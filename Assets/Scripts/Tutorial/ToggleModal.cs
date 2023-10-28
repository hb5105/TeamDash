using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleModal : MonoBehaviour
{
    public GameObject modal;

    public void onToggle()
    {
        modal.SetActive(true);
    }
    public void offToggle()
    {
        modal.SetActive(false);
    }
}
