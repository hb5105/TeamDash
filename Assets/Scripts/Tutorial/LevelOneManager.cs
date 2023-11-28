using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneManager : MonoBehaviour
{
    public GameObject MoveControlsUp1;
    public GameObject MoveControlsDown1;
    public GameObject MoveControlsUp2;
    public GameObject MoveControlsDown2;

    //word bubbles
    public GameObject GainLetterBubble1;
    public GameObject CompleteWordBubble1;
    public GameObject GainLetterBubble2;
    public GameObject CompleteWordBubble2;
    public TutorialToggle toggle;
    private void Update()
    {
        if (!toggle.isTutorialActive)
        {
            if(Input.GetKey(KeyCode.W))
            {
                MoveControlsUp1.SetActive(false);
            }
            if (Input.GetKey(KeyCode.S))
            {
                MoveControlsDown1.SetActive(false);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                MoveControlsUp2.SetActive(false);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                MoveControlsDown2.SetActive(false);
            }
        }
        


    }

    public IEnumerator ToggleWordBubbles(int choice)
    {
        
        switch (choice)
        {
            case 1:
                GainLetterBubble1.SetActive(true);
                yield return new WaitForSeconds(2);
                GainLetterBubble1 .SetActive(false);
                break;
            case 2:
                CompleteWordBubble1.SetActive(true);
                yield return new WaitForSeconds(2);
                CompleteWordBubble1.SetActive(false);
                break;
            case 3:
                GainLetterBubble2.SetActive(true);
                yield return new WaitForSeconds(2);
                GainLetterBubble2.SetActive(false);
                break;
            case 4:
                CompleteWordBubble2.SetActive(true);
                yield return new WaitForSeconds(2);
                CompleteWordBubble2.SetActive(false);
                break;
        }
           
    }
}
