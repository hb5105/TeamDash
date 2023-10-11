using UnityEngine;

public class BallSplitPowerUp : MonoBehaviour
{
    public GameObject ballPrefab;  // Drag your Ball prefab here in the Inspector
    public int ballSplittingActiveForPlayer = 1;  // 0: no one, 1: player1, 2: player2

    void OnCollisionEnter2D(Collision2D collision)
    {
        Paddle hitPaddle = collision.gameObject.GetComponent<Paddle>();

        if (hitPaddle && ballSplittingActiveForPlayer == hitPaddle.id)
        {
            SplitBall(this.gameObject);
        }
    }
    void SplitBall(GameObject originalBall)
    {
        // Assumption: Your ball prefab or original ball has all necessary components attached, including Rigidbody2D, Ball, BallText, etc.

        // 1. Clone the original ball twice
        GameObject ballClone1 = Instantiate(originalBall, originalBall.transform.position + new Vector3(-0.5f, 0, 0), originalBall.transform.rotation);
        GameObject ballClone2 = Instantiate(originalBall, originalBall.transform.position + new Vector3(0.5f, 0, 0), originalBall.transform.rotation);

        // 2. Set up ballClone1's BallText script
        BallText ballText1 = ballClone1.GetComponent<BallText>();
        ballText1.ballTextObj = ballClone1; // Point to the ball itself
        // Initialize the text for the ball
        // ballText1.Start();

        // 3. Set up ballClone2's BallText script
        BallText ballText2 = ballClone2.GetComponent<BallText>();
        ballText2.ballTextObj = ballClone2; // Point to the ball itself
        // Initialize the text for the ball
        // ballText2.Start();

        // 4. Destroy the original ball
        Destroy(originalBall);
    }

}

