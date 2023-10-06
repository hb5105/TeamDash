using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int scorePlayer1, scorePlayer2;
    public ScoreText scoreTextLeft, scoreTextRight;
    public BallText ballText;
    public WordGenerator wordGenerator;
    private TextMeshProUGUI textBox;
    public TextMeshProUGUI gameOverText;
    public string word;
    public static bool isGameOver = false;
    private HashSet<char> wordSet = new HashSet<char>();
    private HashSet<char> usedSet = new HashSet<char>();


    private string res1;
    private string res2;

    public void Start()
    {
        textBox = wordGenerator.textBox;
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
    public void UpdateScores(string res1, string res2)
    {
        scoreTextLeft.SetScore(res1);
        scoreTextRight.SetScore(res2);
    }
    public bool IsWordCompleted(string playerProgress, string targetWord)
    {
        return playerProgress == targetWord;
    }
}
