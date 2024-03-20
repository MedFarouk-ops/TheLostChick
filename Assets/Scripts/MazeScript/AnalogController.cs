using UnityEngine;

public class AnalogController : MonoBehaviour
{
    private GameObject knobObject; // Reference to the analog knob object
    private Vector3 centerPosition; // Center position of the analog controller
    private bool isTouching; // Flag to track if touch is active
    private LineRenderer circleRenderer; // LineRenderer for drawing the circular boundary

    [SerializeField] private float knobRadius = 1f; // Radius of the analog knob
    [SerializeField] private float maxKnobDistance = 1f; // Maximum distance the knob can move from the center
    [SerializeField] private Color knobColor = Color.gray; // Color of the analog knob
    [SerializeField] private int circleSegments = 50; // Number of segments for drawing the circle
    [SerializeField] private float circleWidth = 4f; // Width of the circle boundary
    [SerializeField] private Material circleMaterial; // Material for the circle boundary

    // Singleton instance of the AnalogController
    public static AnalogController Instance { get; private set; }

    private void Awake()
    {
        Instance = this; // Set the singleton instance
        CreateKnob();
        CreateCircularBoundary();
    }

    private void Update()
    {
        HandleInput();
    }

    // Method for knob creation and initialization
    private void CreateKnob()
    {
        // Create a sphere for the knob
        knobObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        knobObject.transform.localScale = new Vector3(knobRadius * 2f, knobRadius * 2f, knobRadius * 2f);
        knobObject.GetComponent<Renderer>().material.color = knobColor;

        centerPosition = transform.position;
    }

    // Method for creating the circular boundary
    private void CreateCircularBoundary()
    {
        // Create a cylinder for the circular boundary
        GameObject boundaryObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        boundaryObject.transform.position = centerPosition;
        boundaryObject.transform.localScale = new Vector3(knobRadius * 4f, circleWidth, knobRadius * 4f);

        // Adjust rotation to align with the Y-axis
        boundaryObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);

        // Add a LineRenderer component to visualize the boundary
        circleRenderer = boundaryObject.AddComponent<LineRenderer>();
        circleRenderer.positionCount = circleSegments + 1;
        circleRenderer.startWidth = circleWidth;
        circleRenderer.endWidth = circleWidth;

        // Set the material for the circle renderer
        circleRenderer.material = circleMaterial;

        // Calculate points for drawing the circle
        float angleStep = 360f / circleSegments;
        float angle = 0f;

        for (int i = 0; i <= circleSegments; i++)
        {
            float x = centerPosition.x + Mathf.Sin(Mathf.Deg2Rad * angle) * knobRadius;
            float z = centerPosition.z + Mathf.Cos(Mathf.Deg2Rad * angle) * knobRadius;
            circleRenderer.SetPosition(i, new Vector3(x, centerPosition.y, z));
            angle += angleStep;
        }
    }

    // Method to handle input for analog movement (touch and mouse)
    private void HandleInput()
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
        Vector3 inputWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, centerPosition.z ));

        // Calculate the direction from the center to the input position
        Vector3 direction = (inputWorldPosition - centerPosition).normalized;

        // Calculate the knob position based on the input direction
        Vector3 horizontalDirection = new Vector3(direction.x, 0f, 0f);
        Vector3 verticalDirection = new Vector3(0f, 0f, 0f);
        Vector3 depthDirection = new Vector3(0f, 0f, direction.z); // Add depth direction
        Vector3 knobPosition = transform.position + horizontalDirection * Mathf.Min(Vector3.Distance(inputWorldPosition, centerPosition), maxKnobDistance)
                                                  + verticalDirection * Mathf.Min(Vector3.Distance(inputWorldPosition, centerPosition), maxKnobDistance)
                                                  + depthDirection * Mathf.Min(Vector3.Distance(inputWorldPosition, centerPosition), maxKnobDistance);

        // Update the knob position
        knobObject.transform.position = knobPosition;

        isTouching = true;
    }
        else
        {
            isTouching = false;
            knobObject.transform.position = centerPosition;
        }
    }

    // Method to get analog input direction
    public Vector3 GetAnalogDirection()
    {
        if (isTouching)
        {
            Vector3 direction = (knobObject.transform.position - centerPosition).normalized;
            return direction;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
