using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class WordGenerator : MonoBehaviour
{
    public ResetController ResetController;
    // Start is called before the first frame update
    public TextMeshProUGUI textBox; // Reference to the Text element in the Inspector

    // Lists of random words based on length
    private static List<string> threeLetterWords = new List<string> { };
    private static List<string> fourLetterWords = new List<string> { "GOAL" };
    private static List<string> fiveLetterWords = new List<string> { "SCORE", "GRASS", "FIELD", "FOULS"};

    // Current list being used
    private static List<string> currentList = new List<string> { };
    private int listIndex = 0;

    // Static variable to store the initial word
    private static string initialWord = null;

    private int currIndex = -1;
    private string currWord = "";

    private void Start()
    {
        if (textBox == null)
        {
            Debug.LogError("Text element not assigned in the Inspector.");
            return;
        }

        // Start with the 3-letter words
        currentList = new List<string>(threeLetterWords);

        // If initialWord is null, assign a random word to it
        if (initialWord == null)
        {
            initialWord = GetRandomWord();
        }

        textBox.text = initialWord;
    }

    // Method to get a random word
    private string GetRandomWord()
    {
        if (currentList.Count == 0)
        {
            // Move to the next list of words based on their length
            listIndex++;
            if (listIndex == 1) currentList = new List<string>(fourLetterWords);
            else if (listIndex == 2) currentList = new List<string>(fiveLetterWords);
            else
            {
                Debug.Log("All words used. Restarting with 3-letter words.");
                listIndex = 0;
                currentList = new List<string>(threeLetterWords);
            }
        }

        currIndex = Random.Range(0, currentList.Count);
        currWord = currentList[currIndex];
        currentList.RemoveAt(currIndex);

        return currWord;
    }

    // Call this method when the player successfully guesses the word
    public void OnWordCompleted()
    {
        textBox.text = GetRandomWord();
    }
}

