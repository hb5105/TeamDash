using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float id;
    public float moveSpeed = 2f;
    public float tiltAmount = 45.0f;

    private float processInput()
    {
        float movement = 0f;
        if(!GameManager.isGameOver)
        {
            bool moveForward = false;
            bool moveBackward = false;

            switch (id)
            {
                case 1:
                    movement = Input.GetAxis("MovePlayer1");
                    moveForward = Input.GetKey(KeyCode.W);
                    moveBackward = Input.GetKey(KeyCode.S);
                    break;
                case 2:
                    movement = Input.GetAxis("MovePlayer2");
                    moveForward = Input.GetKey(KeyCode.UpArrow);
                    moveBackward = Input.GetKey(KeyCode.DownArrow);
                    break;
            }

            // Check for conflicting input (e.g., A+D or W+S) and set movement to 0 if found
            if ((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) || 
                (moveForward && moveBackward))
            {
                movement = 0f;
            }
        }
        return movement;
    }

    private void Move(float movement)
    {
        Vector2 velo = rb2d.velocity;
        velo.y = movement * moveSpeed;
        rb2d.velocity = velo;
    }

    private void Tilt()
    {
        if (id == 1) // Player 1
        {
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                transform.rotation = Quaternion.Euler(0, 0, -tiltAmount);
            }
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
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
            if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
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
