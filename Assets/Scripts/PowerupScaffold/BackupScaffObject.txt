using UnityEngine;

public class ScaffObjectScript : MonoBehaviour
{
    public GameObject p1PowerUpIndicator; // Drag the GameObject for Paddle1's PowerUp Indicator here in the inspector
    public GameObject p2PowerUpIndicator; // Drag the GameObject for Paddle2's PowerUp Indicator here in the inspector

    private ScaffPowerUpManager powerUpManager;

    private void Start()
    {
        powerUpManager = GetComponent<ScaffPowerUpManager>();
        if (powerUpManager == null)
        {
            Debug.LogError("ScaffPowerUpManager not found on this GameObject!");
            return;
        }

        HidePowerUpIndicators(); // Hides the indicators at the start
    }

    private void Update()
    {
        // Check the state of the powerups and update the indicators
        if (!string.IsNullOrEmpty(powerUpManager.p1powerup) && !powerUpManager.p1PowerUpActive)
        {
            ShowP1PowerUpIndicator();
        }
        else
        {
            HideP1PowerUpIndicator();
        }

        if (!string.IsNullOrEmpty(powerUpManager.p2powerup) && !powerUpManager.p2PowerUpActive)
        {
            ShowP2PowerUpIndicator();
        }
        else
        {
            HideP2PowerUpIndicator();
        }
    }

    public void ShowP1PowerUpIndicator()
    {
        p1PowerUpIndicator.SetActive(true);
    }

    public void HideP1PowerUpIndicator()
    {
        p1PowerUpIndicator.SetActive(false);
    }

    public void ShowP2PowerUpIndicator()
    {
        p2PowerUpIndicator.SetActive(true);
    }

    public void HideP2PowerUpIndicator()
    {
        p2PowerUpIndicator.SetActive(false);
    }

    public void HidePowerUpIndicators()
    {
        p1PowerUpIndicator.SetActive(false);
        p2PowerUpIndicator.SetActive(false);
    }
}
