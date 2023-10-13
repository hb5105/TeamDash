using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float id;
    public float moveSpeed = 2f;
    public float tiltAmount = 45.0f;
    public Ball ball;
    private bool isTilting = false; 
 
    private float processInput()
{   
    float movement = 0f;

    if(!GameManager.isGameOver)
    {

        if (id == 1)  // Player 1
        {
            if (Input.GetKey(KeyCode.W)) movement = 1f;
            if (Input.GetKey(KeyCode.S)) movement = -1f;

            if (Input.GetKey(KeyCode.D) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
            {
                movement = 0f;
                isTilting = true;
            }
            else{
                isTilting=false;
            }
        }
        else if (id == 2)  // Player 2
        {
            if (Input.GetKey(KeyCode.UpArrow)) movement = 1f;
            if (Input.GetKey(KeyCode.DownArrow)) movement = -1f;

            if (Input.GetKey(KeyCode.LeftArrow) && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
            {
                movement = 0f;
                isTilting = true;
            }
            else{
                isTilting=false;
            }
        }
    }

    return movement;
}
private bool IsTryingToTilt()
{
    if (id == 1 && Input.GetKey(KeyCode.D) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
        return true;
    if (id == 2 && Input.GetKey(KeyCode.LeftArrow) && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
        return true;

    return false;
}

 
    private void Move(float movement)
    {
        if (!isTilting)  // Only update the paddle's velocity if it's not in the tilting state
        {
            Vector2 velo = rb2d.velocity;
            velo.y = movement * moveSpeed;
            rb2d.velocity = velo;
        }
    }

    private void Tilt()
    {
        if (id == 1) // Player 1
        {
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                transform.rotation = Quaternion.Euler(0, 0, -tiltAmount);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                transform.rotation = Quaternion.Euler(0, 0, tiltAmount);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else if (id == 2) // Player 2
        {
            if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                transform.rotation = Quaternion.Euler(0, 0, -tiltAmount);
            }
            else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                transform.rotation = Quaternion.Euler(0, 0, tiltAmount);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }


    private void Update()
    {
        float movement = processInput();
        Move(movement);
        Tilt();
    }
}
