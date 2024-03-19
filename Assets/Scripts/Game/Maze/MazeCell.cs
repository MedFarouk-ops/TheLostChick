using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    [SerializeField] private GameObject _leftWall;
    [SerializeField] private GameObject _rightWall;
    [SerializeField] private GameObject _frontWall;
    [SerializeField] private GameObject _backWall;
    [SerializeField] private GameObject _unvisitedBlock;

    private float wallOffset = 0.5f; // Offset to ensure objects do not intersect with walls

    public bool IsVisited { get; private set; }

    public void Visit()
    {
        IsVisited = true;
        _unvisitedBlock.SetActive(false);
    }

    public void ClearLeftWall()
    {
        _leftWall.SetActive(false);
    }

    public void ClearRightWall()
    {
        _rightWall.SetActive(false);
    }

    public void ClearFrontWall()
    {
        _frontWall.SetActive(false);
    }

    public void ClearBackWall()
    {
        _backWall.SetActive(false);
    }

    public Vector3 GetCellPosition()
    {
        return transform.position;
    }

    public Vector3 GetLeftWallPosition()
    {
        return _leftWall.transform.position;
    }

    public Vector3 GetRightWallPosition()
    {
        return _rightWall.transform.position;
    }

    public Vector3 GetFrontWallPosition()
    {
        return _frontWall.transform.position;
    }

    public Vector3 GetBackWallPosition()
    {
        return _backWall.transform.position;
    }

    public Vector3 GetAdjustedObjectPosition(Vector3 objectPosition)
    {
        // Adjust the object position to ensure it does not intersect with walls
        Vector3 cellPosition = GetCellPosition();
        Vector3 leftWallPosition = GetLeftWallPosition();
        Vector3 rightWallPosition = GetRightWallPosition();
        Vector3 frontWallPosition = GetFrontWallPosition();
        Vector3 backWallPosition = GetBackWallPosition();

        float xMin = leftWallPosition.x + wallOffset;
        float xMax = rightWallPosition.x - wallOffset;
        float zMin = backWallPosition.z + wallOffset;
        float zMax = frontWallPosition.z - wallOffset;

        float x = Mathf.Clamp(objectPosition.x, xMin, xMax);
        float z = Mathf.Clamp(objectPosition.z, zMin, zMax);

        return new Vector3(x, objectPosition.y, z);
    }
}
