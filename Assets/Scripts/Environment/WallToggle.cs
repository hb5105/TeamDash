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
    public GameObject p1Timer;
    public GameObject p2Timer;
    public TextMeshProUGUI p1BulletLeft;
    public TextMeshProUGUI p2BulletLeft;
    public GameObject p1Bullet;
    public GameObject p2Bullet;
    public void Start()
    {
        SwitchToNormalWalls();  // This sets the walls to their normal state
    }
    public void SwitchToNormalWalls()
    {   Camera mainCam = Camera.main;  
        mainCam.orthographicSize = 5; 
        foreach (var wall in pointedWalls)
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
        Debug.Log("stleft"+rectTransform.anchoredPosition);
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 83);

        rectTransform = ScoreTextRight.rectTransform;
        Debug.Log("stright"+rectTransform.anchoredPosition);
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 83);

        rectTransform = Word1.rectTransform;
        Debug.Log("w1"+rectTransform.anchoredPosition);
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -117);

        rectTransform = Word2.rectTransform;
        Debug.Log("w2"+rectTransform.anchoredPosition);
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -117);

        rectTransform = Powerup1.rectTransform;
        Debug.Log("pup1"+rectTransform.anchoredPosition);
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 113);

        rectTransform = Powerup2.rectTransform;
        Debug.Log("pup2"+rectTransform.anchoredPosition);
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 113);

        rectTransform = p1Timer.GetComponent<RectTransform>();
        Debug.Log("p1timer"+rectTransform.anchoredPosition);
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 113);

        rectTransform = p2Timer.GetComponent<RectTransform>();
        Debug.Log("p2timer"+rectTransform.anchoredPosition);
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 113);

        rectTransform = p1BulletLeft.rectTransform;
        Debug.Log("p1bul"+rectTransform.anchoredPosition);
        rectTransform.anchoredPosition = new Vector2(-200, -163);

        rectTransform = p2BulletLeft.rectTransform;
        Debug.Log("p2bul"+rectTransform.anchoredPosition);
        rectTransform.anchoredPosition = new Vector2(220, -163);

        rectTransform = p1Bullet.GetComponent<RectTransform>();
        Debug.Log("p1bulcomp"+rectTransform.anchoredPosition);
        rectTransform.anchoredPosition = new Vector2(-220, -163);

        rectTransform = p2Bullet.GetComponent<RectTransform>();
        Debug.Log("p2bulcomp"+rectTransform.anchoredPosition);
        rectTransform.anchoredPosition = new Vector2(200, -163);

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
