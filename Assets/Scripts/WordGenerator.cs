using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI textBox; // Reference to the Text element in the Inspector

    // List of random words to choose from
    private string[] randomWords = { "CAT", "CAR", "HAT", "HIT", "MAT", "DOG", "SUN", "PEN", "BUS", "SKY" };

    // Static variable to store the initial word
    private static string initialWord = null;

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
        return randomWords[Random.Range(0, randomWords.Length)];
    }

    // Call this method when the player successfully guesses the word
    public void OnWordCompleted()
    {
        textBox.text = GetRandomWord();
    }
}
