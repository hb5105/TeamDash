using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WallToggle : MonoBehaviour
{
    public GameObject[] normalWalls;    // Drag your normal rectangular walls here
    public GameObject[] pointedWalls;   // Drag your '^' and 'v' walls here
    public TextMeshProUGUI ScoreTextLeft;
    public TextMeshProUGUI ScoreTextRight;
    //public Image Bg1;
    //public Image Bg2;
    public TextMeshProUGUI Word1;
    public TextMeshProUGUI Word2;
    public Image Powerup1;
    public Image Powerup2;
    public GameObject p1Timer;
    public GameObject p2Timer;
    public TextMeshProUGUI p1BulletLeft;
    public TextMeshProUGUI p2BulletLeft;
    public GameObject p1Bullet;
    public GameObject p2Bullet;
    public GameObject normalField;
    public GameObject pointedField;
    public GameObject player1Paddle;
    public GameObject player2Paddle;
    // public GameObject ball;
    public GameObject scoreZoneLeft;
    public GameObject scoreZoneRight;
    public GameObject scoreBoard;
    public Vector3 paddleScaleBefore;
    public Vector3 paddleScaleAfter;

    public bool isPointedWalls = false; // This is a flag to check if the walls are in their normal state or not
    public void Start()
    {
        Camera mainCam = Camera.main;  
        mainCam.orthographicSize = 5;
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
        normalField.SetActive(true);
        pointedField.SetActive(false);
        isPointedWalls = false;
    }

    public void SwitchToPointedWalls()
    {
        isPointedWalls = true;
        foreach (var wall in normalWalls)
        {
            wall.SetActive(false);
        }

        foreach (var wall in pointedWalls)
        {
            wall.SetActive(true);
        }

        Camera mainCam = Camera.main;  
        mainCam.orthographicSize = 8;

        normalField.SetActive(false);
        pointedField.SetActive(true);

        // RectTransform rectTransform = ScoreTextLeft.rectTransform;
        // rectTransform.anchoredPosition = new Vector2(-186,224);

        // rectTransform = ScoreTextRight.rectTransform;
        // rectTransform.anchoredPosition = new Vector2(304, 224);

        //rectTransform = Bg1.rectTransform;
        //rectTransform.anchoredPosition = new Vector2(-117, 145);

        //rectTransform = Bg2.rectTransform;
        //rectTransform.anchoredPosition = new Vector2(121, 145);

        // rectTransform = Word1.rectTransform;
        // rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -117);

        // rectTransform = Word2.rectTransform;
        // rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -117);

        // rectTransform = Powerup1.rectTransform;
        // rectTransform.anchoredPosition = new Vector2(-301, 204);

        // rectTransform = Powerup2.rectTransform;
        // rectTransform.anchoredPosition = new Vector2(301, 204);

        // rectTransform = p1Timer.GetComponent<RectTransform>();
        // rectTransform.anchoredPosition = new Vector2(-301, 204);

        // rectTransform = p2Timer.GetComponent<RectTransform>();
        // rectTransform.anchoredPosition = new Vector2(301, 204);

        // rectTransform = p1BulletLeft.rectTransform;
        // rectTransform.anchoredPosition = new Vector2(-200, -160);

        // rectTransform = p2BulletLeft.rectTransform;
        // rectTransform.anchoredPosition = new Vector2(220, -160);

        // rectTransform = p1Bullet.GetComponent<RectTransform>();
        // rectTransform.anchoredPosition = new Vector2(-220, -160);

        // rectTransform = p2Bullet.GetComponent<RectTransform>();
        // rectTransform.anchoredPosition = new Vector2(200, -160);

        player1Paddle.transform.position = new Vector3(-11, 1, gameObject.transform.position.z);
        Vector3 paddle1Scale = player1Paddle.transform.localScale;
        paddleScaleBefore = paddle1Scale;
        paddle1Scale.x = paddle1Scale.x*2;
        paddle1Scale.y = (float)(paddle1Scale.y*1.54);
        paddleScaleAfter = paddle1Scale;
        player1Paddle.transform.localScale = paddle1Scale;

        player2Paddle.transform.position = new Vector3(11, 1, gameObject.transform.position.z);
        Vector3 paddle2Scale = player2Paddle.transform.localScale;
        paddle2Scale.x = paddle2Scale.x * 2;
        paddle2Scale.y = (float)(paddle2Scale.y * 1.54);
        player2Paddle.transform.localScale = paddle2Scale;

        // Vector3 ballScale = ball.transform.localScale;
        // ballScale.x = 0.75f;
        // ballScale.y = 0.75f;
        // ball.transform.localScale = ballScale;

        scoreZoneLeft.transform.position = new Vector3(-13, 0, gameObject.transform.position.z);

        scoreZoneRight.transform.position = new Vector3(13, 0, gameObject.transform.position.z);

        scoreBoard.SetActive(false);
    }
}
