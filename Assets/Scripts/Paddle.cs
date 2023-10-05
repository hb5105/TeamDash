using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float id;
    public float moveSpeed = 2f;

    private float processInput()
    {
        float movement = 0f;
        //if(!GameManager.isGameOver){
            switch (id)
            {
                case 1:
                    movement = Input.GetAxis("MovePlayer1");
                    break;
                case 2:
                    movement = Input.GetAxis("MovePlayer2");
                    break;
            }
        //}
        return movement;

    }
    private void Move(float movement)
    {
        Vector2 velo = rb2d.velocity;
        velo.y = movement * moveSpeed;
        rb2d.velocity = velo;
        
    }
    private void Update()
    {  
        float movement = processInput();
        Move(movement);
    }
}
