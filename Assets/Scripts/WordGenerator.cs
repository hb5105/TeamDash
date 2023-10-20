using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class WordGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI textBox; // Reference to the Text element in the Inspector

    // List of random words to choose from
    private static List<string> randomWords = new List<string> { "CAT","DOG", "DOWN" ,"FOUR" ,"WATER", "OCEAN" , "FOREST", "FLOWER", "MACHINE", "HUSBAND"};

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
        currIndex = Random.Range(0, randomWords.Count);
        currWord = randomWords[currIndex];
        randomWords.RemoveAt(currIndex);

        return currWord;
    }

    // Call this method when the player successfully guesses the word
    public void OnWordCompleted()
    {
        textBox.text = GetRandomWord();
    }
}
