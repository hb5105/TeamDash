using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallText : MonoBehaviour
{
    public GameObject ballTextObj;
    public GameManager gameManager=GameManager.instance;
    private TextMeshPro tmp;

    // public void Start()
    // {   
    //     tmp = ballTextObj.GetComponent<TextMeshPro>();
    //     int idx = Random.Range(0, gameManager.word.Length);
    //     tmp.SetText(gameManager.word[idx].ToString().ToUpper());
    // }
    
    public void Start()
    {
        if (ballTextObj == null)
        {
            Debug.LogError("ballTextObj is null!");
            return;
        }

        tmp = ballTextObj.GetComponent<TextMeshPro>();

        if (tmp == null)
        {
            Debug.LogError("tmp is null (TextMeshPro component not found)!");
            return;
        }

        if (gameManager == null)
        {
            Debug.LogError("gameManager is null!");
            return;
        }

        if (gameManager.remainingChars == null || gameManager.remainingChars.Count == 0)
        {
            Debug.LogError("remainingChars is null or empty!");
            return;
        }

        int idx = Random.Range(0, gameManager.remainingChars.Count);
        tmp.SetText(gameManager.remainingChars[idx].ToString().ToUpper());
    }


    public string getText()
    {
        string text = tmp.text.ToUpper();
        return text;
    }

    public void setText(string tchar)
    {
        tmp.text = tchar.ToUpper();
    }
}
