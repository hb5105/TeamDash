using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.Analytics;
using Proyecto26;
using UnityEditor.Experimental.RestService;

[System.Serializable]
public class PlayerData
    {
        public int playerID;
        public float ball_position;
        public float ball_velocity;
        public float paddlePosition;
        public float paddle_velocity;
    }
public class Ball : MonoBehaviour
{
    public GameObject paddleLeft;
    public GameObject paddleRight;
    public Rigidbody2D rb2d;
    public float maxInitialAngle = 0.3f;
    public float moveSpeed = 1f;
    public float maxStartY = 4f;
    public GameManager gameManager;

    private float startX = 0f;
    private float minimumHorizontalVelocity = 0.5f;  // Adjust as needed
    private float minimumVerticalVelocity = 0.5f;  
    
    private void AdjustVelocity()
    {
        if (Mathf.Abs(rb2d.velocity.x) < minimumHorizontalVelocity)
        {
            float newVelocityX = (rb2d.velocity.x >= 0) ? minimumHorizontalVelocity : -minimumHorizontalVelocity;
            rb2d.velocity = new Vector2(newVelocityX, rb2d.velocity.y);
        }
         if (Mathf.Abs(rb2d.velocity.y) < minimumVerticalVelocity)
        {
            float newVelocityY = (rb2d.velocity.y >= 0) ? minimumVerticalVelocity : -minimumVerticalVelocity;
            rb2d.velocity = new Vector2(rb2d.velocity.x, newVelocityY);
        }
    }

    private void Start()
    {
        InitialPush();
        //Paddle child objects, paddleLeft and paddleRight
        //paddleLeft = paddleParent.transform.Find("PaddleLeft").gameObject;
        //paddleRight = paddleParent.transform.Find("PaddleRight").gameObject;

        // Add a 5-second delay before starting the ball's movement.
        Invoke(nameof(InitialPush), 2f);
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* We create a object of ScoreZone and check if collision happened with
            that scorezone 
         */
        ScoreZone scoreZone = collision.GetComponent<ScoreZone>();
        if (scoreZone)
        {   
            // Send the GameManager the ScoreZone Id of the Game to add score to the player
            gameManager.OnScoreZoneReached(scoreZone.id,this.gameObject);
            //Debug.Log(GameObject.FindObjectsOfType<Ball>().Length);
            //Analytics of Game
            float yPosition = transform.position.y;
            Rigidbody2D rbPaddleLeft = paddleLeft.GetComponent<Rigidbody2D>();
            Rigidbody2D rbPaddleRight = paddleRight.GetComponent<Rigidbody2D>();
            if (scoreZone.id == 1)
            {
                PlayerData playerData = new PlayerData();
                playerData.playerID = 1;
                playerData.ball_position = yPosition;
                playerData.ball_velocity = rb2d.velocity.magnitude;
                playerData.paddlePosition = paddleLeft.transform.localPosition.y;
                Debug.Log("paddle left position: " + paddleLeft.transform.localPosition.y);
                playerData.paddle_velocity = rbPaddleLeft.velocity.magnitude;
                string json = JsonUtility.ToJson(playerData);
                RestClient.Post("https://csci526-bee47-default-rtdb.firebaseio.com/.json", playerData);
                
                /*Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    {"ball_position", yPosition},
                    {"paddlePosition", paddleLeft.transform.position.y},
                    {"ball_velocity", rb2d.velocity.magnitude},
                    {"paddle_velocity", rbPaddleLeft.velocity.magnitude}
                };
                AnalyticsService.Instance.CustomData("player1Miss", parameters);
                AnalyticsService.Instance.Flush();
                Debug.Log("Unity analytics for player 1 miss triggered!");*/
                /*Debug.Log("y-position: " + yPosition);
                Debug.Log("paddle position: " + paddleLeft.transform.position.y);
                Debug.Log("ball velocity: " + rb2d.velocity);
                Debug.Log("paddle velocity: " + rbPaddleLeft.velocity);*/
            }
            else if (scoreZone.id == 2)
            {
                PlayerData playerData = new PlayerData();
                playerData.playerID = 2;
                playerData.ball_position = yPosition;
                playerData.ball_velocity = rb2d.velocity.magnitude;
                playerData.paddlePosition = paddleRight.transform.localPosition.y;
                Debug.Log("paddle right position: " + paddleRight.transform.localPosition.y);
                playerData.paddle_velocity = rbPaddleRight.velocity.magnitude;
                string json = JsonUtility.ToJson(playerData);
                RestClient.Post("https://csci526-bee47-default-rtdb.firebaseio.com/.json", playerData);
                /*Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    {"ball_position", yPosition},
                    {"paddlePosition", paddleRight.transform.position.y},
                    {"ball_velocity", rb2d.velocity.magnitude},
                    {"paddle_velocity", rbPaddleRight.velocity.magnitude }
                };
                AnalyticsService.Instance.CustomData("player2Miss", parameters);
                AnalyticsService.Instance.Flush();
                Debug.Log("Unity analytics for player 2 miss triggered!");*/
                /*Debug.Log("y-position: " + yPosition);
                Debug.Log("paddle position: " + paddleRight.transform.position.y);
                Debug.Log("ball velocity: " + rb2d.velocity);
                Debug.Log("paddle velocity: " + rbPaddleRight.velocity);*/
            }
             
            
            if (!GameManager.isGameOver && GameObject.FindObjectsOfType<Ball>().Length==1)
            {
                // ResetBall();
                // InitialPush();
                gameManager.SpawnNewBall(this.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

// This method checks if there are no other balls left in the scene

    private void OnCollisionEnter2D(Collision2D collision)
    {  
    // Check if the ball collided with a paddle
    Paddle paddle = collision.gameObject.GetComponent<Paddle>();
    if (paddle)
    { 
        // Debug.Log("entered oncollision");
        // Check if the paddle is tilted (rotation is not zero)
        if (paddle.transform.rotation.z != 0)
        {   Debug.Log("entered paddle flick");
            // Increase the ball's speed
            Debug.Log("Ball Velocity before collision: " + rb2d.velocity);
            float speedMultiplier = 3f;  // Adjust as needed
            rb2d.velocity = rb2d.velocity.normalized*moveSpeed*speedMultiplier;
            Debug.Log("Ball Velocity after collision: " + rb2d.velocity);
            AdjustVelocity();
        }
        else{
            rb2d.velocity=rb2d.velocity.normalized*moveSpeed;
            AdjustVelocity();
        }

         if(paddle.id ==1){
                this.GetComponent<SpriteRenderer>().color = Color.red;
           }
           if(paddle.id==2){
                this.GetComponent<SpriteRenderer>().color = Color.blue;
            }

    }
    }

    public void ResetBall()
    {
        float posY = Random.Range(-maxStartY, maxStartY);
        Vector2 position = new Vector2(startX, posY);
        transform.position = position;

    }
}
