using UnityEngine;
using UnityEngine.EventSystems;
using EasyJoystick ;

public class PlayerAnalogControl : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private Joystick joystick;

    public static PlayerAnalogControl instance ;

    void Awake(){
        instance = this;
    }

    private void Start()
    {
        joystick = FindObjectOfType<Joystick>();
    }

    private void Update()
    {
        if (joystick == null)
            return;

        // Get input direction from joystick
        Vector3 inputDirection = new Vector3(joystick.Vertical (), 0f, -joystick.Horizontal () ).normalized;

        if (inputDirection != Vector3.zero)
        {
            // Convert input direction to world space
            Vector3 targetPosition = transform.position + inputDirection;

            // Rotate the player smoothly towards the input direction
            Quaternion lookRotation = Quaternion.LookRotation(-inputDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

            // Move the player towards the input direction
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
