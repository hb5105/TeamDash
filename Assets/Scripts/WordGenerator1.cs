using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordGenerator1 : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI textBox; // Reference to the Text element in the Inspector

    // List of random words to choose from
    private string[] randomWords = { "CAT", "CAR", "HAT", "HIT", "MAT", "DOG", "SUN", "PEN", "BUS", "SKY" };

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
