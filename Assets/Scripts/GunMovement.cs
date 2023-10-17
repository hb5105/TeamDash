using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMovement : MonoBehaviour
{   
    public GameObject paddle; // Reference to the paddle
    public float rotationSpeed = 20f;
    private float[] rotationPoints = { 0f, 7f, 0f, -7f };
    private int currentIndex = 0;
    private float currentTargetRotation;
    private Vector3 initialPosition; // To store the initial local position of the gun
    public KeyCode fireKey = KeyCode.Space;
    public GameObject bulletPrefab; // Assign your Bullet Prefab here
    public float bulletSpeed = 10f; // Adjust the speed as needed
    private int bulletsFired = 0; // Track the number of bullets fired
    private const int maxBullets = 50; // Maximum bullets allowed to fire
    public Transform firePoint;

    void Start()
    {
        initialPosition = transform.localPosition; // Store initial position
        currentTargetRotation = rotationPoints[currentIndex];
    }

    void Update()
    {
        FollowParent(); // Make the gun follow the paddle
        RotateGun(); // Handle the gun rotation
         if (Input.GetKeyDown(fireKey) && bulletsFired < maxBullets)
        {
            FireBullet();
        }
    }


    

    void FireBullet()
    {
        // Instantiate the bullet at the gun's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        //GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        
        // Get the Rigidbody2D component from the bullet and add force to it
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        // Use the firePoint's right direction to ensure the bullet moves in the direction the gun is pointing
        Vector2 fireDirection = firePoint.right;
        if (fireKey != KeyCode.Space)
        {
            fireDirection = -fireDirection; // Invert direction if needed, based on your logic
        }
        rb.AddForce(fireDirection * bulletSpeed, ForceMode2D.Impulse);
        bulletsFired++;
    }

    void FollowParent()
    {
        if (paddle != null)
        {
            // If you want to keep the initial offset between the paddle and gun, add initialPosition.
            transform.position = paddle.transform.position + initialPosition; 
        }
    }

    void RotateGun()
    {
        float step = rotationSpeed * Time.deltaTime;
        float newRotationZ = Mathf.MoveTowardsAngle(transform.eulerAngles.z, currentTargetRotation, step);
        transform.eulerAngles = new Vector3(0f, 0f, newRotationZ);

        if (Mathf.Approximately(newRotationZ, currentTargetRotation))
        {
            currentIndex = (currentIndex + 1) % rotationPoints.Length;
            currentTargetRotation = rotationPoints[currentIndex];
        }
    }
}
