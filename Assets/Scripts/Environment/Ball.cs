using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;

[System.Serializable]
public class PlayerData   
{
    public int numMisses;     
    public int playerID;
    public float ballToPaddleDistance;
    public float ball_position;
    public float ball_velocity;
    public float paddlePosition;
    public float paddle_velocity;
}
public class Ball : MonoBehaviour
{
    public TrackAnalytics trackAnalytics;
    public GameObject paddleLeft;
    public GameObject paddleRight;
    public Rigidbody2D rb2d;
    public float maxInitialAngle = 0.3f;
    public float moveSpeed = 1f;
    public float spinStrength = 500f;
    public float maxStartY = 4f;
    public GameManager gameManager=GameManager.instance;
    public BallText ballText;
    private int wallHitCounter = 0;
    public  bool isClonedBall = false;
    private float startX = 0f;
    private float minimumHorizontalVelocity = 1f;  // Adjust as needed
    private float minimumVerticalVelocity = 0.5f;
    private float minimumSpeed = 6f;
    public WallToggle wallToggle;
    private bool isScaled = false;

    private void AdjustVelocity()
    {
        // Adjusting Horizontal Velocity
        if (Mathf.Abs(rb2d.velocity.x) < minimumHorizontalVelocity)
        {
            float newVelocityX = (rb2d.velocity.x >= 0) ? minimumHorizontalVelocity : -minimumHorizontalVelocity;
            rb2d.velocity = new Vector2(newVelocityX, rb2d.velocity.y);
        }
        // Adjusting Vertical Velocity
        if (Mathf.Abs(rb2d.velocity.y) < minimumVerticalVelocity)
        {
            float newVelocityY = (rb2d.velocity.y >= 0) ? minimumVerticalVelocity : -minimumVerticalVelocity;
            rb2d.velocity = new Vector2(rb2d.velocity.x, newVelocityY);
        }
            if (rb2d.velocity.magnitude < minimumSpeed)
            {
                rb2d.velocity = rb2d.velocity.normalized * minimumSpeed;
            }
    }

    private void Start()
    {  if(!isClonedBall){
        InitialPush();
        // Add a 5-second delay before starting the ball's movement.
        //Invoke(nameof(InitialPush), 2f);
        }
        GetComponent<SpriteRenderer>().color = Color.black;
    }

    // Moves the Ball to Random Angle in the Left Direction
    public void InitialPush()
    {
        Vector2 dir;
        if (Random.value < 0.5f)
        {
            dir = Vector2.left;
        }
        else
        {
            dir = Vector2.right;
        }
        dir.y = Random.Range(-maxInitialAngle, maxInitialAngle);
        rb2d.velocity = dir * moveSpeed;
    }
    /* Triggers are something which the Spirit doesnt bounce/act 
     * upon rather used to collect some information or do something
     */
    private void CheckForRecurringMotion(Collision2D collision){
        if (collision.gameObject.CompareTag("Wall"))
        {
            wallHitCounter++;

            if (wallHitCounter >= 3)
            {
                ChangeBallDirection();
                wallHitCounter = 0; // reset the counter
            }
            //    Debug.Log("Wall Hit Counter:"+ wallHitCounter);
        }
        else
        {
            wallHitCounter = 0; // reset the counter if ball hits anything other than the wall
        }
     
    }
     public void ScaleBall()
    {
        Debug.Log("Current scale of Ball: " + transform.localScale);
        Vector3 ballScale = transform.localScale;
        ballScale.x *= 1.5f;
        ballScale.y *= 1.5f;
        transform.localScale = ballScale;
        Debug.Log("New scale of Ball: " + transform.localScale);
    }
    private void Update()
{
   if (!isScaled && wallToggle.isPointedWalls)
    {
        ScaleBall();
        isScaled = true;
    }
    AdjustVelocity();
    //check if there are no balls in the scene and spawn a new ball
    // Debug.Log("No of balls in update"+GameObject.FindObjectsOfType<Ball>().Length);
    if (!GameManager.isGameOver && GameObject.FindObjectsOfType<Ball>().Length == 0)
    {
        gameManager.SpawnNewBall(this.gameObject);
    }
}
    // private void ChangeBallDirection()
    // {
    //     Rigidbody2D rb = GetComponent<Rigidbody2D>();
    //     Debug.Log("entered change ball direction");

