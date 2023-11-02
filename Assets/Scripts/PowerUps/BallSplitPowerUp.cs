using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        ballSplittingActiveForPlayer = Random.Range(1,101)%2==0? 1:2;
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


//Debug.Log("Upper limit: " + upperLimit);
//Debug.Log("Lower limit: " + lowerLimit);
    }
    
 public void SplitBall(GameObject originalBall, float paddleId)
{   //check if isClonedBall flag is true
    if(originalBall.GetComponent<Ball>().isClonedBall){
    Debug.Log("isClonedBall flag is true for this split ball");
    }
    // Debug.Log("split ball method called");
    getUpperAndLowerLimit();
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
         Debug.Log("initial rb1.position.y "+rb1.position.y);
        separation = rb1.position.y+separation<= upperLimit? separation: 0;
         rb1.position = new Vector2(rb1.position.x, rb1.position.y + separation);
         Debug.Log("later rb1.position.y and separation"+rb1.position.y + " "+separation);
        Debug.Log("initial rb2.position.y "+rb2.position.y);
        separation = rb2.position.y-separation>= lowerLimit? separation: 0;
            rb2.position = new Vector2(rb2.position.x, rb2.position.y - separation);
            Debug.Log("later rb2.position.y and separation"+rb2.position.y + " "+separation);
        // Debug.Log("rb1.position.y "+rb1.position.y);
        // Debug.Log("rb2.position.y "+rb2.position.y);
        // if (paddleId == 1) // If Player 1 hit the ball
        // {
        //     rb1.velocity = originalRb.velocity;
        //     rb2.velocity = new Vector2(Mathf.Abs(rb2.velocity.x), rb1.velocity.y);
        // }
        // else if (paddleId == 2) // If Player 2 hit the ball
        // {
        //     rb1.velocity = new Vector2(Mathf.Abs(rb1.velocity.x), rb1.velocity.y);
        //     rb2.velocity = new Vector2(Mathf.Abs(rb2.velocity.x), rb2.velocity.y);
        // }
       
        // give some gap in the y coordinates of both
        // rb1.position = new Vector2(rb1.position.x, rb1.position.y );
        // rb2.position = new Vector2(rb2.position.x, rb2.position.y );
    // Ensure references are set for ballClone1
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

