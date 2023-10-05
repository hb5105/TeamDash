using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void SetScore(string value)
    {
        text.text = value;
    }

    public string GetScore()
    {
        return text.text;
    }
}
