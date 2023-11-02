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
    //public TextMeshProUGUI Powerup1;
    //public TextMeshProUGUI Powerup2;
    //public GameObject p1Timer;
    //public GameObject p2Timer;
    public TextMeshProUGUI p1BulletLeft;
    public TextMeshProUGUI p2BulletLeft;
    public GameObject p1Bullet;
    public GameObject p2Bullet;
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

        //rectTransform = Powerup1.rectTransform;
        //rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 113);

        //rectTransform = Powerup2.rectTransform;
        //rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 113);

        //rectTransform = p1Timer.GetComponent<RectTransform>();
        //rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 113);

        //rectTransform = p2Timer.GetComponent<RectTransform>();
        //rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 113);

        rectTransform = p1BulletLeft.rectTransform;
        rectTransform.anchoredPosition = new Vector2(-200, -160);

        rectTransform = p2BulletLeft.rectTransform;
        rectTransform.anchoredPosition = new Vector2(220, -160);

        rectTransform = p1Bullet.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(-220, -160);

        rectTransform = p2Bullet.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(200, -160);

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
