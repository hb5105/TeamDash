using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
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

    public Ball ballPrefab;
    public CountDown countDown;
    public BallText ballText;
    public WordGenerator wordGeneratorPlayer1;
    public WordGenerator wordGeneratorPlayer2;
    private TextMeshProUGUI textBoxPlayer1;
    private TextMeshProUGUI textBoxPlayer2;
    public GameObject gameOverMenu;
    public TextMeshProUGUI gameOverText;
    public string word1;
    public string word2;
    public List<char> remainingChars = new List<char>();
    private int numWords1, numWords2;
    public static bool isGameOver = false;
    public HashSet<char> wordSet1 = new HashSet<char>();
    public HashSet<char> wordSet2 = new HashSet<char>();
    [SerializeField] private float timeInSeconds;
    [SerializeField] private TextMeshProUGUI timerText;


    private string res1;
    private string res2;
    private float currentTime;
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
    
    
    public void Start()
    {
        TimerText.text = "02:30";
        wordSet1 = new HashSet<char>();
        wordSet2 = new HashSet<char>();
        isGameOver = false;
        //SetTime(150);
        // Initialize words for players
        gameOverMenu.SetActive(false);
        textBoxPlayer1 = wordGeneratorPlayer1.textBox;
        textBoxPlayer2 = wordGeneratorPlayer2.textBox;
        currentTime = timeInSeconds;
        word1 = textBoxPlayer1.text;
        word2 = textBoxPlayer2.text;
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        numWords1 = 0;
        numWords2 = 0;
        
        res1 = "";
        res2 = "";
        for (int i = 0; i < word1.Length; i++)
        {
            res1 = res1 + "_";
        }
        for (int i = 0; i < word2.Length; i++)
        {
            res2 = res2 + "_";
        }
        foreach (char c in word1)
        {
            wordSet1.Add(c);
        }
        foreach (char c in word2)
        {
            wordSet2.Add(c);
        }
        UpdateScores(res1, res2);
        remainingChars = new List<char>(wordSet1);
        remainingChars.AddRange(new List<char>(wordSet2));
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
    {   Debug.Log("spawning new");
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
        if (isGameOver == false && !(countDown.isCountDown))
        {
            currentTime -= Time.deltaTime;
            SetTime(currentTime);
            float threshold = 0.05f; // Small threshold value to account for precision errors
            if (currentSplitIndex < ballSplitTimes.Count && Mathf.Abs(currentTime - ballSplitTimes[currentSplitIndex]) < threshold)
            { Debug.Log("currentTime "+currentTime);
            // Here, initiate the ball split logic. Depending on your implementation, you might call a function or set a flag.
            StartCoroutine(RandomizePowerUpActivePlayer());


            // Move to the next time checkpoint
            currentSplitIndex++;
             }
        }
    }

    void SetTime(float value)
    {
        //print(currentTime);
        TimeSpan time = TimeSpan.FromSeconds(currentTime);                       // set the time value
        TimerText.text = time.ToString("mm':'ss");   // convert time to Time format

        if (currentTime <= 0)
        {
            // Game Over
            GameEnd();
        }
    }

    public void GameEnd()
    {
        
        gameOverMenu.SetActive(true);
        if(scorePlayer1 > scorePlayer2)
        {
            // Display Game Over message and winner's name
            gameOverText.text = "Game Over\nPlayer 1 Wins!";
        }
        else if(scorePlayer2 > scorePlayer1)
        {
            // Display Game Over message and winner's name
            gameOverText.text = "Game Over\nPlayer 2 Wins!";
        }
        else
        {
            gameOverText.text = "Game Over\nIt's a Tie!";
        }
        isGameOver = true;
    }

    public void UpdateScores(string res1, string res2)
    {
        scoreTextLeft.SetScore(res1);
        scoreTextRight.SetScore(res2);
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

            for (int i = 0; i < word1.Length; i++)
            {
                res1 = res1 + "_";
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

            for (int i = 0; i < word2.Length; i++)
            {
                res2 = res2 + "_";
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
        if (pos == -1) return;  // exit if currChar is not in playerWord

        if (id == 1 && !res1.Contains(curr))
        {
            char[] res1Chars = res1.ToCharArray();
            res1Chars[pos] = currChar;
            res1 = new string(res1Chars);
            scorePlayer1 += 1;

            wordSet1.Remove(currChar); // remove from wordSet

            if (IsWordCompleted(res1, word1))
            {
                wallToggle.SwitchToPointedWalls();
                numWords1 += 1;

                if(numWords1 < 3)
                {
                    UpdateWord(id);
                    
                }
                else
                {
                    GameEnd();
                }
            }
        }
        else if (id == 2 && !res2.Contains(curr))
        {
            char[] res2Chars = res2.ToCharArray();
            res2Chars[pos] = currChar;
            res2 = new string(res2Chars);
            scorePlayer2 += 1;

            wordSet2.Remove(currChar);
        
            if (IsWordCompleted(res2, word2))
            {   wallToggle.SwitchToPointedWalls();
                numWords2 += 1;

                if (numWords2 < 3)
                {
                    UpdateWord(id);
                }
                else
                {
                    GameEnd();
                }
            }
        }
        
        UpdateScores(res1, res2);
        if (id == 1 && scorePlayer1 > 0 && scorePlayer1 % 3 == 0)
        {
            player1GunMovement.IncreaseBullets(); // Update bullets for player 1
        }
        else if (id == 2 && scorePlayer2 > 0 && scorePlayer2 % 3 == 0)
        {
            player2GunMovement.IncreaseBullets(); // Update bullets for player 2
        }
    }
}
