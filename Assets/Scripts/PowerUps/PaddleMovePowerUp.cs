using UnityEngine;

public class PaddleMovePowerUp : MonoBehaviour
{
    private Paddle paddle;  
    private Paddle opponentPaddle;

    public GameObject northWall; // Variable for the north wall
    public GameObject southWall; // Variable for the south wall

    public float shiftDistance = 3.0f;
    public float maxYPosition = 4.5f;
    public float minYPosition = -4.5f;

    private static bool powerUpActiveGlobal = false;

    private void Start()
    {
        paddle = GetComponent<Paddle>();
        if (!paddle)
        {
            Debug.LogError("Paddle component missing on " + gameObject.name);
        }

        FindOpponentPaddle();
    }
public void ShiftOpponentPosition()
{
    if ( opponentPaddle)
    {
        powerUpActiveGlobal = true;

        // Get the Y-coordinate of the opponent's paddle
        float opponentY = opponentPaddle.transform.position.y;

        // Determine if the opponent is above or below the center (0,0)
        bool isAboveCenter = opponentY > 0;

        // Calculate new Y position based on the opponent's position relative to the center
        float newYPosition;
        if (isAboveCenter)
        {
            // If the opponent is above the center, move towards the south wall
            newYPosition = Mathf.Max(opponentY - shiftDistance, minYPosition);
        }
        else
        {
            // If the opponent is below the center, move towards the north wall
            newYPosition = Mathf.Min(opponentY + shiftDistance, maxYPosition);
        }

        // Move the paddle
        opponentPaddle.transform.position = new Vector2(opponentPaddle.transform.position.x, newYPosition);
    }
}

    private void FindOpponentPaddle()
    {
        Paddle[] paddles = FindObjectsOfType<Paddle>();
        foreach (Paddle p in paddles)
        {
            if (p != paddle)
            {
                opponentPaddle = p;
                break;
            }
        }
    }

    // The ResetOpponentPosition method is no longer needed and has been removed
}
