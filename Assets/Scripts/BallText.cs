using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallText : MonoBehaviour
{
    public GameObject ballTextObj;
    public GameManager gameManager;
    private TextMeshPro tmp;

    public void Start()
    {
        tmp = ballTextObj.GetComponent<TextMeshPro>();
        int idx = Random.Range(0, gameManager.word.Length);
        tmp.SetText(gameManager.word[idx].ToString().ToUpper());
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
