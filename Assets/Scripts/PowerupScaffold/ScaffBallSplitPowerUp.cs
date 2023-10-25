using UnityEngine;
using System.Collections;

public class ScaffBallSplitPowerUp : MonoBehaviour
{
    public GameObject ballPrefab;  // Drag your Ball prefab here in the Inspector
    // public int ballSplittingActiveForPlayer = 1;  // 0: no one, 1: player1, 2: player2
    public int ballSplittingActiveForPlayer;
    void Start(){
        ballSplittingActiveForPlayer = Random.Range(1,101)%2==0? 1:2;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Paddle hitPaddle = collision.gameObject.GetComponent<Paddle>();
            Debug.Log("GM ballsplitid "+GameManager.instance.ballSplittingActiveForPlayer);
        if (hitPaddle && GameManager.instance.ballSplittingActiveForPlayer == hitPaddle.id)
        {Debug.Log("powerup split "+hitPaddle.id);
            SplitBall(this.gameObject, hitPaddle.id);
            GameManager.instance.ballSplittingActiveForPlayer = 0;
        }
    }
 void SplitBall(GameObject originalBall, float paddleId)
{
    // 1. Clone the original ball twice
    int activeBallCount = GameObject.FindObjectsOfType<BallSplitPowerUp>().Length;

    // If there are already 2 or more balls, do not split further
    if (activeBallCount >= 2)
    {
        return;
    }
    GameObject ballClone1=null;
    GameObject ballClone2=null;
    if(paddleId == 1){
     ballClone1 = Instantiate(originalBall, originalBall.transform.position + new Vector3(0.5f, 0, 0), originalBall.transform.rotation);
    ballClone2 = Instantiate(originalBall, originalBall.transform.position + new Vector3(0.5f, 0, 0), originalBall.transform.rotation);
    }
    else if(paddleId == 2){
     ballClone1 = Instantiate(originalBall, originalBall.transform.position + new Vector3(-0.5f, 0, 0), originalBall.transform.rotation);
     ballClone2 = Instantiate(originalBall, originalBall.transform.position + new Vector3(-0.5f, 0, 0), originalBall.transform.rotation);
    }

    // Destroy(ballClone1.GetComponent<BallSplitPowerUp>());
    // Destroy(ballClone2.GetComponent<BallSplitPowerUp>());
      if (ballClone1 == null || ballClone2 == null)
    {
        Debug.LogError("Cloning failed.");
        return;
    }
    Rigidbody2D originalRb = originalBall.GetComponent<Rigidbody2D>();
    Rigidbody2D rb1 = ballClone1.GetComponent<Rigidbody2D>();
    Rigidbody2D rb2 = ballClone2.GetComponent<Rigidbody2D>();

        if (paddleId == 1) // If Player 1 hit the ball
        {
            rb1.velocity = new Vector2(Mathf.Abs(rb1.velocity.x), rb1.velocity.y);
            rb2.velocity = new Vector2(Mathf.Abs(rb2.velocity.x), rb1.velocity.y);
        }
        else if (paddleId == 2) // If Player 2 hit the ball
        {
            rb1.velocity = new Vector2(-Mathf.Abs(rb1.velocity.x), rb1.velocity.y);
            rb2.velocity = new Vector2(-Mathf.Abs(rb2.velocity.x), rb2.velocity.y);
        }

    // Ensure references are set for ballClone1
     BallText ballText1 = ballClone1.transform.GetChild(0).GetComponent<BallText>();
    if (ballText1 == null)
    {
        Debug.LogError("No BallText component on ballClone1.");
        return;
    }
    ballText1.ballTextObj = ballClone1.transform.Find("BallText").gameObject;
     if (ballText1.ballTextObj == null)
    {
        Debug.LogError("No BallText child object in ballClone1.");
        return;
    }
    // Initialize the text for ballClone1
    ballText1.Start();

    // Ensure references are set for ballClone2
    BallText ballText2 = ballClone2.transform.GetChild(0).GetComponent<BallText>();
     if (ballText2 == null)
    {
        Debug.LogError("No BallText component on ballClone2.");
        return;
    }

    ballText2.ballTextObj = ballClone2.transform.Find("BallText").gameObject;
     if (ballText2.ballTextObj == null)
    {
        Debug.LogError("No BallText child object in ballClone2.");
        return;
    }

    // Initialize the text for ballClone2
    ballText2.Start();
    if(originalBall == null){
        Debug.Log("Original ball null");
        return;
    }
    // 4. Destroy the original ball
    Destroy(originalBall);
}


}

