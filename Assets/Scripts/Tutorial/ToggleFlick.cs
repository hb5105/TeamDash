using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleFlick : MonoBehaviour
{
    private bool AKeyPressed = false;
    private bool DKeyPressed = false;
    private bool leftKeyPressed = false;
    private bool rightKeyPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            AKeyPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DKeyPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftKeyPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightKeyPressed = true;
        }
        if (AKeyPressed && DKeyPressed && rightKeyPressed && leftKeyPressed)
        {
            gameObject.SetActive(false);
        }
    }
}
