using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float moveSpeed = 0.25f; // Speed of cloud movement
    public float moveDistance = 2.0f; // Distance the cloud moves from side to side

    private bool moveRight = true; // Flag to indicate direction of movement

    void Update()
    {
        // Calculate movement distance based on time and speed
        float moveDelta = moveSpeed * Time.deltaTime;

        // Move the cloud horizontally
        if (moveRight)
            transform.Translate(Vector3.right * moveDelta);
        else
            transform.Translate(Vector3.left * moveDelta);

        // Check if the cloud reaches the maximum move distance
        if (transform.localPosition.x >= moveDistance)
        {
            // Change direction to move left
            moveRight = false;
        }
        else if (transform.localPosition.x <= -moveDistance)
        {
            // Change direction to move right
            moveRight = true;
        }
    }
}
