using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
public class PlayerPUData
{
    public int playerID = 0;
    public int hasScored = 0;
    public int freezePowerUsed = 0;
    public int magnifyPowerUsed = 0;
    public int movePowerUsed = 0;
    public int noPowerUsed = 0;
}
public class PlayerShootData
{
    public int bulletUsed = 0;
    public int ballShot = 0;
    public int timeStamp = 0;
}
public class PlayerWordData
{
    public int bulletUsed = 0;
    public int ballShot = 0;
    public int timeStamp = 0;
}
public class TrackAnalytics : MonoBehaviour
{
    public Ball ballScriptRef;
    public PowerUpManager powerUpManager;
    public GameManager gameManager;
    public void CollectPUScoreData(int playerIDNum)
    {
        PlayerPUData playerPUData = new PlayerPUData();
        playerPUData.playerID = playerIDNum;
        playerPUData.hasScored = 1;
        if(playerIDNum == 1)
        {
            if (powerUpManager.p1PowerUpActive)
            {
                switch (powerUpManager.p1powerup)
                {
                    case "MoveOpponent":
                        playerPUData.movePowerUsed = 1;
                        break;
                    case "Freeze":
                        playerPUData.freezePowerUsed = 1;
                        break;
                    case "Magnify":
                        playerPUData.magnifyPowerUsed = 1;
                        break;
                }
            }
            else { playerPUData.noPowerUsed = 1; }
        }
        else if (playerIDNum == 2)
        {
            if (powerUpManager.p2PowerUpActive)
            {
                switch (powerUpManager.p2powerup)
                {
                    case "MoveOpponent":
                        playerPUData.movePowerUsed = 1;
                        break;
                    case "Freeze":
                        playerPUData.freezePowerUsed = 1;
                        break;
                    case "Magnify":
                        playerPUData.magnifyPowerUsed = 1;
                        break;
                }
            }
            else { playerPUData.noPowerUsed = 1; }
        }


        string json = JsonUtility.ToJson(playerPUData);
        RestClient.Post("https://csci-526-powerups-default-rtdb.firebaseio.com/.json", playerPUData);
    }
    
    public void CollectPUNoScoreData(int playerIDNum)
    {
        PlayerPUData playerPUData = new PlayerPUData();
        playerPUData.playerID = playerIDNum;
        // when player 2 defended shot, check if player 1 used powerup
        if (playerIDNum == 2)
        {
            if (powerUpManager.p1PowerUpActive)
            {
                switch (powerUpManager.p1powerup)
                {
                    case "MoveOpponent":
                        playerPUData.movePowerUsed = 1;
                        break;
                    case "Freeze":
                        playerPUData.freezePowerUsed = 1;
                        break;
                    case "Magnify":
                        playerPUData.magnifyPowerUsed = 1;
                        break;
                }
            }
            else { playerPUData.noPowerUsed = 1; }
        }
        // when player 1 defended shot, check if player 2 used powerup
        else if (playerIDNum == 1)
        {
            if (powerUpManager.p2PowerUpActive)
            {
                switch (powerUpManager.p2powerup)
                {
                    case "MoveOpponent":
                        playerPUData.movePowerUsed = 1;
                        break;
                    case "Freeze":
                        playerPUData.freezePowerUsed = 1;
                        break;
                    case "Magnify":
                        playerPUData.magnifyPowerUsed = 1;
                        break;
                }
            }
            else { playerPUData.noPowerUsed = 1; }
        }
        string json = JsonUtility.ToJson(playerPUData);
        RestClient.Post("https://csci-526-powerups-default-rtdb.firebaseio.com/.json", playerPUData);
    }
    
    public void CollectShootData(bool hasHit)
    {
        PlayerShootData playerShootData = new PlayerShootData();
        playerShootData.bulletUsed = 1;
        playerShootData.timeStamp = (int)gameManager.currentTime;
        if (hasHit)
        {
            playerShootData.ballShot = 1;
        }
        string json = JsonUtility.ToJson(playerShootData);
        RestClient.Post("https://csci-526-shooting-default-rtdb.firebaseio.com//.json", playerShootData);
    }

    public void CollectWordData()
    {

    }
}
