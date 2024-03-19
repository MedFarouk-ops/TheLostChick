using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab; // Reference to the bullet prefab

    [SerializeField]
    private float bulletSpeed = 10f; // Speed of the bullet

    [SerializeField]
    private Vector3 shootOffset = new Vector3(0f, 0.5f, 1f); // Offset for the shoot position

    void Update()
    {
        // Shooting
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Calculate the shoot position with offset
        Vector3 shootPosition = transform.position + transform.TransformDirection(shootOffset);

        // Instantiate a bullet at the shoot position
        GameObject bullet = Instantiate(bulletPrefab, shootPosition, Quaternion.identity);

        // Get the Rigidbody component of the bullet
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        // Apply forward force to the bullet
        rb.velocity = transform.forward * bulletSpeed;
    }
}
