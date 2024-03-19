using UnityEngine;

public class EndLevelContact : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            // Call the GameManager to win the level
            GameManager.instance.CompleteLevel();
        }
    }
}
