using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float maxInitialAngle = 0.67f;
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
             
            Debug.Log(GameObject.FindObjectsOfType<Ball>().Length);
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
    }
    }

    public void ResetBall()
    {
        float posY = Random.Range(-maxStartY, maxStartY);
        Vector2 position = new Vector2(startX, posY);
        transform.position = position;

    }
}
