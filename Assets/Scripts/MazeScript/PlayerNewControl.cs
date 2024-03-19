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
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                isTouching = true;
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
            else if (touch.phase == TouchPhase.Ended)
            {
                isTouching = false;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            isTouching = true;
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
        // Create an empty GameObject to hold the arrow
        traceObject = new GameObject("ArrowTrace");
        
        // Create an empty MeshFilter and MeshRenderer for the arrow
        MeshFilter meshFilter = traceObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = traceObject.AddComponent<MeshRenderer>();

        // Create a new Mesh for the arrow
        Mesh mesh = new Mesh();

        // Define vertices for the arrow (you can adjust these vertices to form your arrow shape)
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0, 0.5f, 0),       // Tip of the arrow (higher position)
            new Vector3(0.5f, 0.5f, -0.5f), // Right bottom corner
            new Vector3(-0.5f, 0.5f, -0.5f),// Left bottom corner
            new Vector3(0.5f, 0.5f, -1.5f), // Right end of the arrow tail
            new Vector3(-0.5f, 0.5f, -1.5f) // Left end of the arrow tail
        };

        // Define triangles for the arrow
        int[] triangles = new int[]
        {
            0, 1, 2, // Triangle 1
            0, 3, 1, // Triangle 2
            0, 2, 4, // Triangle 3
            0, 4, 3, // Triangle 4
            1, 3, 2, // Triangle 5
            2, 3, 4  // Triangle 6
        };

        // Apply vertices and triangles to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // Recalculate normals for the mesh
        mesh.RecalculateNormals();

        // Assign the mesh to the MeshFilter
        meshFilter.mesh = mesh;

        // Apply material to the MeshRenderer
        meshRenderer.material = traceMaterial;
    }

    // Set the position of the arrow to the target position
    traceObject.transform.position = targetPosition;

    if (targetPosition != transform.position)
    {
        Vector3 direction = (transform.position - targetPosition).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        traceObject.transform.rotation = Quaternion.Euler(10f, 0f, 0f) * lookRotation; // Adjust the arrow's initial rotation as needed
    }
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
