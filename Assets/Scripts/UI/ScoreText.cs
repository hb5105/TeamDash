using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    public TextMeshProUGUI text;
    private string rawText = ""; // To store the unformatted text

    public void SetScore(string value, string raw, List<int> pos)
    {
        //foreach (int p in pos)
        //{
        //    Debug.Log(p);
        //}
        rawText = value;
        string coloredText = "";
        for (int i = 0; i < raw.Length; i++)
        {
            if (pos.Contains(i))
            {
                Color textColor = new Color(1, 1, 1);
                string hexColor = ColorUtility.ToHtmlStringRGB(textColor);
                Debug.Log(hexColor);
                coloredText += $"<color=#{hexColor}>{raw[i]}</color>";
            }
            else
            {
                Color textColor = new Color(106f/255f, 106f/255f, 106f/255f);
                string hexColor = ColorUtility.ToHtmlStringRGB(textColor);
                coloredText += $"<color=#{hexColor}>{raw[i]}</color>";
            }
        }
        text.text = coloredText;
    }

    public string GetScore()
    {
        return rawText;
    }
}
