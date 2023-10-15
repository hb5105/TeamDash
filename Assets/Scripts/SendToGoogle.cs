using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendToGoogle : MonoBehaviour
{
    public GameObject paddleParent;
    public GameObject ball;
    private GameObject paddleLeft;
    private GameObject paddleRight;
    // Start is called before the first frame update
    void Start()
    {
        paddleLeft = paddleParent.transform.Find("PaddleLeft").gameObject;
        paddleRight = paddleParent.transform.Find("PaddleRight").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
