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

    void Start()
    {
        initialPosition = transform.localPosition; // Store initial position
        currentTargetRotation = rotationPoints[currentIndex];
    }

    void Update()
    {
        FollowParent(); // Make the gun follow the paddle
        RotateGun(); // Handle the gun rotation
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