    //     // Negate the y direction and add a random component to x direction
    //     float randomX = Random.Range(-1f, 1f);
    //     rb.velocity = new Vector2(rb.velocity.x + randomX, -rb.velocity.y);
    // }
    private void ChangeBallDirection()
{
    // Identify both players. This can be done during Start() and cached if they don't change.

    // Determine which player is closer to the ball.
    float distanceToPlayer1 = Vector2.Distance(transform.position, paddleLeft.transform.position);
    float distanceToPlayer2 = Vector2.Distance(transform.position, paddleRight.transform.position);
    
    Vector2 targetDirection;

    if (distanceToPlayer1 < distanceToPlayer2)
    {
        // Player 1 is closer. Compute direction to Player 1.
        targetDirection = (paddleLeft.transform.position - transform.position).normalized;
    }
    else
    {
        // Player 2 is closer. Compute direction to Player 2.
        targetDirection = (paddleRight.transform.position - transform.position).normalized;
    }

    // Set the velocity of the ball towards the closer player.
    // Note: 'ballSpeed' is the desired speed of the ball. You can adjust as needed. // or whatever speed you prefer
    GetComponent<Rigidbody2D>().velocity = targetDirection * moveSpeed;
}
    public void OnTriggerEnter2D(Collider2D collision)
    {
        /* We create a object of ScoreZone and check if collision happened with
            that scorezone 
         */
        ScoreZone scoreZone = collision.GetComponent<ScoreZone>();
        if (scoreZone)
        {
            // used to notify TrackAnalytics script to collect data for player score
            
            // Send the GameManager the ScoreZone Id of the Game to add score to the player
            gameManager.OnScoreZoneReached(scoreZone.id,this.gameObject);
            //Debug.Log(GameObject.FindObjectsOfType<Ball>().Length);

            //ANALYTICS: Tracking player misses and speed of gameobjects
            float yPosition = transform.position.y;
            Rigidbody2D rbPaddleLeft = paddleLeft.GetComponent<Rigidbody2D>();
            Rigidbody2D rbPaddleRight = paddleRight.GetComponent<Rigidbody2D>();

            PlayerData playerData = new PlayerData();
            playerData.numMisses = 1;
            playerData.ball_position = yPosition;
            playerData.ball_velocity = rb2d.velocity.magnitude;
            if(scoreZone.id == 1) // if id is 1, player 1 scored ball
            {
                trackAnalytics.CollectPUScoreData(1);
                // set playerID to 2 since player 2 missed
                playerData.playerID = 2;
                playerData.paddlePosition = paddleLeft.transform.localPosition.y;
                playerData.ballToPaddleDistance = Mathf.Abs(playerData.paddlePosition - playerData.ball_position);
                playerData.paddle_velocity = rbPaddleLeft.velocity.magnitude;
            }
            else if(scoreZone.id == 2) // if id is 2, player 2 scored ball
            {
                trackAnalytics.CollectPUScoreData(2);
                // set playerID to 1 since player 1 missed
                playerData.playerID = 1;
                playerData.paddlePosition = paddleRight.transform.localPosition.y;
                playerData.ballToPaddleDistance = Mathf.Abs(playerData.paddlePosition - playerData.ball_position);
                playerData.paddle_velocity = rbPaddleRight.velocity.magnitude;
            }

               
            string json = JsonUtility.ToJson(playerData);
            RestClient.Post("https://csci526-bee47-default-rtdb.firebaseio.com/.json", playerData);
            //   Debug.Log("No of balls "+GameObject.FindObjectsOfType<Ball>().Length);       
            // if (!GameManager.isGameOver && GameObject.FindObjectsOfType<Ball>().Length==1)
            // {
            //     // ResetBall();
            //     // InitialPush();
            //     gameManager.SpawnNewBall(this.gameObject);
            // }
             gameManager.AddBallToQueue(this.gameObject);
            // Destroy(this.gameObject);
        }
    }

// This method checks if there are no other balls left in the scene

