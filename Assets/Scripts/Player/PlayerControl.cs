using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance;

    public Transform rightPosition;
    public Transform leftPosition;
    public Animator animator; // Add an Animator component for jump animation.

    bool IsMovedToLeft = false;
    bool IsMovedToRight = false;
    bool IsInCenter = true;

    public Vector3 startingPosition;

    public float jumpForce = 6f;
    public Rigidbody rb; // Rename rigidbody to rb for consistency.
    private int jumpNumber = 0; // Initialize jumpNumber to 0.

    public float speed = 2f;

    private Vector3 targetPosition;


    void Start(){
        
        transform.position = new Vector3(0f ,0f , -11.37f);
        // Assuming 'rb' is a reference to the Rigidbody component
        rb.velocity = Vector3.zero;
    }

    void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        startingPosition = transform.position;
        targetPosition = transform.position;
        animator = GetComponent<Animator>(); // Assign the Animator component.
        // Assuming 'rb' is a reference to the Rigidbody component
        rb.velocity = Vector3.zero;

    }

    

    void Update()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded()) // Allow up to 2 jumps.
        {
            Jump();
        }

        if (Input.GetButtonDown("q"))
        {
            MoveToLeft();
        }

        if (Input.GetButtonDown("d"))
        {
            MoveToRight();
        }
        if (Input.GetButtonDown("s"))
        {
            Slide();
        }
        
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (Vector3.Distance(transform.position, targetPosition) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, transform.position.y, transform.position.z), 30f * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            targetPosition = transform.position;
        }
    }

  bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1f, LayerMask.GetMask("Ground")))
        {
            animator.SetTrigger("NotJump");
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Jump()
    {
        if (IsGrounded()) // Check jumpNumber to allow double jump.
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Zero out the vertical velocity.
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpNumber++;
            animator.SetTrigger("Jump"); // Trigger the jump animation.
        }
    }

    public void MoveToLeft()
    {
        if (IsMovedToRight)
        {
            MoveInit();
        }
        else if (IsMovedToLeft)
        {
            // Already moved left.
        }
        else if (IsInCenter)
        {
        animator.SetTrigger("Turn"); // Trigger the jump animation.
            targetPosition += new Vector3(leftPosition.position.x, 0, 0);
            IsInCenter = false;
            IsMovedToLeft = true;
            IsMovedToRight = false;
            Debug.Log("Left");
        }
    }

    public void MoveToRight()
    {
        if (IsMovedToLeft)
        {
            MoveInit();
        }
        else if (IsMovedToRight)
        {
            // Already moved right.
        }
        else if (IsInCenter)
        {
        animator.SetTrigger("TurnRight"); // Trigger the jump animation.
            targetPosition += new Vector3(rightPosition.position.x, 0, 0);
            IsInCenter = false;
            IsMovedToLeft = false;
            IsMovedToRight = true;
            Debug.Log("Right !");
        }
    }

   public void Slide()
    {
        // Trigger the slide animation.
        animator.SetTrigger("Slide");

        // Calculate the duration of the slide animation (adjust as needed).
        float slideDuration = 0.8f; // 1 second in this example

        // Start a coroutine to stop the slide after the calculated duration.
        StartCoroutine(StopSlideAfterDelay(slideDuration));
    }

    private IEnumerator StopSlideAfterDelay(float delay)
    {
        // Wait for the specified duration.
        yield return new WaitForSeconds(delay);

        // Trigger the stop slide animation.
        animator.SetTrigger("StopSlide");
    }

    public void MoveInit()
    {
        animator.SetTrigger("Turn"); // Trigger the jump animation.
        targetPosition = new Vector3(0f, transform.position.y, transform.position.z);
        IsInCenter = true;
        IsMovedToLeft = false;
        IsMovedToRight = false;
        Debug.Log("Center !");
    }
}
