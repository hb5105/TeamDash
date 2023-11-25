using System.Collections;
using UnityEngine;

public class PaddleMovePowerUp : MonoBehaviour
{
    private Paddle paddle;  
    private Paddle opponentPaddle;

    public GameObject northWall; // Variable for the north wall
    public GameObject southWall; // Variable for the south wall

    public float shiftDistance = 3.0f;
    public float maxYPosition = 3.5f;
    public float minYPosition = -3.5f;

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
    if (opponentPaddle)
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

        // Set the target position for the paddle
        Vector3 targetPosition = new Vector2(opponentPaddle.transform.position.x, newYPosition);

        // Start the coroutine to move the paddle smoothly to the new position
        StartCoroutine(MovePaddleSmoothly(opponentPaddle.transform, targetPosition));
    }
}

    private IEnumerator MovePaddleSmoothly(Transform paddleTransform, Vector3 targetPosition)
    {
        float duration = 0.8f; // Duration of the movement in seconds
        float elapsedTime = 0f;

        Vector3 startPosition = paddleTransform.position;

        while (elapsedTime < duration)
        {
            paddleTransform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        paddleTransform.position = targetPosition; // Ensure it's exactly at the target in the end
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
