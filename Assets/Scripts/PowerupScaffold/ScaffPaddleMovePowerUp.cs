using UnityEngine;

public class ScaffPaddleMovePowerUp : MonoBehaviour
{
    private Paddle paddle;
    private Paddle opponentPaddle;

    public float shiftDistance = 3.0f;
    public float powerUpDuration = 5f;
    public float maxYPosition = 4.5f;
    public float minYPosition = -4.5f;

    private Vector2 originalOpponentPosition;

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
        if (!powerUpActiveGlobal && opponentPaddle)
        {
            powerUpActiveGlobal = true;

            originalOpponentPosition = opponentPaddle.transform.position;
            float newYPosition = originalOpponentPosition.y + shiftDistance;
            newYPosition = Mathf.Clamp(newYPosition, minYPosition, maxYPosition);
            opponentPaddle.transform.position = new Vector2(originalOpponentPosition.x, newYPosition);

            Invoke("ResetOpponentPosition", powerUpDuration);
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

    private void ResetOpponentPosition()
    {
        if (opponentPaddle)
        {
            opponentPaddle.transform.position = originalOpponentPosition;
            powerUpActiveGlobal = false;
        }
    }
}
