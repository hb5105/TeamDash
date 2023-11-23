using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject[] obstacles;
    void Start()
    {
        obstacles = new GameObject[6];
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.currentTime == 120 || gameManager.currentTime == 90 
            || gameManager.currentTime == 60 || gameManager.currentTime == 30)
        {
            System.Random random = new System.Random();
            int firstIndex = random.Next(obstacles.Length);
            int secondIndex = random.Next(obstacles.Length);


        }
    }
}
