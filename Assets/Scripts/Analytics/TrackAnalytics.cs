using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
public class PlayerPUData
{
    public int playerID;
    public int hasScored = 0;
    public float freezePowerUsed = 0;
    public float magnifyPowerUsed = 0;
    public float movePowerUsed = 0;
    public float noPowerUsed = 0;
}
public class TrackAnalytics : MonoBehaviour
{
    public Ball ballScriptRef;
    public PowerUpManager powerUpManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ballScriptRef.hasTriggeredScoreZone)
        {
            ballScriptRef.hasTriggeredScoreZone = false;
            PlayerPUData playerPUData = new PlayerPUData();
            playerPUData.hasScored = 1;
            
            if(ballScriptRef.hasP1Scored)
            {
                ballScriptRef.hasP1Scored = false;
                playerPUData.playerID = 1;
                if (powerUpManager.p1PowerUpActive)
                {
                    if (powerUpManager.p1powerup == "MoveOpponent") { playerPUData.movePowerUsed = 1; }
                    else if (powerUpManager.p1powerup == "Freeze") { playerPUData.freezePowerUsed = 1; }
                    else if (powerUpManager.p1powerup == "Magnify") { playerPUData.magnifyPowerUsed = 1; }
                    else { playerPUData.movePowerUsed = 234; }
                }
                else { playerPUData.noPowerUsed = 1; }
            }
            else if (ballScriptRef.hasP2Scored)
            {
                ballScriptRef.hasP2Scored = false;
                playerPUData.playerID = 2;
                if (powerUpManager.p2PowerUpActive)
                {
                    if (powerUpManager.p2powerup == "MoveOpponent") { playerPUData.movePowerUsed = 1; }
                    else if (powerUpManager.p2powerup == "Freeze") { playerPUData.freezePowerUsed = 1; }
                    else if (powerUpManager.p2powerup == "Magnify") { playerPUData.magnifyPowerUsed = 1; }
                    else { playerPUData.movePowerUsed = 234; }
                }
                else { playerPUData.noPowerUsed = 1; }
            }
            string json = JsonUtility.ToJson(playerPUData);
            RestClient.Post("https://csci-526-powerups-default-rtdb.firebaseio.com/.json", playerPUData);
        }
        else if( ballScriptRef.hasCollidedPaddle)
        {
            Debug.Log("triggered data collection for paddle hit");
            ballScriptRef.hasCollidedPaddle = false;
            PlayerPUData playerPUData = new PlayerPUData();
            if (ballScriptRef.hasP1PaddleHit)
            {
                ballScriptRef.hasP1PaddleHit = false;
                playerPUData.playerID = 1;
                if (powerUpManager.p1PowerUpActive)
                {
                    if (powerUpManager.p1powerup == "MoveOpponent") { playerPUData.movePowerUsed = 1; }
                    else if (powerUpManager.p1powerup == "Freeze") { playerPUData.freezePowerUsed = 1; }
                    else if (powerUpManager.p1powerup == "Magnify") { playerPUData.magnifyPowerUsed = 1; }
                }
                else { playerPUData.noPowerUsed = 1; }
            }
            else if (ballScriptRef.hasP2PaddleHit)
            {
                ballScriptRef.hasP2PaddleHit = false;
                playerPUData.playerID = 2;
                if (powerUpManager.p2PowerUpActive)
                {
                    if (powerUpManager.p2powerup == "MoveOpponent") { playerPUData.movePowerUsed = 1; }
                    else if (powerUpManager.p2powerup == "Freeze") { playerPUData.freezePowerUsed = 1; }
                    else if (powerUpManager.p2powerup == "Magnify") { playerPUData.magnifyPowerUsed = 1; }
                }
                else { playerPUData.noPowerUsed = 1; }
            }
            string json = JsonUtility.ToJson(playerPUData);
            RestClient.Post("https://csci-526-powerups-default-rtdb.firebaseio.com/.json", playerPUData);
        }
    }
}
