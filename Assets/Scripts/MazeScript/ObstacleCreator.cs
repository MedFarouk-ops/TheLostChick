using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCreator : MonoBehaviour
{
    [SerializeField] private MazeGenerator _mazeGenerator; // Reference to the MazeGenerator script
    [SerializeField] private List<GameObject> _obstaclePrefabs; // List of obstacle prefabs to be randomly placed
    [SerializeField] private float _obstaclePlacementChance = 0.5f; // Chance of placing an obstacle in each cell

    void Start()
    {
        if (_mazeGenerator == null)
        {
            Debug.LogError("MazeGenerator reference is not set!");
            return;
        }

        StartCoroutine(CreateObstaclesAfterMazeGeneration());
    }

    IEnumerator CreateObstaclesAfterMazeGeneration()
    {
        // Wait until the maze generation is complete
        yield return new WaitUntil(() => _mazeGenerator.IsMazeGenerated);

        // Loop through all cells in the maze
        for (int x = 0; x < _mazeGenerator.MazeWidth; x++)
        {
            for (int z = 0; z < _mazeGenerator.MazeDepth; z++)
            {
                // Check if an obstacle should be placed in this cell based on the chance
                if (Random.value < _obstaclePlacementChance)
                {
                    // Get the position of the current cell
                    Vector3 cellPosition = new Vector3(x, 0, z);

                    // Check if the cell is not the start or end cell
                    if (!IsStartOrEndCell(cellPosition))
                    {
                        // Randomly select an obstacle prefab from the list
                        GameObject obstaclePrefab = _obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Count)];

                        // Get the MazeCell component of the current cell
                        MazeCell mazeCell = _mazeGenerator.GetMazeCellAtPosition(cellPosition);

                        // Adjust the obstacle position to ensure it does not intersect with walls
                        Vector3 adjustedPosition = mazeCell.GetAdjustedObjectPosition(cellPosition);

                        // Instantiate the obstacle at the adjusted position
                        Instantiate(obstaclePrefab, adjustedPosition, Quaternion.identity);
                    }
                }
            }
        }
    }

    // Function to check if the given position is the start or end cell position
    private bool IsStartOrEndCell(Vector3 position)
    {
        Vector3 startPosition = new Vector3(0, 0, 0);
        Vector3 endPosition = new Vector3(_mazeGenerator.MazeWidth - 1, 0, _mazeGenerator.MazeDepth - 1);

        return position == startPosition || position == endPosition;
    }
}
