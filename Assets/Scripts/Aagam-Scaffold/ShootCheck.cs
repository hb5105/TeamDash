using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCheck : MonoBehaviour
{
    private bool spaceKeyPressed = false;
    private bool rightShiftKeyPressed = false;
    void Start()
    {
        
    }
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceKeyPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            rightShiftKeyPressed = true;
        }
        if (spaceKeyPressed && rightShiftKeyPressed)
        {
            gameObject.SetActive(false);
        }
    }
}
