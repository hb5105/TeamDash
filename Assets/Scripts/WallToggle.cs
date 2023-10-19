using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WallToggle : MonoBehaviour
{
    public GameObject[] normalWalls;    // Drag your normal rectangular walls here
    public GameObject[] pointedWalls;   // Drag your '^' and 'v' walls here
    public TextMeshProUGUI ScoreTextLeft;
    public TextMeshProUGUI ScoreTextRight;
    public TextMeshProUGUI Word1;
    public TextMeshProUGUI Word2;
    public TextMeshProUGUI Powerup1;
    public TextMeshProUGUI Powerup2;
    public void Start()
    {
        SwitchToNormalWalls();  // This sets the walls to their normal state
    }
    public void SwitchToNormalWalls()
    {    foreach (var wall in pointedWalls)
        {
            wall.SetActive(false);
        }
        foreach (var wall in normalWalls)
        {
            wall.SetActive(true);
        }
 
    }

    public void SwitchToPointedWalls()
    {
        Camera mainCam = Camera.main;  
        mainCam.orthographicSize = 8;

        RectTransform rectTransform = ScoreTextLeft.rectTransform;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 83);

        rectTransform = ScoreTextRight.rectTransform;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 83);

        rectTransform = Word1.rectTransform;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -117);

        rectTransform = Word2.rectTransform;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -117);

        rectTransform = Powerup1.rectTransform;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 113);

        rectTransform = Powerup2.rectTransform;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 113);

        foreach (var wall in normalWalls)
        {
            wall.SetActive(false);
        }
        foreach (var wall in pointedWalls)
        {
            wall.SetActive(true);
        }
    }
}
