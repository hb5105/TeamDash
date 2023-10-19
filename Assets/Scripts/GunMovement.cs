using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunMovement : MonoBehaviour
{ 
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public KeyCode fireKey = KeyCode.Space; // Define your fire key here

    private int bulletsFired = 0;
    private int maxBullets = 3;
    public int playerId;

    // Declare two TextMeshProUGUI variables
    public TextMeshProUGUI player1BulletsText;
    public TextMeshProUGUI player2BulletsText;


    void Update()
    {
        if (Input.GetKeyDown(fireKey) && bulletsFired < maxBullets)
        {
            FireBullet();
        }
         if (playerId == 1)
        {
            player1BulletsText.text = (maxBullets - bulletsFired).ToString();
        }
        else if (playerId == 2)
        {
            player2BulletsText.text = (maxBullets - bulletsFired).ToString();
        }
    }

    public void IncreaseBullets()
    {
        maxBullets++;
    }

    void FireBullet()
    {
        // Instantiate the bullet at the gun's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

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
}
