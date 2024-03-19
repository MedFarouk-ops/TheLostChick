using UnityEngine;

public class SpinObject : MonoBehaviour
{
    public float spinSpeed = 180f;

    void Update()
    {
        // Spin the object around its Y-axis
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }
}
