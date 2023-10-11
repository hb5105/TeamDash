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
    // 1. Clone the original ball twice
    GameObject ballClone1 = Instantiate(originalBall, originalBall.transform.position + new Vector3(0.5f, 0, 0), originalBall.transform.rotation);
    GameObject ballClone2 = Instantiate(originalBall, originalBall.transform.position + new Vector3(0.5f, 0, 0), originalBall.transform.rotation);
    Destroy(ballClone1.GetComponent<BallSplitPowerUp>());
    Destroy(ballClone2.GetComponent<BallSplitPowerUp>());
      if (ballClone1 == null || ballClone2 == null)
    {
        Debug.LogError("Cloning failed.");
        return;
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

