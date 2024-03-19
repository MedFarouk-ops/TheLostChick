using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObstacle : MonoBehaviour
{
    public GameObject[] vegetableModels; // Array to hold your vegetable 3D models

    private void Start()
    {
        // Call a function to display a random vegetable model
        DisplayRandomVegetable();
    }

    private void DisplayRandomVegetable()
    {
        // Check if you have at least one vegetable model
        if (vegetableModels.Length > 0)
        {
            // Choose a random index from the array
            int randomIndex = Random.Range(0, vegetableModels.Length);

            // Instantiate the randomly chosen vegetable at the current position
            GameObject randomVegetable = Instantiate(vegetableModels[randomIndex], transform.position, Quaternion.identity);

            // Parent the vegetable to the same GameObject (optional)
            randomVegetable.transform.parent = transform;

            
        }
        else
        {
            Debug.LogError("No vegetable models assigned to the array.");
        }
    }

 
}
