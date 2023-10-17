using UnityEngine;

public class PaddleSizePowerUp : MonoBehaviour
{
    private Paddle paddle;
    public float sizeMultiplier = 1.5f;
    public float powerUpDuration = 5f;
    private Vector3 originalSize;

    private void Start()
    {
        paddle = GetComponent<Paddle>();
        if (!paddle)
        {
            Debug.LogError("Paddle component missing on " + gameObject.name);
        }

        originalSize = paddle.transform.localScale;
    }

    public void ActivatePowerUp()
    {
        // Increase the size of the paddle by the specified multiplier
        paddle.transform.localScale = new Vector3(originalSize.x, originalSize.y * sizeMultiplier, originalSize.z);

        // After the power-up duration, reset the paddle's size
        Invoke("DeactivatePowerUp", powerUpDuration);
    }

    public void DeactivatePowerUp()
    {
        paddle.transform.localScale = originalSize;
    }
}
