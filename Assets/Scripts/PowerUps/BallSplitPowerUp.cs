using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BallSplitPowerUp : MonoBehaviour
{
    public GameObject ballPrefab;  // Drag your Ball prefab here in the Inspector
    // public int ballSplittingActiveForPlayer = 1;  // 0: no one, 1: player1, 2: player2
    public int ballSplittingActiveForPlayer;
    public float upperLimit;
    public float lowerLimit;
    public ScoreZone leftScoreZone;
    public ScoreZone rightScoreZone;
    void Start(){
        ballSplittingActiveForPlayer = 0==0? 1:2;
    }
    public int CheckBallsBetweenScoreZones()
    { // let's get array of game obejcts of type Ball
        //Debug.Log("CheckBallsBetweenScoreZones called");
        GameObject[] activeBalls = GameObject.FindGameObjectsWithTag("Ball");
        float leftX = leftScoreZone.transform.position.x;
        float rightX = rightScoreZone.transform.position.x;
        int count = 0;
        foreach (GameObject ball in activeBalls)
        {
            if (ball.transform.position.x > leftX && ball.transform.position.x < rightX)
            {
                count++;
            }
        }
        return count;
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
    public  void getUpperAndLowerLimit(){
        Transform wallsParent = GameObject.Find("Walls").transform;
        List<GameObject> upperWalls = new List<GameObject>();
        List<GameObject> lowerWalls = new List<GameObject>();

        // Loop through children of the "Walls" parent to find upper and lower walls
        foreach (Transform child in wallsParent)
        {
            if (child.name.StartsWith("NorthWall"))
            {
                upperWalls.Add(child.gameObject);
            }
            else if (child.name.StartsWith("SouthWall"))
            {
                lowerWalls.Add(child.gameObject);
            }
        }
        upperLimit = float.MaxValue;
       lowerLimit = float.MinValue;

// Find the minimum of the upper boundaries
foreach (GameObject wall in upperWalls)
{
    float wallUpperBoundary = wall.transform.position.y;
    if (wallUpperBoundary < upperLimit)
    {
        upperLimit = wallUpperBoundary;
    }
}

// Find the maximum of the lower boundaries
foreach (GameObject wall in lowerWalls)
{
    float wallLowerBoundary = wall.transform.position.y;
    if (wallLowerBoundary > lowerLimit)
    {
        lowerLimit = wallLowerBoundary;
    }
}


        Debug.Log("Upper limit: " + upperLimit);
        Debug.Log("Lower limit: " + lowerLimit);
    }
    
 public void SplitBall(GameObject originalBall, float paddleId)
{   //check if isClonedBall flag is true
    if(originalBall.GetComponent<Ball>().isClonedBall){
    Debug.Log("isClonedBall flag is true for this split ball");
    }
    // Debug.Log("split ball method called");
    getUpperAndLowerLimit();
        Debug.Log("Upper limit: " + upperLimit);
        Debug.Log("Lower limit: " + lowerLimit);
        upperLimit = upperLimit - 0.5f;
        lowerLimit = lowerLimit + 0.5f;
        float separation = 2f;

    int count = CheckBallsBetweenScoreZones();
    //Debug.Log("CheckBallsBetweenScoreZones "+count);
    if (count>=2)
    { if(originalBall.GetComponent<Ball>().isClonedBall){
        Debug.Log("split ball returning isClonedBall flag is true for this split ball");}
        Debug.Log("split ball returning");
        return;
    }
    if(originalBall == null){

        Debug.Log("Original ball null");
        return;
    }
    //Debug.Log("Yayy Sumiii");
    GameObject ballClone1=null;
    GameObject ballClone2=null;
    
    ballClone1 = Instantiate(originalBall, originalBall.transform.position, originalBall.transform.rotation);
    ballClone2 = Instantiate(originalBall, originalBall.transform.position, originalBall.transform.rotation);
    
    //assign the isClonedBall flag to true
    ballClone1.GetComponent<Ball>().isClonedBall = true;
    ballClone2.GetComponent<Ball>().isClonedBall = true;

      if (ballClone1 == null || ballClone2 == null)
    {
        Debug.LogError("Cloning failed.");
        return;
    }
    Rigidbody2D originalRb = originalBall.GetComponent<Rigidbody2D>();
    Rigidbody2D rb1 = ballClone1.GetComponent<Rigidbody2D>();
    Rigidbody2D rb2 = ballClone2.GetComponent<Rigidbody2D>();
    //get text on original ball
    string originalBallText = originalBall.transform.GetChild(0).GetComponent<BallText>().getText();
    Debug.Log("originalBallText "+originalBallText);
     rb1.velocity = originalRb.velocity;
        rb2.velocity = originalRb.velocity;
        // // Ensure they don't exceed the boundary after split
        // For ballClone1: Adjust position.y and then assign a new Vector2 to rb1.position
        float newY1 = Math.Min(rb1.position.y + separation, upperLimit);
        Debug.Log("New Y1" + newY1);
        rb1.position = new Vector2(rb1.position.x, newY1);
        Debug.Log("Ball position Y: " + rb1.position.y);

        // For ballClone2: Adjust position.y and then assign a new Vector2 to rb2.position
        float newY2 = Math.Max( rb2.position.y - separation, lowerLimit);
        Debug.Log("New Y2" + newY1);
        rb2.position = new Vector2(rb2.position.x, newY2);
        Debug.Log("Ball2 position Y: " + rb2.position.y);

        BallText ballText1 = ballClone1.transform.GetChild(0).GetComponent<BallText>();
    if (ballText1 == null)
    {
        Debug.LogError("No BallText component on ballClone1.");
        return;
    }
    ballText1.ballTextObj = ballClone1.transform.Find("BallText").gameObject;
    ballText1.isClonedBall = true;
     if (ballText1.ballTextObj == null)
    {
        Debug.LogError("No BallText child object in ballClone1.");
        return;
    }
    // Initialize the text for ballClone1
    

    // Ensure references are set for ballClone2
    BallText ballText2 = ballClone2.transform.GetChild(0).GetComponent<BallText>();
     if (ballText2 == null)
    {
        Debug.LogError("No BallText component on ballClone2.");
        return;
    }

    ballText2.ballTextObj = ballClone2.transform.Find("BallText").gameObject;
    ballText2.isClonedBall = true;
     if (ballText2.ballTextObj == null)
    {
        Debug.LogError("No BallText child object in ballClone2.");
        return;
    }

    // Initialize the text for ballClone2
    
    if(originalBall == null){
        Debug.Log("Original ball null");
        return;
    }
    Debug.Log("originalBallText end "+originalBallText);
    //set text on ballClone1 and ballClone2
    ballText1.InitializeText(originalBallText);
    ballText2.InitializeText(originalBallText);
    // 4. Destroy the original ball
    Destroy(originalBall);
}


}

