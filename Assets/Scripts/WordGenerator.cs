using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI textBox; // Reference to the Text element in the Inspector

    // List of random words to choose from
    private string[] randomWords = { "SPACE", "PLANET", "EARTH", "MARS" };

    private void Start()
    {
        if (textBox != null)
        {
            // Get a random word from the array
            string randomWord = randomWords[Random.Range(0, randomWords.Length)];

            // Set the Text element's text to the random word
            textBox.text = randomWord;
        }
        else
        {
            Debug.LogError("Text element not assigned in the Inspector.");
        }
    }
}
