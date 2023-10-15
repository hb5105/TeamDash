using UnityEngine;

public class PaddleMovePowerUp : MonoBehaviour
{
    private Paddle paddle;
    private Paddle opponentPaddle;

    public float shiftDistance = 3.0f; // distance to shift the paddle's position
    public float powerUpDuration = 5f;  // how long the effect lasts
    public float maxYPosition = 4.5f;   // max y position the paddle can go to
    public float minYPosition = -4.5f;  // min y position the paddle can go to

    private Vector2 originalOpponentPosition;

    private static bool powerUpActiveGlobal = false;  // Global flag to prevent simultaneous activation.

    private void Start()
    {
        paddle = GetComponent<Paddle>();
        if (!paddle)
        {
            Debug.LogError("Paddle component missing on " + gameObject.name);
        }

        FindOpponentPaddle();
    }

    private void Update()
    {
        if (!powerUpActiveGlobal)
        {
            if (paddle.id == 1 && Input.GetKeyDown(KeyCode.R))
            {
                ShiftOpponentPosition();
                Invoke("ResetOpponentPosition", powerUpDuration);
            }
            else if (paddle.id == 2 && Input.GetKeyDown(KeyCode.Comma))
            {
                ShiftOpponentPosition();
                Invoke("ResetOpponentPosition", powerUpDuration);
            }
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

    private void ShiftOpponentPosition()
    {
        if (opponentPaddle)
        {
            powerUpActiveGlobal = true;

            originalOpponentPosition = opponentPaddle.transform.position;
            float newYPosition = originalOpponentPosition.y + shiftDistance;

            // Clamping the position to ensure it doesn't go outside the playable area.
            newYPosition = Mathf.Clamp(newYPosition, minYPosition, maxYPosition);

            opponentPaddle.transform.position = new Vector2(originalOpponentPosition.x, newYPosition);
        }
    }

    private void ResetOpponentPosition()
    {
        if (opponentPaddle)
        {
            opponentPaddle.transform.position = originalOpponentPosition;
            powerUpActiveGlobal = false;
        }
    }
}
