using UnityEngine;

public class AnalogMove : MonoBehaviour
{
    [SerializeField] private Transform analogTransform; // Reference to the analog's RectTransform
    [SerializeField] private float offsetX; // Offset in the x-axis from the player
    [SerializeField] private float offsetY; // Offset in the y-axis from the player

    private Transform playerTransform; // Reference to the player's transform

    private void Start()
    {
        // Assuming you have a reference to the player's transform, if not, adjust this line accordingly
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
    
        // Calculate the desired position for the analog based on the player's position and offset
        Vector3 playerPosition = playerTransform.position;
        Vector3 desiredPosition = new Vector3(playerPosition.x + offsetX, playerPosition.y + offsetY, playerPosition.z);

        // Convert the desired position from world space to screen space
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(desiredPosition);

        // Set the analog's position to the screen position
        transform.position = new Vector3(screenPosition.x, analogTransform.position.y, analogTransform.position.z);
    }
}
