using UnityEngine;

public class ScaffPaddleSkewPowerUp : MonoBehaviour
{
    private Paddle paddle;

    public float powerupDuration = 5f;

    private static bool powerUpActiveGlobal = false; // Global flag shared between instances.

    private void Start()
    {
        paddle = GetComponent<Paddle>();

        if (!paddle)
        {
            Debug.LogError("Paddle component missing on " + gameObject.name);
            return;
        }
    }

    private void Update()
    {
        if (!powerUpActiveGlobal)
        {
            if (paddle.id == 1 && Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Activating power-up for Player 1");
                ActivatePowerUp(1);
                Invoke("DeactivatePowerUpPlayer1", powerupDuration);
            }
            else if (paddle.id == 2 && Input.GetKeyDown(KeyCode.Comma))
            {
                Debug.Log("Activating power-up for Player 2");
                ActivatePowerUp(2);
                Invoke("DeactivatePowerUpPlayer2", powerupDuration);
            }
        }
    }

    public void ActivatePowerUp(int playerId)
    {
        powerUpActiveGlobal = true;

        if (playerId == 1)
        {
            Debug.Log("plYW=ER 1 ACTIAVTED");
            transform.rotation = Quaternion.Euler(-45.0f, 0, -45.0f); //same euler angeles as the 
        }
        else if (playerId == 2)
        {
            transform.rotation = Quaternion.Euler(0, 0, 45.0f);
        }
    }

    public void DeactivatePowerUpPlayer1()
    {
        transform.rotation = Quaternion.identity;
        powerUpActiveGlobal = false;
    }

    public void DeactivatePowerUpPlayer2()
    {
        transform.rotation = Quaternion.identity;
        powerUpActiveGlobal = false;
    }
}
