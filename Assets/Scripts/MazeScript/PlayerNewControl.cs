using UnityEngine;
using UnityEngine.UI; // Required for Button
using UnityEngine.EventSystems; // Required for EventSystem

public class PlayerNewControl : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private float rotationSpeed = 5f; // Adjust rotation speed

    private Vector3 targetPosition = Vector3.zero; // Target position for the player to move towards

    void Update()
    {
        // Ignore movement input if clicking on a UI button
    if (IsPointerOverButton())
            return;
     HandleMovementInput();

        // Draw Gizmo
        OnDrawGizmos();
    }

    void HandleMovementInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                // Convert touch position to world space
                targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));
                targetPosition.y = transform.position.y; // Set target position's Y to player's Y

                // Rotate the player smoothly towards the touched position
                Vector3 direction = (targetPosition - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(-direction); // Invert the direction
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

                // Move the player towards the touched position
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
        }
        else if (Input.GetMouseButton(0))
        {
            // Handle mouse input similarly to touch input
            targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            targetPosition.y = transform.position.y; // Set target position's Y to player's Y

            // Rotate the player smoothly towards the clicked position
            Vector3 direction = (targetPosition - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(-direction); // Invert the direction
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

            // Move the player towards the clicked position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    // Check if the pointer is over a UI button
    bool IsPointerOverButton()
    {
        // Check if the pointer is over a UI element
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // Check if the pointer is over a button
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
                if (button != null)
                {
                    // Pointer is over a button
                    return true;
                }
            }
        }
        return false;
    }

    // Draw a gizmo to visualize the target position
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(targetPosition, 0.5f);
    }
}
