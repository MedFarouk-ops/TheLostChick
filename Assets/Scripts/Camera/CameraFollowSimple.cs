using UnityEngine;

public class CameraFollowSimple : MonoBehaviour
{
    [SerializeField]
    private float _smoothSpeed = 0.125f; // Smoothing factor for camera movement

    [SerializeField]
    private Vector3 _offset; // Offset from the target position

    [SerializeField]
    private float _minXPosition; // Minimum X position for the camera

    private Transform _target; // Reference to the player's transform

    public void SetTarget(Transform target)
    {
        _target = target;
    }

        void LateUpdate()
        {
            if (_target == null)
                return;

            // Calculate the desired position for the camera
            Vector3 desiredPosition = _target.position + _offset;

            // Ensure the camera doesn't go beyond the minimum X position
            if (desiredPosition.x < _minXPosition)
            {
                desiredPosition.x = _minXPosition;
            }

            // Interpolate between the current position and the desired position with smoothing
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);

            // Update the camera's position
            transform.position = new Vector3(smoothedPosition.x, transform.position.y, smoothedPosition.z);
        }

}
