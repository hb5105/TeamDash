﻿using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public enum PowerUpType { None, Freeze, Magnify, MoveOpponent }
    private PowerUpType[] powerUpArray = { PowerUpType.Freeze, PowerUpType.Magnify, PowerUpType.MoveOpponent };

    private Paddle paddle1;
    private Paddle paddle2;

    public string p1powerup = "";
    public string p2powerup = "";

    public bool p1PowerUpActive = false;
    public bool p2PowerUpActive = false;

    private float powerUpCooldown = 10f;
    private float powerUpActiveDuration = 10f;

    private void Start()
    {
        Paddle[] paddles = FindObjectsOfType<Paddle>();
        paddle1 = paddles[0].id == 1 ? paddles[0] : paddles[1];
        paddle2 = paddles[1].id == 2 ? paddles[1] : paddles[0];
        AssignRandomPowerUp();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !p1PowerUpActive && p1powerup != ""&& !p2PowerUpActive)
        {
            ActivatePowerUp(paddle1, p1powerup);
            p1PowerUpActive = true;
            p1powerup = "";
            Invoke("DeactivateP1PowerUp", powerUpActiveDuration);
        }

        if (Input.GetKeyDown(KeyCode.Slash) && !p2PowerUpActive && p2powerup != ""&& !p1PowerUpActive)
        {
            ActivatePowerUp(paddle2, p2powerup);
            p2PowerUpActive = true;
            p2powerup = "";
            Invoke("DeactivateP2PowerUp", powerUpActiveDuration);
        }
    }

    void AssignRandomPowerUp()
    {
        p1powerup = powerUpArray[Random.Range(0, powerUpArray.Length)].ToString();
        p2powerup = powerUpArray[Random.Range(0, powerUpArray.Length)].ToString();
    }

    //void ActivatePowerUp(Paddle paddle, string powerUpName)
    //{
    //    if (paddle == null) return;

    //    switch (powerUpName)
    //    {
    //        case "Freeze":
    //            paddle.gameObject.GetComponent<PaddleFreezePowerUp>().FreezeOpponent();
    //            break;
    //        case "Magnify":
    //            paddle.gameObject.GetComponent<PaddleSizePowerUp>().ActivatePowerUp();
    //            break;
    //        case "MoveOpponent":
    //            paddle.gameObject.GetComponent<PaddleMovePowerUp>().ShiftOpponentPosition();
    //            break;
    //    }
    //}
    void ActivatePowerUp(Paddle paddle, string powerUpName)
    {
        if (paddle == null)
        {
            Debug.LogError("Paddle is null in ActivatePowerUp method.");
            return;
        }

        switch (powerUpName)
        {
            case "Freeze":
                var freezePowerUp = paddle.gameObject.GetComponent<PaddleFreezePowerUp>();
                if (freezePowerUp == null)
                {
                    Debug.LogError("PaddleFreezePowerUp script is missing on paddle " + paddle.id);
                    return;
                }
                freezePowerUp.FreezeOpponent();
                break;
            case "Magnify":
                var sizePowerUp = paddle.gameObject.GetComponent<PaddleSizePowerUp>();
                if (sizePowerUp == null)
                {
                    Debug.LogError("PaddleSizePowerUp script is missing on paddle " + paddle.id);
                    return;
                }
                sizePowerUp.ActivatePowerUp();
                break;
            case "MoveOpponent":
                var movePowerUp = paddle.gameObject.GetComponent<PaddleMovePowerUp>();
                if (movePowerUp == null)
                {
                    Debug.LogError("PaddleMovePowerUp script is missing on paddle " + paddle.id);
                    return;
                }
                movePowerUp.ShiftOpponentPosition();
                break;
        }
    }


    void DeactivateP1PowerUp()
    {
        p1PowerUpActive = false;
        Invoke("AssignPowerUpToP1", powerUpCooldown);
    }

    void DeactivateP2PowerUp()
    {
        p2PowerUpActive = false;
        Invoke("AssignPowerUpToP2", powerUpCooldown);
    }

    void AssignPowerUpToP1()
    {
        p1powerup = powerUpArray[Random.Range(0, powerUpArray.Length)].ToString();
    }

    void AssignPowerUpToP2()
    {
        p2powerup = powerUpArray[Random.Range(0, powerUpArray.Length)].ToString();
    }
}