using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StopwatchTimer : MonoBehaviour
{
    [SerializeField] private Image uiFill;
    public int Duration;
    private int remainingDuration;

    private void Start()
    {
        //Begin(Duration);
    }

    public void BeginCountdown(int Second)
    {
        Begin(Second);
    }

    private void Begin(int Second)
    {
        remainingDuration = Second;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingDuration >= 0)
        {
            uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
            remainingDuration--;
            yield return new WaitForSeconds(1f);
        }
        OnEnd();
    }
     
    private void OnEnd()
    {
        print("End");
    }
}
