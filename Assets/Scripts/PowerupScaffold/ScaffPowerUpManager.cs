using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class ScaffPowerUpManager : MonoBehaviour
{
    public enum PowerUpType { None, Freeze, MoveOpponent, Magnify, BallSplit } // Added BallSplit
    private PowerUpType[] powerUpArray = { PowerUpType.Freeze, PowerUpType.MoveOpponent, PowerUpType.Magnify, PowerUpType.BallSplit };


    private Paddle paddle1;
    private Paddle paddle2;

    public int player1powerupcount = 1;
    public int player2powerupcount = 1;



    public string testing_powerup = "";

    public string p1powerup = "";
    public string p2powerup = "";

    public bool p1PowerUpActive = false;
    public bool p2PowerUpActive = false;

    // public TextMeshProUGUI player1Powerup;
    // public TextMeshProUGUI player2Powerup;

    private float powerUpCooldown = 10f;
    private float powerUpActiveDuration = 10f;

    public float p1PowerUpTimer = 5f;  // New timer for player 1
    public float p2PowerUpTimer = 5f;  // New timer for player 2


    //USED TO DEFINE THE TIMER FOR TIMER COIN IMAGE
    private float p1CurrentPowerUpTimer = 5f;
    private float p2CurrentPowerUpTimer = 5f;

    public GameObject p1Timer;
    public GameObject p2Timer;

    public Image player1Powerup;
    public Image player2Powerup;

    public Image freezeImage;
    public Image magnifyImage;
    public Image moveOpponentImage;
    public Image splitPowerupImage;

    private void Start()
    {
        Paddle[] paddles = FindObjectsOfType<Paddle>();
        paddle1 = paddles[0].id == 1 ? paddles[0] : paddles[1];
        paddle2 = paddles[1].id == 2 ? paddles[1] : paddles[0];
        AssignRandomPowerUp();
    }

    private void Update()
    {
        // Decrementing the timers when the powerups are active
        if (p1PowerUpActive)
        {
            p1PowerUpTimer = p1PowerUpTimer > 0 ? (p1PowerUpTimer - Time.deltaTime) : 0;
            TimeSpan time = TimeSpan.FromSeconds(p1PowerUpTimer);                       // set the time value
            string seconds = time.ToString("ss");
            char secondDigit = seconds.Length > 1 ? seconds[1] : seconds[0];

            // p1Timer.GetComponentInChildren<TextMeshProUGUI>().text = secondDigit.ToString();
            // p1Timer.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            // p1Timer.GetComponent<Image>().color = Color.green;

            // Assuming p1Timer is a GameObject
            Transform childTransform = p1Timer.transform.Find("Timer1");

            if (childTransform != null)
            {
                Image childImage = childTransform.GetComponent<Image>();
                if (childImage != null)
                {
                    childImage.fillAmount = Mathf.InverseLerp(0, p1CurrentPowerUpTimer, p1PowerUpTimer);
                }
                else
                {
                    Debug.LogError("The child does not have an Image component!");
                }
            }
            else
            {
                Debug.LogError("Child with the specified name not found!");
            }

        }
        if (p2PowerUpActive)
        {
            p2PowerUpTimer = p2PowerUpTimer > 0 ? (p2PowerUpTimer - Time.deltaTime) : 0;
            TimeSpan time = TimeSpan.FromSeconds(p2PowerUpTimer);                       // set the time value
            string seconds = time.ToString("ss");
            char secondDigit = seconds.Length > 1 ? seconds[1] : seconds[0];

            // p2Timer.GetComponentInChildren<TextMeshProUGUI>().text = secondDigit.ToString();
            // p2Timer.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            // p2Timer.GetComponent<Image>().color = Color.green;

            // Assuming p2Timer is a GameObject
            Transform childTransform = p2Timer.transform.Find("Timer2");

            if (childTransform != null)
            {
                Image childImage = childTransform.GetComponent<Image>();
                if (childImage != null)
                {
                    childImage.fillAmount = Mathf.InverseLerp(0, p2CurrentPowerUpTimer, p2PowerUpTimer);
                }
                else
                {
                    Debug.LogError("The child does not have an Image component!");
                }
            }
            else
            {
                Debug.LogError("Child with the specified name not found!");
            }
        }

        //if (Input.GetKeyDown(KeyCode.Q) && !p1PowerUpActive && p1powerup != ""&& !p2PowerUpActive)
        //{

        //    ActivatePowerUp(paddle1, p1powerup);
        //    p1PowerUpActive = true;
        //    // player1Powerup.color = Color.green;

        //    // if (p1powerup == "MoveOpponent")
        //    // {
        //    //     player1Powerup.text = "Move Opponent";
        //    // }
        //    // else
        //    // {
        //    //     player1Powerup.text = p1powerup;
        //    // }
        //    Transform timerTransform = p1Timer.transform.Find("Timer1");
        //    Image timerImage = timerTransform.GetComponent<Image>();
        //    SetPowerUpImage(player1Powerup, p1powerup, timerImage);


        //    p1powerup = "";
        //    Invoke("DeactivateP1PowerUp", powerUpActiveDuration);
        //}

        //if (Input.GetKeyDown(KeyCode.Slash) && !p2PowerUpActive && p2powerup != ""&& !p1PowerUpActive)
        //{
        //    ActivatePowerUp(paddle2, p2powerup);
        //    p2PowerUpActive = true;
        //    // player2Powerup.color = Color.green;

        //    // if (p2powerup == "MoveOpponent")
        //    // {
        //    //     player2Powerup.text = "Move Opponent";
        //    // }
        //    // else
        //    // {
        //    //     player2Powerup.text = p2powerup;
        //    // }


        //    Transform timerTransform = p2Timer.transform.Find("Timer2");
        //    Image timerImage = timerTransform.GetComponent<Image>();
        //    SetPowerUpImage(player2Powerup, p2powerup, timerImage);

        //    p2powerup = "";
        //    Invoke("DeactivateP2PowerUp", powerUpActiveDuration);
        //}


        if (Input.GetKeyDown(KeyCode.Q) && !p1PowerUpActive && p1powerup != "")
        {
            ActivatePowerUp(paddle1, p1powerup);
            p1PowerUpActive = true;
            p1PowerUpTimer = (p1powerup == "Freeze") ? 3f : 5f;
            p1CurrentPowerUpTimer = p1PowerUpTimer;
            Transform timerTransform = p1Timer.transform.Find("Timer1");
            Image timerImage = timerTransform.GetComponent<Image>();
            SetPowerUpImage(player1Powerup, p1powerup, timerImage);
            p1powerup = "";
            Invoke("DeactivateP1PowerUp", powerUpActiveDuration);
        }

        if (Input.GetKeyDown(KeyCode.Slash) && !p2PowerUpActive && p2powerup != "")
        {
            ActivatePowerUp(paddle2, p2powerup);
            p2PowerUpActive = true;
            p2PowerUpTimer = (p2powerup == "Freeze") ? 3f : 5f;
            p2CurrentPowerUpTimer = p2PowerUpTimer;
            Transform timerTransform = p2Timer.transform.Find("Timer2");
            Image timerImage = timerTransform.GetComponent<Image>();
            SetPowerUpImage(player2Powerup, p2powerup, timerImage);
            p2powerup = "";
            Invoke("DeactivateP2PowerUp", powerUpActiveDuration);
        }




    }

    void AssignRandomPowerUp()
    {

        p1powerup = testing_powerup;
        //if(p1powerup == "SplitBall" && ballsInScoreZone == 2 ){
        //    p1powerup = powerUpArrayWithoutSplitBall[Random.Range(0, powerUpArrayWithoutSplitBall.Length)].ToString();
        //}
        Transform timerTransform1 = p1Timer.transform.Find("Timer1");
        Image timerImage1 = timerTransform1.GetComponent<Image>();
        SetPowerUpImage(player1Powerup, p1powerup, timerImage1);



        p2powerup = testing_powerup;

        //if(p2powerup == "SplitBall" && ballsInScoreZone == 2 ){
        //    p2powerup = powerUpArrayWithoutSplitBall[Random.Range(0, powerUpArrayWithoutSplitBall.Length)].ToString();
        //}
        Transform timerTransform2 = p2Timer.transform.Find("Timer2");
        Image timerImage2 = timerTransform2.GetComponent<Image>();
        SetPowerUpImage(player2Powerup, p2powerup, timerImage2);
        // //change from here
        // player1Powerup.color = Color.red;
        // if (p1powerup == "MoveOpponent")
        // {
        //     player1Powerup.text = "Move Opponent";
        // }
        // else
        // {
        //     player1Powerup.text = p1powerup;
        // }

        // player2Powerup.color = Color.red;
        // if (p2powerup == "MoveOpponent")
        // {
        //     player2Powerup.text = "Move Opponent";
        // }
        // else
        // {
        //     player2Powerup.text = p2powerup;
        // }


    }

    //void ActivatePowerUp(Paddle paddle, string powerUpName)
    //{
    //    if (paddle == null) return;

    //    switch (powerUpName)
    //    {
    //        case "Freeze":
    //            paddle.gameObject.GetComponent<PaddleFreezePowerUp>().FreezeOpponent();
    //            break;
    //        case "Magnify":
    //            paddle.gameObject.GetComponent<PaddleSizePowerUp>().ActivatePowerUp();
    //            break;
    //        case "MoveOpponent":
    //            paddle.gameObject.GetComponent<PaddleMovePowerUp>().ShiftOpponentPosition();
    //            break;
    //    }
    //}
    void ActivatePowerUp(Paddle paddle, string powerUpName)
    {
        if (paddle == null)
        {
            Debug.LogError("Paddle is null in ActivatePowerUp method.");
            return;
        }
        // if (powerUpName=="SplitBall" && ball == null)
        // {
        //     Debug.LogError("Ball is null in ActivatePowerUp method.");
        //     return;
        // }

        switch (powerUpName)
        {
            case "Freeze":
                var freezePowerUp = paddle.gameObject.GetComponent<PaddleFreezePowerUp>();
                if (freezePowerUp == null)
                {
                    Debug.LogError("PaddleFreezePowerUp script is missing on paddle " + paddle.id);
                    return;
                }
                freezePowerUp.FreezeOpponent();
                break;
            case "Magnify":
                var sizePowerUp = paddle.gameObject.GetComponent<PaddleSizePowerUp>();
                if (sizePowerUp == null)
                {
                    Debug.LogError("PaddleSizePowerUp script is missing on paddle " + paddle.id);
                    return;
                }
                sizePowerUp.ActivatePowerUp();
                break;
            case "MoveOpponent":
                var movePowerUp = paddle.gameObject.GetComponent<PaddleMovePowerUp>();
                if (movePowerUp == null)
                {
                    Debug.LogError("PaddleMovePowerUp script is missing on paddle " + paddle.id);
                    return;
                }
                movePowerUp.ShiftOpponentPosition();
                break;
        }
    }


    void DeactivateP1PowerUp()
    {
        player1powerupcount++;

        // player1Powerup.text = "";
        // p1Timer.SetActive(false);
        // p1PowerUpActive = false;
        // p1PowerUpTimer = 5f;  // Resetting the timer
        // TimeSpan time = TimeSpan.FromSeconds(p1PowerUpTimer);                       // set the time value
        // string seconds = time.ToString("ss");
        // char secondDigit = seconds.Length > 1 ? seconds[1] : seconds[0];

        // p1Timer.GetComponentInChildren<TextMeshProUGUI>().text = secondDigit.ToString();
        // p1Timer.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        // p1Timer.GetComponent<Image>().color = Color.red;
        //player1Powerup.gameObject.SetActive(false);
        p1Timer.SetActive(false);
        p1PowerUpActive = false;
        p1PowerUpTimer = 5f;  // Resetting the timer
        TimeSpan time = TimeSpan.FromSeconds(p1PowerUpTimer);
        string seconds = time.ToString("ss");
        char secondDigit = seconds.Length > 1 ? seconds[1] : seconds[0];

        // Assuming p1Timer is a GameObject
        Transform childTransform = p1Timer.transform.Find("Timer1");

        if (childTransform != null)
        {
            Image childImage = childTransform.GetComponent<Image>();
            if (childImage != null)
            {
                childImage.fillAmount = Mathf.InverseLerp(0, p1CurrentPowerUpTimer, p1PowerUpTimer);
            }
            else
            {
                Debug.LogError("The child does not have an Image component!");
            }
        }
        else
        {
            Debug.LogError("Child with the specified name not found!");
        }

        Invoke("AssignPowerUpToP1", powerUpCooldown);
    }

    void DeactivateP2PowerUp()
    {
        player2powerupcount++;

        // player2Powerup.text = "";
        // p2Timer.SetActive(false);
        // p2PowerUpActive = false;
        // p2PowerUpTimer = 5f;  // Resetting the timer
        // TimeSpan time = TimeSpan.FromSeconds(p2PowerUpTimer);                       // set the time value
        // string seconds = time.ToString("ss");
        // char secondDigit = seconds.Length > 1 ? seconds[1] : seconds[0];

        // p2Timer.GetComponentInChildren<TextMeshProUGUI>().text = secondDigit.ToString();
        // p2Timer.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        // p2Timer.GetComponent<Image>().color = Color.red;

        //player2Powerup.gameObject.SetActive(false);
        p2Timer.SetActive(false);
        p2PowerUpActive = false;
        p2PowerUpTimer = 5f;  // Resetting the timer
        TimeSpan time = TimeSpan.FromSeconds(p2PowerUpTimer);                       // set the time value
        string seconds = time.ToString("ss");
        char secondDigit = seconds.Length > 1 ? seconds[1] : seconds[0];

        // Assuming p2Timer is a GameObject
        Transform childTransform = p2Timer.transform.Find("Timer2");

        if (childTransform != null)
        {
            Image childImage = childTransform.GetComponent<Image>();
            if (childImage != null)
            {
                childImage.fillAmount = Mathf.InverseLerp(0, p2CurrentPowerUpTimer, p2PowerUpTimer);
            }
            else
            {
                Debug.LogError("The child does not have an Image component!");
            }
        }
        else
        {
            Debug.LogError("Child with the specified name not found!");
        }
        Invoke("AssignPowerUpToP2", powerUpCooldown);
    }

    void AssignPowerUpToP1()
    {
        player1Powerup.gameObject.SetActive(true);
        // p1Timer.SetActive(true);
        if (player1powerupcount < 2)
        {
            // player1Powerup.color = Color.red;
            p1Timer.SetActive(true);

            p1powerup = testing_powerup;
            p1PowerUpTimer = 5f;  // Resetting the timer



            // if (p1powerup == "MoveOpponent")
            // {
            //     player1Powerup.text = "Move Opponent";
            // }
            // else
            // {
            //     player1Powerup.text = p1powerup;
            // }

            Transform timerTransform = p1Timer.transform.Find("Timer1");
            Image timerImage = timerTransform.GetComponent<Image>();
            SetPowerUpImage(player1Powerup, p1powerup, timerImage);

        }
        else
        {
            int last = 0;

            if (testing_powerup == "Freeze")
            {
                last = 1;
            }
            else if (testing_powerup == "MoveOpponent")
            {
                last = 2;

            }
            else if (testing_powerup == "Magnify")
            {
                last = 3;
            }
            else if (testing_powerup == "BallSplit")
            {
                last = 4;
            }
            // player1Powerup.color = Color.red;
            p1Timer.SetActive(true);

            p1powerup = powerUpArray[Random.Range(0, last)].ToString();
            p1PowerUpTimer = 5f;  // Resetting the timer

            // p1powerup = powerUpArray[Random.Range(0, powerUpArray.Length)].ToString();
            // p1PowerUpTimer = 5f;  // Resetting the timer
            Transform timerTransform = p1Timer.transform.Find("Timer1");
            Image timerImage = timerTransform.GetComponent<Image>();
            SetPowerUpImage(player1Powerup, p1powerup, timerImage);



            // if (p1powerup == "MoveOpponent")
            // {
            //     player1Powerup.text = "Move Opponent";
            // }
            // else
            // {
            //     player1Powerup.text = p1powerup;
            // }
        }
        if (p1powerup == "Freeze")
        {
            p1PowerUpTimer = 3f;
        }
        else
        {
            p1PowerUpTimer = 5f;
        }
        p1CurrentPowerUpTimer = p1PowerUpTimer;
    }

    void AssignPowerUpToP2()
    {
        player2Powerup.gameObject.SetActive(true);

        if (player2powerupcount < 2)
        {
            // player2Powerup.color = Color.red;
            p2Timer.SetActive(true);
            p2powerup = testing_powerup;
            p2PowerUpTimer = 5f;  // Resetting the timer


            // if (p2powerup == "MoveOpponent")
            // {
            //     player2Powerup.text = "Move Opponent";
            // }
            // else
            // {
            //     player2Powerup.text = p2powerup;
            // }
            Transform timerTransform = p2Timer.transform.Find("Timer2");
            Image timerImage = timerTransform.GetComponent<Image>();
            SetPowerUpImage(player2Powerup, p2powerup, timerImage);
        }
        else
        {
            int last = 0;

            if (testing_powerup == "Freeze")
            {
                last = 1;
            }
            else if (testing_powerup == "MoveOpponent")
            {
                last = 2;

            }
            else if (testing_powerup == "Magnify")
            {
                last = 3;
            }
            else if (testing_powerup == "BallSplit")
            {
                last = 4;
            }
            // player2Powerup.color = Color.red;
            p2Timer.SetActive(true);

            p2powerup = powerUpArray[Random.Range(0, last)].ToString();
            p2PowerUpTimer = 5f;  // Resetting the timer



            // if (p2powerup == "MoveOpponent")
            // {
            //     player2Powerup.text = "Move Opponent";
            // }
            // else
            // {
            //     player2Powerup.text = p2powerup;
            // }

            Transform timerTransform = p2Timer.transform.Find("Timer2");
            Image timerImage = timerTransform.GetComponent<Image>();
            SetPowerUpImage(player2Powerup, p2powerup, timerImage);
        }

        if (p2powerup == "Freeze")
        {
            p2PowerUpTimer = 3f;
        }
        else
        {
            p2PowerUpTimer = 5f;
        }
        p2CurrentPowerUpTimer = p2PowerUpTimer;
    }
    void SetPowerUpImage(Image imageComponent, string powerUpName, Image timerImage)
    {
        switch (powerUpName)
        {
            case "Freeze":
                imageComponent.sprite = freezeImage.sprite;
                timerImage.sprite = freezeImage.sprite;
                break;
            case "Magnify":
                imageComponent.sprite = magnifyImage.sprite;
                timerImage.sprite = magnifyImage.sprite;
                break;
            case "MoveOpponent":
                imageComponent.sprite = moveOpponentImage.sprite;
                timerImage.sprite = moveOpponentImage.sprite;
                break;
            case "SplitBall":
                imageComponent.sprite = splitPowerupImage.sprite;
                timerImage.sprite = splitPowerupImage.sprite;
                break;
            default:
                Debug.LogError("Unknown power-up: " + powerUpName);
                break;
        }
    }

    //

    //void AssignPowerUpToP1()
    //{
    //    player1Powerup.color = Color.red;
    //    p1Timer.SetActive(true);

    //    p1powerup = powerUpArray[Random.Range(0, powerUpArray.Length)].ToString();
    //    p1PowerUpTimer = 5f;  // Resetting the timer
    //    if (p1powerup == "MoveOpponent")
    //    {
    //        player1Powerup.text = "Move Opponent";
    //    }
    //    else
    //    {
    //        player1Powerup.text = p1powerup;
    //    }
    //}

    //void AssignPowerUpToP2()
    //{
    //    player2Powerup.color = Color.red;
    //    p2Timer.SetActive(true);

    //    p2powerup = powerUpArray[Random.Range(0, powerUpArray.Length)].ToString();
    //    p2PowerUpTimer = 5f;  // Resetting the timer
    //    if (p2powerup == "MoveOpponent")
    //    {
    //        player2Powerup.text = "Move Opponent";
    //    }
    //    else
    //    {
    //        player2Powerup.text = p2powerup;
    //    }
    //}
}
