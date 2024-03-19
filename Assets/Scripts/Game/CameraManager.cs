using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public List<GameObject> Cameras;
    private int currentCameraIndex = 0;

    private void Start()
    {
        // Ensure at least one camera is available and activate it at the start.
        if (Cameras.Count > 0)
        {
            ActivateCamera(currentCameraIndex);
        }
    }

    public void SwitchCamera()
    {
        // Deactivate the current camera.
        Cameras[currentCameraIndex].SetActive(false);

        // Increment the camera index or loop back to the first camera if necessary.
        currentCameraIndex = (currentCameraIndex + 1) % Cameras.Count;

        // Activate the new current camera.
        ActivateCamera(currentCameraIndex);

        // Set the target for the follow script of the active camera
        SetCameraTarget(currentCameraIndex);
    }

    private void ActivateCamera(int index)
    {
        if (index >= 0 && index < Cameras.Count)
        {
            Cameras[index].SetActive(true);
        }
    }

    private void SetCameraTarget(int index)
    {
        if (index >= 0 && index < Cameras.Count)
        {
            CameraFollowSimple followScript = Cameras[index].GetComponent<CameraFollowSimple>();
            if (followScript != null)
            {
                GameObject currentPlayer = GameManager.instance.currentPlayer;
                if (currentPlayer != null)
                {
                    followScript.SetTarget(currentPlayer.transform);
                }
                else
                {
                    Debug.LogWarning("No current player found.");
                }
            }
            else
            {
                Debug.LogWarning("Camera does not have CameraFollowSimple script attached.");
            }
        }
    }
}