    public void OnCollisionEnter2D(Collision2D collision)
    {  
    // Check if the ball collided with a paddle
    //   Debug.Log("Collided with: " + collision.gameObject.name);
        CheckForRecurringMotion(collision);
    Paddle paddle = collision.gameObject.GetComponent<Paddle>();
    if (paddle)
    { 
        // Debug.Log("entered oncollision");
        // Check if the paddle is tilted (rotation is not zero)
        if (paddle.transform.rotation.z != 0)
        {   
            // Debug.Log("entered paddle flick");
            // Increase the ball's speed
            // Debug.Log("Ball Velocity before collision: " + rb2d.velocity);
            float speedMultiplier = 3f;  // Adjust as needed
            float angleAdjustment = 2f;
            float tiltDirection=(paddle.transform.rotation.z>0)? 1:-1;
            rb2d.velocity = rb2d.velocity.normalized*moveSpeed*speedMultiplier;
            float newYVelocity;
            if(paddle.id==2){
            newYVelocity = rb2d.velocity.y + (angleAdjustment * tiltDirection);
            }
            else{
                newYVelocity = rb2d.velocity.y + (angleAdjustment * -1 * tiltDirection);
            }
            rb2d.velocity = new Vector2(rb2d.velocity.x, newYVelocity);
            // Debug.Log("Ball Velocity after collision: " + rb2d.velocity);
            AdjustVelocity();
        }
        else{
            rb2d.velocity=rb2d.velocity.normalized*moveSpeed;
            AdjustVelocity();
        }

            if (paddle.id == 1)
            {   
                trackAnalytics.CollectPUNoScoreData(1);
                // Debug.Log("entered paddle 1");
                List<char> wordList1 = new List<char>(gameManager.wordSet1);
                // Check if the list has any elements to prevent possible ArgumentOutOfRangeException
                if (wordList1.Count > 0)
                {
                    int idx = Random.Range(0, wordList1.Count);
                    char nextChar = wordList1[idx];
                    ballText.setText(nextChar.ToString());
                }
                // Debug.Log("powerup active"+FindObjectOfType<PowerUpManager>().p1PowerUpActive);
                // Debug.Log("powerup name"+FindObjectOfType<PowerUpManager>().p1powerup);
                // if(FindObjectOfType<PowerUpManager>().p1PowerUpActive && FindObjectOfType<PowerUpManager>().p1powerup == "SplitBall"){
                //         var splitPowerUp = this.gameObject.GetComponent<BallSplitPowerUp>();
                //         if(!splitPowerUp){
                //             Debug.Log("split powerup null");
                //         }
                //         if(splitPowerUp){
                //             splitPowerUp.SplitBall(this.gameObject, paddle.id);
                //         }
                // }
            }

            if (paddle.id == 2)
            {
                trackAnalytics.CollectPUNoScoreData(2);
                List<char> wordList2 = new List<char>(gameManager.wordSet2);
                // Check if the list has any elements to prevent possible ArgumentOutOfRangeException
                if (wordList2.Count > 0)
                {
                    int idx = Random.Range(0, wordList2.Count);
                    char nextChar = wordList2[idx];
                    ballText.setText(nextChar.ToString());
                }
                // Debug.Log("powerup active 2"+FindObjectOfType<PowerUpManager>().p2PowerUpActive);
                // Debug.Log("powerup name 2"+FindObjectOfType<PowerUpManager>().p2powerup);
                // if(FindObjectOfType<PowerUpManager>().p2PowerUpActive && FindObjectOfType<PowerUpManager>().p2powerup == "SplitBall"){
                //     Debug.Log("split ball indide if called");
                //         var splitPowerUp = this.gameObject.GetComponent<BallSplitPowerUp>();
                //         if(!splitPowerUp){
                //             Debug.Log("split powerup null");
                //         }
                //         if(splitPowerUp){
                //             splitPowerUp.SplitBall(this.gameObject, paddle.id);
                //         }
                // }
            }
            AdjustVelocity();
        }
            if (collision.gameObject.CompareTag("NorthWall"))
            {
                Vector2 currentVelocity = rb2d.velocity;
                rb2d.velocity = new Vector2(currentVelocity.x, -Mathf.Abs(currentVelocity.y));
            }
            // Check for the SouthWall tag
            else if (collision.gameObject.CompareTag("SouthWall"))
            {
                Vector2 currentVelocity = rb2d.velocity;
                rb2d.velocity = new Vector2(currentVelocity.x, Mathf.Abs(currentVelocity.y));
            }
    }

    public void ResetBall()
    {
        float posY = Random.Range(-maxStartY, maxStartY);
        Vector2 position = new Vector2(startX, posY);
        transform.position = position;

    }
}