using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDown : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public float countdownDuration = 3.0f;
    public Image darkBackground;
    public GameObject gameManager;
    public GameObject ball;
    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        gameManager.SetActive(false);
        ball.SetActive(false);
        int countdownValue = (int)countdownDuration;
        darkBackground.gameObject.SetActive(true);
        while(countdownValue > 0)
        {
            countdownText.text = countdownValue.ToString();
            yield return new WaitForSeconds(1.0f);
            countdownValue--;
        }
        countdownText.text = "GO!";

        yield return new WaitForSeconds(1.0f);
        countdownText.gameObject.SetActive(false);
        darkBackground.gameObject.SetActive(false);
        gameManager.SetActive(true);
        ball.SetActive(true);

    }
}
