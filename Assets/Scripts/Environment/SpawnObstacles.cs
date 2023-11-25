using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject[] P1Obstacles, P2Obstacles;
    public GameObject obstacleNotifier;
    private bool isQuarterTimeActionDone;
    private bool isThreeQuarterTimeActionDone;
    private bool isFirstWarningDone;
    void Start()
    {
        isThreeQuarterTimeActionDone = false;
        isQuarterTimeActionDone = false;
        isFirstWarningDone = false;
        for (int i = 0; i < P1Obstacles.Length; i++)
        {
            P1Obstacles[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < P2Obstacles.Length; i++)
        {
            P2Obstacles[i].gameObject.SetActive(false);
        } 

    }

    void Update()
    {
        if (!isFirstWarningDone && (int)gameManager.currentTime == (((int)gameManager.totalTime / 4) * 3) + 4)
        {
            StartCoroutine(StartObstacleWarning());
            isFirstWarningDone=true;
        }
        if (!isThreeQuarterTimeActionDone && (int)gameManager.currentTime == (((int)gameManager.totalTime / 4) * 3))
        {
            SetObstacles();
            isThreeQuarterTimeActionDone = true;
        }
    }

    public void SetObstacles()
    {
        //first player obstacles index
        System.Random random = new System.Random();
        int P1firstIndex = random.Next(P1Obstacles.Length);
        int P1secondIndex = random.Next(P1Obstacles.Length);
        while(P1firstIndex == P1secondIndex)
        {
            P1secondIndex = random.Next(P1Obstacles.Length);
        }

        //second player obstacles index
        int P2firstIndex = random.Next(P2Obstacles.Length);
        int P2secondIndex = random.Next(P2Obstacles.Length);
        while(P2firstIndex == P2secondIndex)
        {
            P2secondIndex = random.Next(P2Obstacles.Length);
        }

        //set obstacles active
        P1Obstacles[P1firstIndex].gameObject.SetActive(true);
        P1Obstacles[P1secondIndex].gameObject.SetActive(true);
        P2Obstacles[P2firstIndex].gameObject.SetActive(true);
        P2Obstacles[P2secondIndex].gameObject.SetActive(true);

        // first player random rotation for obstacles
        foreach (GameObject obj in P1Obstacles)
        {
            float randomZRotation = UnityEngine.Random.Range(0f, 360f);
            obj.transform.localEulerAngles = new Vector3(0, 0, randomZRotation);
        }

        // second player random rotation for obstacles
        foreach (GameObject obj in P2Obstacles)
        {
            float randomZRotation = UnityEngine.Random.Range(0f, 360f);
            obj.transform.localEulerAngles = new Vector3(0, 0, randomZRotation);
        }

        StartCoroutine(HideObstacles(P1firstIndex,P1secondIndex, P2firstIndex, P2secondIndex));

    }

    IEnumerator HideObstacles(int p1index1, int p1index2, int p2index1, int p2index2)
    {
        yield return new WaitForSeconds(10);
        P1Obstacles[p1index1].gameObject.SetActive(false);
        P1Obstacles[p1index2].gameObject.SetActive(false);
        P2Obstacles[p2index1].gameObject.SetActive(false);
        P2Obstacles[p2index2].gameObject.SetActive(false);
    }

    IEnumerator StartObstacleWarning()
    {
        obstacleNotifier.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        obstacleNotifier.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        obstacleNotifier.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        obstacleNotifier.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        obstacleNotifier.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        obstacleNotifier.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        obstacleNotifier.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        obstacleNotifier.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        obstacleNotifier.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        obstacleNotifier.gameObject.SetActive(false);
    }

}
