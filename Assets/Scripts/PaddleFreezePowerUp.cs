using UnityEngine;

public class PaddleFreezePowerUp : MonoBehaviour
{
    private Paddle paddle;
    private Paddle opponentPaddle;
    private float originalOpponentSpeed;

    public float freezeDuration = 5f;

    private void Start()
    {
        paddle = GetComponent<Paddle>();
        if (!paddle)
        {
            Debug.LogError("Paddle component missing on " + gameObject.name);
        }
    }

    private void Update()
    {
        if (paddle.id == 1 && Input.GetKeyDown(KeyCode.E))
        {
            FreezeOpponent();
        }
        else if (paddle.id == 2 && Input.GetKeyDown(KeyCode.Period))
        {
            FreezeOpponent();
        }
    }

    private void FreezeOpponent()
    {
        if (opponentPaddle == null)
        {
            // Find the opponent paddle.
            Paddle[] paddles = FindObjectsOfType<Paddle>();
            foreach (Paddle p in paddles)
            {
                if (p != paddle)
                {
                    opponentPaddle = p;
                    break;
                }
            }

            if (opponentPaddle)
            {
                originalOpponentSpeed = opponentPaddle.moveSpeed;
            }
        }

        if (opponentPaddle)
        {
            opponentPaddle.moveSpeed = 0f;  // Set move speed to zero to "freeze" the paddle.
            Invoke("UnfreezeOpponent", freezeDuration);
        }
    }

    private void UnfreezeOpponent()
    {
        if (opponentPaddle)
        {
            opponentPaddle.moveSpeed = originalOpponentSpeed; // Restore original move speed.
        }
    }
}
