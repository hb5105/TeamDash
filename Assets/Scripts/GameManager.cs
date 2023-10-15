using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    public int scorePlayer1, scorePlayer2;
    public ScoreText scoreTextLeft, scoreTextRight;

    public Ball ballPrefab;

    public BallText ballText;
    public WordGenerator wordGenerator;
    private TextMeshProUGUI textBox;
    public TextMeshProUGUI gameOverText;
    public string word;
    public static bool isGameOver = false;
    private HashSet<char> wordSet = new HashSet<char>();
    private HashSet<char> usedSet = new HashSet<char>();
    [SerializeField] private float timeInSeconds;
    [SerializeField] private TextMeshProUGUI timerText;


    private string res1;
    private string res2;
    private float currentTime;
    public TextMeshProUGUI TimerText { get => timerText; }

    public void Start()
    {
        textBox = wordGenerator.textBox;
        currentTime = timeInSeconds;
        word = textBox.text;
        Debug.Log(word);
        res1 = "";
        res2 = "";
        for (int i = 0; i < word.Length; i++)
        {
            res1 = res1 + "_";
            res2 = res2 + "_";
        }
        foreach (char c in word)
        {
            wordSet.Add(c);
        }
        UpdateScores(res1, res2);
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
        if (isGameOver == false)
        {
            currentTime -= Time.deltaTime;
            SetTime(currentTime);
        }
    }

    void SetTime(float value)
    {
        print(currentTime);
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
        // Display Game Over message and winner's name
        gameOverText.text = "Game Over\nPlayer 2 Wins!";
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
    public void OnScoreZoneReached(int id,GameObject ballGameObject)
    {   ballText=ballGameObject.GetComponentInChildren<BallText>();
        res1 = scoreTextLeft.GetScore();
        res2 = scoreTextRight.GetScore();
        Debug.Log(res1);
        Debug.Log(res2);
        string curr = ballText.getText();
        char currChar = curr[0];
        int pos = word.IndexOf(currChar);

        if (id == 1 && !res1.Contains(curr))
        {
            char[] res1Chars = res1.ToCharArray();
            res1Chars[pos] = currChar;
            res1 = new string(res1Chars);
            if (res2.Contains(curr))
            {
                usedSet.Add(currChar); // add to usedSet
                wordSet.Remove(currChar); // remove from wordSet
            }
        }
        else if (id == 2 && !res2.Contains(curr))
        {
            char[] res2Chars = res2.ToCharArray();
            res2Chars[pos] = currChar;
            res2 = new string(res2Chars);
            if (res1.Contains(curr))
            {
                usedSet.Add(currChar);
                wordSet.Remove(currChar);
            }
        }
        if (IsWordCompleted(res1, word))
        {
            // Display Game Over message and winner's name
            gameOverText.text = "Game Over\nPlayer 1 Wins!";
            isGameOver = true;
        }
        else if (IsWordCompleted(res2, word))
        {
            // Display Game Over message and winner's name
            gameOverText.text = "Game Over\nPlayer 2 Wins!";
            isGameOver = true;
        }
        UpdateScores(res1, res2);
        if (!isGameOver)
        {
            if (wordSet.Count == 0)
            {
                Debug.LogWarning("All characters are used!");
                return;
            }
            List<char> remainingChars = new List<char>(wordSet);
            int idx = Random.Range(0, remainingChars.Count);
            char nextChar = remainingChars[idx];
            ballText.setText(nextChar.ToString());

        }
    }
}
