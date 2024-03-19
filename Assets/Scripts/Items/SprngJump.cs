using UnityEngine;

public class SprngJump : MonoBehaviour
{
    public float jumpForce = 100f; // Adjust the jump force as needed

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
                // Apply an upward force to the player
                PlayerControl.instance.rb.velocity = new Vector3(PlayerControl.instance.rb.velocity.x, 0, PlayerControl.instance.rb.velocity.z); // Zero out the vertical velocity.
                PlayerControl.instance.rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
