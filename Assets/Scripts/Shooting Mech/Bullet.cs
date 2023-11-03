using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string ballTag = "Ball";  // Assign the Ball tag in the Inspector.
    private BoxCollider2D bulletCollider;

    public GameManager gameManager;
    public TrackAnalytics trackAnalytics;
    void Start()
    {
        bulletCollider = GetComponent<BoxCollider2D>();
        gameManager = GameManager.instance;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with " + collision.gameObject.name);

        if (collision.gameObject.CompareTag(ballTag))
        {
            trackAnalytics.CollectShootData(true);
            gameManager.SpawnNewBall(collision.gameObject);
            Destroy(collision.gameObject); // Destroy the ball
            Destroy(gameObject); // Destroy the bullet     
        }
        else
        {
            trackAnalytics.CollectShootData(false);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScoreZone scoreZone = collision.GetComponent<ScoreZone>();
        if (scoreZone)
        {
            trackAnalytics.CollectShootData(false);
            Destroy(gameObject);
        }
    }
}
