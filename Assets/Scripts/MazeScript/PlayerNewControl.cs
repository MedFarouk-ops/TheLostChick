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
    private bool isTouching = false; // Flag to track if touch is active
    private GameObject traceObject; 
    [SerializeField]
    private Material traceMaterial; // Material for the trace object
// Object to draw the trace

    void Update()
    {
        // Ignore movement input if clicking on a UI button
        if (IsPointerOverButton())
            return;
        
        HandleMovementInput();

        // Draw Gizmo
        if (isTouching)
            DrawTrace();
        else
            RemoveTrace();
    }

    void HandleMovementInput()
{
    if (Input.touchCount > 0 || Input.GetMouseButton(0))
    {
        Vector3 inputPosition;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            inputPosition = touch.position;
        }
        else
        {
            inputPosition = Input.mousePosition;
        }

        // Convert input position to world space
        targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, 10f));
        targetPosition.y = transform.position.y; // Set target position's Y to player's Y

        // Rotate the player smoothly towards the input position
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(-direction); // Invert the direction
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        // Move the player towards the input position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        isTouching = true;
    }
    else
    {
        isTouching = false;
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

     void DrawTrace()
    {
        if (traceObject == null)
        {
            traceObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Destroy(traceObject.GetComponent<Collider>());
        }
        traceObject.transform.position = targetPosition;
        traceObject.transform.localScale = Vector3.one * 0.2f;
    }

 



    // Remove the trace when the touch is released
    void RemoveTrace()
    {
        if (traceObject != null)
        {
            Destroy(traceObject);
            traceObject = null;
        }
    }
}
