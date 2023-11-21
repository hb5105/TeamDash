using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallText : MonoBehaviour
{
    public GameObject ballTextObj;
    public GameManager gameManager=GameManager.instance;
    private TextMeshPro tmp;
    public  bool isClonedBall = false;
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

        int idx = Random.Range(0, gameManager.remainingChars.Count);
        if(!isClonedBall)
        {
        tmp.SetText(gameManager.remainingChars[idx].ToString().ToUpper());
        //Debug.Log("Start called and text is "+gameManager.remainingChars[idx].ToString().ToUpper()+tmp.text);
        }
    }


    public string getText()
    {
        string text = tmp.text.ToUpper();
        return text;
    }

    public void setText(string tchar)
    {
        if (tmp == null) 
        {
            Debug.LogError("setText: tmp is null (TextMeshPro component not found)!");
            return;
        }
        //Debug.Log("setText called and text is "+tchar+tmp.text);
        tmp.text = tchar.ToUpper();
        // Debug.Log("setText called and text is "+tmp.text);
    }
    public void InitializeText(string text)
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
        //Debug.Log("InitializeText called and text is "+text);

        tmp.SetText(text.ToUpper());
        //Debug.Log("InitializeText2 called and text is "+tmp.text);
    }

}
