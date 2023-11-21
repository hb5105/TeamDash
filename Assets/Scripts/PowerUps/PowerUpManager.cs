using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using System.Collections;

public class PowerUpManager : MonoBehaviour
{
    public enum PowerUpType { None, Freeze, Magnify, MoveOpponent, SplitBall}
    private PowerUpType[] powerUpArray = { PowerUpType.Freeze, PowerUpType.Magnify, PowerUpType.MoveOpponent,PowerUpType.SplitBall};
    private PowerUpType[] powerUpArrayWithoutSplitBall = { PowerUpType.Freeze, PowerUpType.Magnify, PowerUpType.MoveOpponent};
    private Paddle paddle1;
    private Paddle paddle2;

    private Ball ball;
    private Ball[] balls; 

    public ScoreZone leftScoreZone;
    public ScoreZone rightScoreZone;
    public int ballsInScoreZone = 0;

    public string p1powerup = "";
    public string p2powerup = "";

    public bool p1PowerUpActive = false;
    public bool p2PowerUpActive = false;

    private float powerUpCooldown = 10f;
    private float powerUpActiveDuration = 10f;

    public float p1PowerUpTimer = 5f;  // New timer for player 1
    public float p2PowerUpTimer = 5f;  // New timer for player 2

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
    {   getBallBetweenScoreZones();
        // Decrementing the timers when the powerups are active
        if (p1PowerUpActive)
        {
            p1PowerUpTimer = p1PowerUpTimer > 0 ? (p1PowerUpTimer - Time.deltaTime) : 0;
            TimeSpan time = TimeSpan.FromSeconds(p1PowerUpTimer);                       // set the time value
            string seconds = time.ToString("ss");
            char secondDigit = seconds.Length > 1 ? seconds[1] : seconds[0];

            // Assuming p1Timer is a GameObject
            Transform childTransform = p1Timer.transform.Find("Timer1");

            if (childTransform != null)
            {
                Image childImage = childTransform.GetComponent<Image>();
                if (childImage != null)
                {
                    childImage.fillAmount = Mathf.InverseLerp(0, 5, p1PowerUpTimer);
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

            // Assuming p2Timer is a GameObject
            Transform childTransform = p2Timer.transform.Find("Timer2");

            if (childTransform != null)
            {
                Image childImage = childTransform.GetComponent<Image>();
                if (childImage != null)
                {
                    childImage.fillAmount = Mathf.InverseLerp(0, 5, p2PowerUpTimer);
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

        if (Input.GetKeyDown(KeyCode.Q) && !p1PowerUpActive && p1powerup != ""&& !p2PowerUpActive)
        {

            player1Powerup.gameObject.SetActive(true);
            p1Timer.SetActive(true);
            ActivatePowerUp(paddle1, p1powerup);
            p1PowerUpActive = true;

            Transform timerTransform = p1Timer.transform.Find("Timer1");
            Image timerImage = timerTransform.GetComponent<Image>();
            SetPowerUpImage(player1Powerup, p1powerup, timerImage);

            //p1powerup = "";
            Invoke("DeactivateP1PowerUp", powerUpActiveDuration);
        }

        if (Input.GetKeyDown(KeyCode.Slash) && !p2PowerUpActive && p2powerup != ""&& !p1PowerUpActive)
        {
            player2Powerup.gameObject.SetActive(true);
            p2Timer.SetActive(true);
            ActivatePowerUp(paddle2, p2powerup);
            p2PowerUpActive = true;

            Transform timerTransform = p2Timer.transform.Find("Timer2");
            Image timerImage = timerTransform.GetComponent<Image>();
            SetPowerUpImage(player2Powerup, p2powerup, timerImage);

            //p2powerup = "";
            Invoke("DeactivateP2PowerUp", powerUpActiveDuration);
        }
    }
    public void getBallBetweenScoreZones(){
         GameObject[] activeBalls = GameObject.FindGameObjectsWithTag("Ball");
        float leftX = leftScoreZone.transform.position.x;
        float rightX = rightScoreZone.transform.position.x;
        int flag =0;
        ballsInScoreZone = 0;
        foreach (GameObject ballobj in activeBalls)
        {
            if (ballobj.transform.position.x > leftX && ballobj.transform.position.x < rightX)
            {   flag=1;
                ball = ballobj.GetComponent<Ball>();
                ballsInScoreZone++;
            }
        }
        if(flag==0){
            //Debug.Log("No ball between score zones");
            ball=null;
        }
    }
    void AssignRandomPowerUp()
    {
        p1powerup = powerUpArray[Random.Range(0, powerUpArray.Length)].ToString();
        if(p1powerup == "SplitBall" && ballsInScoreZone == 2 ){
            p1powerup = powerUpArrayWithoutSplitBall[Random.Range(0, powerUpArrayWithoutSplitBall.Length)].ToString();
        }
        Transform timerTransform1 = p1Timer.transform.Find("Timer1");
        Image timerImage1 = timerTransform1.GetComponent<Image>();
        SetPowerUpImage(player1Powerup, p1powerup, timerImage1);

        p2powerup = powerUpArray[Random.Range(0, powerUpArray.Length)].ToString();
        if(p2powerup == "SplitBall" && ballsInScoreZone == 2 ){
            p2powerup = powerUpArrayWithoutSplitBall[Random.Range(0, powerUpArrayWithoutSplitBall.Length)].ToString();
        }
        Transform timerTransform2 = p2Timer.transform.Find("Timer2");
        Image timerImage2 = timerTransform2.GetComponent<Image>();
        SetPowerUpImage(player2Powerup, p2powerup, timerImage2);
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
        // balls = FindObjectsOfType<Ball>();
        // ball = balls.Length>0 ? balls[0] : null;
        if (paddle == null)
        {
            Debug.LogError("Paddle is null in ActivatePowerUp method.");
            return;
        }
        if (powerUpName=="SplitBall" && ball == null)
        {
            Debug.LogError("Ball is null in ActivatePowerUp method.");
            return;
        }

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
            case "SplitBall":
                var splitPowerUp = ball.gameObject.GetComponent<BallSplitPowerUp>();
                if (splitPowerUp == null)
                {
                    Debug.LogError("BallSplitPowerUp script is missing on paddle " + paddle.id);
                    return;
                }
                StartCoroutine(CallSplitBall(ball.gameObject, paddle.id, splitPowerUp));
                // splitPowerUp.SplitBall(ball.gameObject, paddle.id);
                break;

        }
    }
        
    private IEnumerator CallSplitBall(GameObject ballGameObject, float paddleId, BallSplitPowerUp splitPowerUp)
    {
        //wait until end of frame to call split ball
        yield return new WaitForEndOfFrame();
        splitPowerUp.SplitBall(ballGameObject, paddleId);
    }
   
    void DeactivateP1PowerUp()
    {
        p1powerup = "";
        player1Powerup.gameObject.SetActive(false);
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
                childImage.fillAmount = Mathf.InverseLerp(0, 5, p1PowerUpTimer);
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
        p2powerup = "";
        player2Powerup.gameObject.SetActive(false);
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
                childImage.fillAmount = Mathf.InverseLerp(0, 5, p2PowerUpTimer);
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
        p1Timer.SetActive(true);

        p1powerup = powerUpArray[Random.Range(0, powerUpArray.Length)].ToString();
        if(p1powerup == "SplitBall" && ballsInScoreZone == 2 ){
            p1powerup = powerUpArrayWithoutSplitBall[Random.Range(0, powerUpArrayWithoutSplitBall.Length)].ToString();
        }
        p1PowerUpTimer = 5f;  // Resetting the timer

        Transform timerTransform = p1Timer.transform.Find("Timer1");
        Image timerImage = timerTransform.GetComponent<Image>();
        SetPowerUpImage(player1Powerup, p1powerup, timerImage);
    }

    void AssignPowerUpToP2()
    {
        player2Powerup.gameObject.SetActive(true);
        p2Timer.SetActive(true);

        p2powerup = powerUpArray[Random.Range(0, powerUpArray.Length)].ToString();
        p2PowerUpTimer = 5f;  // Resetting the timer
        if(p2powerup == "SplitBall" && ballsInScoreZone == 2 ){
            p2powerup = powerUpArrayWithoutSplitBall[Random.Range(0, powerUpArrayWithoutSplitBall.Length)].ToString();
        }
        Transform timerTransform = p2Timer.transform.Find("Timer2");
        Image timerImage = timerTransform.GetComponent<Image>();
        SetPowerUpImage(player2Powerup, p2powerup, timerImage);
    }
}
