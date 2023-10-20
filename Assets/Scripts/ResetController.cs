using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetController : MonoBehaviour
{
    public bool isButtonPressed = false;

    // This function will be called when the button is clicked.
    public void OnButtonClick()
    {
        isButtonPressed = true;
    }
}
