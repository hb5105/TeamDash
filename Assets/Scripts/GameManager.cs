using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviourPunCallbacks
{
    //Scaffolding Level 1
    private Scene currentScene;
    public LevelOneManager levelOneManager;
    // for toggling walls
    private bool isTerrainToggled;
    private bool isTerrainWarningDone;
    public GameObject terrainNotifier;
    //for analytics
    public TrackAnalytics trackAnalytics;
    public ScoreZone leftScoreZone;
    public ScoreZone rightScoreZone;
    public static GameManager instance; // Singleton
    public int scorePlayer1, scorePlayer2;
    public ScoreText scoreTextLeft, scoreTextRight;
    public float powerUpActiveDuration = 5.0f;
    public WallToggle wallToggle;
    public int ballSplittingActiveForPlayer=0;
    private int currentIndex = 0;
    public int[] playerPowerUpSequence = {2, 1, 1, 2, 2, 1}; 
    public List<float> ballSplitTimes = new List<float> {120f, 90f, 60f, 30f};
    private int currentSplitIndex = 0;
    //locations of where the player will spawn when network is connected
    public Transform[] spawnLocs;
    // prefabs for each player
    public GameObject[] playerPrefabs;
    // menu for tutorial scenes
    //public GameObject nextTutorialMenu;
    //public GameObject tutorialTxtPrompt;
    public Ball ballPrefab;
    public GameObject ballGameObject;
    public CountDown countDown;
    public BallText ballText;
    public WordGenerator wordGeneratorPlayer1;
    public WordGenerator wordGeneratorPlayer2;
    private TextMeshProUGUI textBoxPlayer1;
    private TextMeshProUGUI textBoxPlayer2;
    public GameObject gameOverMenu;
    public GameObject levelOverMenu;
    public TextMeshProUGUI gameOverText;
    public string word1;
    public string word2;
    public List<char> remainingChars = new List<char>();
    private int numWords1, numWords2;
    public static bool isGameOver = false;
    public HashSet<char> wordSet1 = new HashSet<char>();
    public HashSet<char> wordSet2 = new HashSet<char>();
    [SerializeField] private float timeInSeconds;
    public float totalTime;
    [SerializeField] private TextMeshProUGUI timerText;
    public int countOfBallsBwScoreZone = 0;
    public GameObject pauseButton;

    public Queue<GameObject> ballsInScoreZone = new Queue<GameObject>();

    private string res1;
    private string res2;
    public float currentTime;
    public float startTimeWord;
    public float endTimeWord;
    private string res1Temp;
    private string res2Temp;
    private List<int> pos1 = new List<int>();
    private List<int> pos2 = new List<int>();

    public GunMovement player1GunMovement; // Assign this to player 1's paddle in the editor
    public GunMovement player2GunMovement; // Assign this to player 2's paddle in the editor

    public TextMeshProUGUI TimerText { get => timerText; }

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    
public void AddBallToQueue(GameObject ball)
{
    ballsInScoreZone.Enqueue(ball);
    Debug.Log("ball added to scorezonequeue");
    ballGameObject = ball;
    StartCoroutine(ProcessBallQueue(ballGameObject));
}

private IEnumerator ProcessBallQueue(GameObject ballGameObject)
{
    // Wait for the end of the frame, ensuring all collisions for this frame are processed
    yield return new WaitForEndOfFrame();
        // Debug.Log("balls in score zone pbq"+ballsInScoreZone.Count);

        // Check if total balls - balls in scorezone is less than or equal to the desired count
        // while ( ballsInScoreZone.Count>1 )
        // {
        //     GameObject ballToProcess = ballsInScoreZone.Dequeue();
        //     // ballComponent = ballToProcess.GetComponent<Ball>();

        //     // Check if the ball has already been destroyed
        //     if (ballToProcess != null)
        //     {

        //         Destroy(ballToProcess);
        //        //wait till next frame
        //         yield return new WaitForEndOfFrame();
        //         // Spawn a new ball

        //     }

        //     // Exit loop if no balls are left in the queue
        //     if (ballsInScoreZone.Count == 0)
        //         break;

        // }
        Invoke("CheckBallsBetweenScoreZones",0.5f);
  
}
     public int CheckBallsBetweenScoreZones()
    { // let's get array of game obejcts of type Ball
        //wait for 2 seconds
        // yield return new WaitForSeconds(2);
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
        countOfBallsBwScoreZone = count;
        //Debug.Log("count of balls bw score zone "+countOfBallsBwScoreZone + "balls in queue"+ ballsInScoreZone.Count);
         if(countOfBallsBwScoreZone == 0)
    {
            if(ballsInScoreZone.Count == 0)
            {
                Debug.Log("no balls in scorezonequeue");
            }
            else
            {
                Debug.Log("spawning new ball");
            }
        GameObject ballToProcess = ballsInScoreZone.Dequeue();
        SpawnNewBall(ballToProcess);
    }
        return count;
    }

    public void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        
        if((currentScene.name == "UpdatedGame")) {
            TimerText.text = "02:30";
        }
        else {
            TimerText.text = "01:00";
        }
        
        wordSet1 = new HashSet<char>();
        wordSet2 = new HashSet<char>();
        gameOverText.text = "";
        isGameOver = false;
        isTerrainToggled = false;
        //SetTime(150);
        // Initialize words for players
        //nextTutorialMenu.SetActive(false);
        // gameOverMenu.SetActive(false);
        levelOverMenu.SetActive(false);
    
        textBoxPlayer1 = wordGeneratorPlayer1.textBox;
        textBoxPlayer2 = wordGeneratorPlayer2.textBox;
        currentTime = timeInSeconds;
        totalTime = timeInSeconds;
        startTimeWord = currentTime;
        word1 = textBoxPlayer1.text;
        word2 = textBoxPlayer2.text;
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        numWords1 = 0;
        numWords2 = 0;
        
        res1 = "";
        res2 = "";
        res1Temp = "";
        res2Temp = "";
        //pos1.Clear();
        //pos2.Clear();
        for (int i = 0; i < word1.Length; i++)
        {
            res1 = res1 + "_";
            res1Temp = res1Temp + word1[i];
        }
        for (int i = 0; i < word2.Length; i++)
        {
            res2 = res2 + "_";
            res2Temp = res2Temp + word2[i];
        }
        foreach (char c in word1)
        {
            wordSet1.Add(c);
        }
        foreach (char c in word2)
        {
            wordSet2.Add(c);
        }
        UpdateScores(res1, res2, res1Temp, res2Temp, pos1, pos2);
        remainingChars = new List<char>(wordSet1);
        remainingChars.AddRange(new List<char>(wordSet2));
        if (PhotonNetwork.IsConnected) 
        {
            Debug.Log("Connected, starting game");
            // set the startpositions of the paddle
            int spawnIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;
            Vector2 spawnPosition = spawnLocs[spawnIndex].position;
            if(spawnIndex >= playerPrefabs.Length)
            {
                Debug.LogError("Not enough prefabs defined for players.");
                return;
            }
            GameObject playerPrefab = playerPrefabs[spawnIndex];
            // spawn player prefab
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.Log("Not connected");
        }
    }
    //this is called when Begin Game button is pressed
    public void BeginGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("StartGame", RpcTarget.All, null);
        }
    }

    [PunRPC]
    public void StartGame()
    {
        
    }

    IEnumerator RandomizePowerUpActivePlayer()
    {
            
            ballSplittingActiveForPlayer = playerPowerUpSequence[currentIndex];
            currentIndex = (currentIndex + 1) % playerPowerUpSequence.Length;

            // Wait for powerUpActiveDuration seconds or until player uses the power-up
            float timer = 0;
            while(timer < powerUpActiveDuration && ballSplittingActiveForPlayer != 0)
            {
                yield return null; // Wait for next frame
                timer += Time.deltaTime;
            }

            // Deactivate the power-up
            ballSplittingActiveForPlayer = 0;
    }
     public void SpawnNewBall(GameObject CurrentBall)
    { // Debug.Log("spawning when no of balls "+ countOfBallsBwScoreZone);
        //extract gameObject from ballPrefab
        CurrentBall = this.ballPrefab.gameObject;
        GameObject newBall = Instantiate(CurrentBall, Vector2.zero, Quaternion.identity); // Spawning the ball at the center for now
        // GameObject newBall = newBallComponent.gameObject;
        Ball newBallComponent = newBall.GetComponent<Ball>();
        BallText ballTextComponent = newBall.transform.GetChild(0).GetComponent<BallText>();
        if (ballTextComponent == null)
        {
            Debug.LogError("No BallText component on the newly spawned ball.");
            return;
        }

        ballTextComponent.ballTextObj = newBall.transform.Find("BallText").gameObject;

        if (ballTextComponent.ballTextObj == null)
        {
            Debug.LogError("No BallText child object in the newly spawned ball.");
            return;
        }

        ballTextComponent.Start(); 
        newBallComponent.ResetBall();  // Set initial position
        newBallComponent.InitialPush();// Initialize the ball text
    }

    private void Update()
    {
        if(!(currentScene.name == "BasicsUpdated"))
        {
            if (!isTerrainWarningDone && (int)currentTime == (int)(totalTime / 2 ) + 4)
            {
                StartCoroutine(StartObstacleWarning());
                isTerrainWarningDone = true;
            }
            if(!isTerrainToggled && (int)currentTime == (int)(totalTime/2))
            {
                wallToggle.SwitchToPointedWalls();
                isTerrainToggled = true;
            }
        }
        
        if (isGameOver == false && !(countDown.isCountDown))
        {
            currentTime -= Time.deltaTime;
            SetTime(currentTime);
            float threshold = 0.05f; // Small threshold value to account for precision errors
            if (currentSplitIndex < ballSplitTimes.Count && Mathf.Abs(currentTime - ballSplitTimes[currentSplitIndex]) < threshold)
            { //Debug.Log("currentTime "+currentTime);
            // Here, initiate the ball split logic. Depending on your implementation, you might call a function or set a flag.
            // StartCoroutine(RandomizePowerUpActivePlayer());


            // Move to the next time checkpoint
            // currentSplitIndex++;
             }
             CheckBallsBetweenScoreZones();
        }
    }

    IEnumerator StartObstacleWarning()
    {
        terrainNotifier.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        terrainNotifier.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        terrainNotifier.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        terrainNotifier.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        terrainNotifier.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        terrainNotifier.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        terrainNotifier.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        terrainNotifier.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        terrainNotifier.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        terrainNotifier.gameObject.SetActive(false);
    }

    void SetTime(float value)
    {
        //print(currentTime);
        TimeSpan time = TimeSpan.FromSeconds(currentTime);                       // set the time value
        TimerText.text = time.ToString("mm':'ss");   // convert time to Time format
        if (currentTime <= 0)
        {
            // Game Over
            endTimeWord = currentTime;
            trackAnalytics.CollectWordData(word1, startTimeWord, endTimeWord, 1, 0);
            trackAnalytics.CollectWordData(word2, startTimeWord, endTimeWord, 1, 0);
            GameEnd();
        }
    }

    public void GameEnd()
    {
        
        Debug.Log("gAME eND CALLED");
        string sceneName = SceneManager.GetActiveScene().name;
        /*if (!(sceneName == "TeamDash") && !(sceneName == "David Scene"))
        {
            tutorialTxtPrompt.SetActive(false);
            nextTutorialMenu.SetActive(true);
        }*/
        //else
        //{
            // gameOverMenu.SetActive(true);
            levelOverMenu.SetActive(true);
        pauseButton.SetActive(false);
            isGameOver = true;
            if(isGameOver == true)
            {
                if(numWords1 > numWords2)
                {
                    // Display Game Over message and winner's name
                    gameOverText.text = "Game Over\nPlayer 1 Wins!";
                }
                else if(numWords2 > numWords1)
                {
                    // Display Game Over message and winner's name
                    gameOverText.text = "Game Over\nPlayer 2 Wins!";
                }
                else
                {
                    gameOverText.text = "Game Over\nIt's a Tie!";
                }
            }
       // }
        
        
    }

    public void UpdateScores(string res1, string res2, string res1Temp, string res2Temp, List<int> pos1, List<int> pos2)
    {
        scoreTextLeft.SetScore(res1, res1Temp, pos1);
        scoreTextRight.SetScore(res2, res2Temp, pos2);
    }

    public bool IsWordCompleted(string playerProgress, string targetWord)
    {
        return playerProgress == targetWord;
    }

    public void UpdateWord(int id)
    {
        if(id == 1)
        {
            // Update word for player 1
            wordGeneratorPlayer1.OnWordCompleted();
            word1 = wordGeneratorPlayer1.textBox.text;

            res1 = "";
            res1Temp = "";
            pos1.Clear();

            for (int i = 0; i < word1.Length; i++)
            {
                res1 = res1 + "_";
                res1Temp = res1Temp + word1[i];

            }

            foreach (char c in word1)
            {
                wordSet1.Add(c);
            }
        }

        else if (id == 2)
        {
            // Update word for player 2
            wordGeneratorPlayer2.OnWordCompleted();
            word2 = wordGeneratorPlayer2.textBox.text;

            res2 = "";
            res2Temp = "";
            pos2.Clear();

            for (int i = 0; i < word2.Length; i++)
            {
                res2 = res2 + "_";
                res2Temp = res2Temp + word2[i];
            }

            foreach (char c in word2)
            {
                wordSet2.Add(c);
            }
        }
    }

    public void OnScoreZoneReached(int id,GameObject ballGameObject)
    {   ballText=ballGameObject.GetComponentInChildren<BallText>();
        res1 = scoreTextLeft.GetScore();
        res2 = scoreTextRight.GetScore();
        //Debug.Log(res1);
        //Debug.Log(res2);
        string curr = ballText.getText();
        char currChar = curr[0];
        
        // Update word based on player
        string playerWord = (id == 1) ? textBoxPlayer1.text : textBoxPlayer2.text;
        int pos = playerWord.IndexOf(currChar);
        if (pos == -1) return; // exit if currChar is not in playerWord


        if (id == 1 && !res1.Contains(curr))
        {
            char[] res1Chars = res1.ToCharArray();
            res1Chars[pos] = currChar;
            res1 = new string(res1Chars);
            scorePlayer1 += 1;

            wordSet1.Remove(currChar); // remove from wordSet
            pos1.Add(pos);
            UpdateScores(res1, res2, res1Temp, res2Temp, pos1, pos2);
            //check if level 1 scene, toggle helpful bubble for player
            if (currentScene.name == "BasicsUpdated")
            {
                StartCoroutine(levelOneManager.ToggleWordBubbles(1));
            }

            if (IsWordCompleted(res1, word1))
            {
                
                numWords1 += 1;
                //check if level 1 scene, toggle helpful bubble for player
                if (currentScene.name == "BasicsUpdated")
                {
                    StartCoroutine(levelOneManager.ToggleWordBubbles(2));
                }
                if(numWords1 < 3)
                {
                    endTimeWord = currentTime;
                    trackAnalytics.CollectWordData(word1, startTimeWord, endTimeWord, 0, 0);
                    startTimeWord = currentTime;
                    UpdateWord(id);
                }
                else
                {
                    if((currentScene.name == "UpdatedGame")) {
                        endTimeWord = currentTime;
                        trackAnalytics.CollectWordData(word1, startTimeWord, endTimeWord, 0, 1);
                        GameEnd();
                    }
                    else {
                        endTimeWord = currentTime;
                        trackAnalytics.CollectWordData(word1, startTimeWord, endTimeWord, 0, 0);
                        startTimeWord = currentTime;
                        UpdateWord(id);
                    }
                }
            }
            Debug.Log(string.Join(", ", pos1));
        }
        else if (id == 2 && !res2.Contains(curr))
        {
            char[] res2Chars = res2.ToCharArray();
            res2Chars[pos] = currChar;
            res2 = new string(res2Chars);
            scorePlayer2 += 1;

            wordSet2.Remove(currChar);
            pos2.Add(pos);
            UpdateScores(res1, res2, res1Temp, res2Temp, pos1, pos2);
            //check if level 1 scene, toggle helpful bubble for player
            if (currentScene.name == "BasicsUpdated")
            {
                StartCoroutine(levelOneManager.ToggleWordBubbles(3));
            }

            if (IsWordCompleted(res2, word2))
            {   
                numWords2 += 1;
                //check if level 1 scene, toggle helpful bubble for player
                if (currentScene.name == "BasicsUpdated")
                {
                    StartCoroutine(levelOneManager.ToggleWordBubbles(4));
                }

                if (numWords2 < 3)
                {
                    endTimeWord = currentTime;
                    trackAnalytics.CollectWordData(word2, startTimeWord, endTimeWord, 0, 0);
                    startTimeWord = currentTime;
                    UpdateWord(id);
                }
                else
                {
                    if((currentScene.name == "UpdatedGame")) {
                        endTimeWord = currentTime;
                        trackAnalytics.CollectWordData(word2, startTimeWord, endTimeWord, 0, 1);
                        GameEnd();
                    }
                    else {
                        endTimeWord = currentTime;
                        trackAnalytics.CollectWordData(word2, startTimeWord, endTimeWord, 0, 0);
                        startTimeWord = currentTime;
                        UpdateWord(id);
                    }
                }
            }
        }

        UpdateScores(res1, res2, res1Temp, res2Temp, pos1, pos2);
        remainingChars = new List<char>(wordSet1);
        remainingChars.AddRange(new List<char>(wordSet2));
        if (id == 1 && scorePlayer1 > 0)
        {
           player1GunMovement.IncreaseBullets(); // Update bullets for player 1
        }
        else if (id == 2 && scorePlayer2 > 0)
        {
           player2GunMovement.IncreaseBullets(); // Update bullets for player 2
        }
    }
}
