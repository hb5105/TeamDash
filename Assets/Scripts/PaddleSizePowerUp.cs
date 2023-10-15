using UnityEngine;

public class PaddleSizePowerUp : MonoBehaviour
{
    private Paddle paddle;

    private bool powerupActivePlayer1 = false;
    private bool powerupActivePlayer2 = false;

    public float powerupDuration = 5f;
    public Vector2 defaultSize;
    public Vector2 poweredUpSize;

    private static bool powerUpActiveGlobal = false; // Global flag shared between instances.

    private void Start()
    {
        paddle = GetComponent<Paddle>();

        if (!paddle)
        {
            Debug.LogError("Paddle component missing on " + gameObject.name);
            return;
        }

        defaultSize = transform.localScale;
        poweredUpSize = defaultSize * 1.5f;
    }

    private void Update()
    {
        if (!powerUpActiveGlobal) // Check the global flag before activation.
        {
            if (paddle.id == 1 && Input.GetKeyDown(KeyCode.Q) && !powerupActivePlayer1)
            {
                Debug.Log("Activating power-up for Player 1");
                ActivatePowerUp(1);
            }
            else if (paddle.id == 2 && Input.GetKeyDown(KeyCode.Slash) && !powerupActivePlayer2)
            {
                Debug.Log("Activating power-up for Player 2");
                ActivatePowerUp(2);
            }
        }
    }

    public void ActivatePowerUp(int playerId)
    {
        powerUpActiveGlobal = true; // Set the global flag on activation.

        if (playerId == 1)
        {
            powerupActivePlayer1 = true;
            transform.localScale = poweredUpSize;
            Invoke("DeactivatePowerUpPlayer1", powerupDuration);
        }
        else if (playerId == 2)
        {
            powerupActivePlayer2 = true;
            transform.localScale = poweredUpSize;
            Invoke("DeactivatePowerUpPlayer2", powerupDuration);
        }
    }

    public void DeactivatePowerUpPlayer1()
    {
        powerupActivePlayer1 = false;
        transform.localScale = defaultSize;
        powerUpActiveGlobal = false; // Reset the global flag on deactivation.
    }

    public void DeactivatePowerUpPlayer2()
    {
        powerupActivePlayer2 = false;
        transform.localScale = defaultSize;
        powerUpActiveGlobal = false; // Reset the global flag on deactivation.
    }
}
