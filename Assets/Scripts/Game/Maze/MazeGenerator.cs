using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private MazeCell _mazeCellPrefab;

    private int _mazeWidth;  // Remove the serialized field
    private int _mazeDepth;  // Remove the serialized field

    [SerializeField]
    private List<GameObject> _obstacles;

    [SerializeField]
    private GameObject _mazeContainer;

    [SerializeField]
    private GameObject _endLevelPrefab;

    public int MazeWidth => _mazeWidth;
    public int MazeDepth => _mazeDepth;

    private MazeCell[,] _mazeGrid;
    private HashSet<Vector2Int> _occupiedCells;

    public static MazeGenerator mzinstance;

    public bool IsMazeGenerated;

    void Awake()
    {
        mzinstance = this;
    }

    void Start()
    {
        // Retrieve maze dimensions from GameManager
        _mazeWidth = GameManager.instance.MazeWidth;
        _mazeDepth = GameManager.instance.MazeDepth;

        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];
        _occupiedCells = new HashSet<Vector2Int>();

        if (_mazeContainer == null)
        {
            _mazeContainer = new GameObject("MazeContainer");
        }

        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int z = 0; z < _mazeDepth; z++)
            {
                MazeCell mazeCell = Instantiate(_mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);
                mazeCell.transform.parent = _mazeContainer.transform;
                _mazeGrid[x, z] = mazeCell;
            }
        }

        GenerateMaze();
    }

    public bool IsCellOccupied(int x, int z)
    {
        return _occupiedCells.Contains(new Vector2Int(x, z));
    }
    private void GenerateMaze()
    {
        // Start generating from the first cell
        GenerateMazeRecursive(_mazeGrid[0, 0]);

        // Clear the entrance and exit walls
        _mazeGrid[0, 0].ClearLeftWall(); // Entrance
        _mazeGrid[_mazeWidth - 1, _mazeDepth - 1].ClearRightWall(); // Exit

        // Place obstacles in the maze
        PlaceObstacles();

        // Instantiate the end level prefab at the end of the maze
        Vector3 endLevelPosition = new Vector3(_mazeWidth + 2, 1, _mazeDepth-2);
        Instantiate(_endLevelPrefab, endLevelPosition, Quaternion.identity);
        IsMazeGenerated = true;
    }


    private void PlaceObstacles()
    {
        List<Vector3> availablePositions = new List<Vector3>();

        // Collect available positions for placing obstacles
        foreach (var cell in _mazeGrid)
        {
            if (!cell.IsVisited)
            {
                availablePositions.Add(cell.transform.position);
            }
        }

        // Place obstacles at available positions
        foreach (var obstaclePrefab in _obstacles)
        {
            if (availablePositions.Count > 0)
            {
                int randomIndex = Random.Range(0, availablePositions.Count);
                Vector3 position = availablePositions[randomIndex];
                Instantiate(obstaclePrefab, position, Quaternion.identity);
                availablePositions.RemoveAt(randomIndex);
            }
            else
            {
                Debug.LogWarning("Ran out of available positions to place obstacles!");
                break;
            }
        }
    }

    public void IncrementMazeDepth(int incrementAmount)
    {
        _mazeDepth += incrementAmount;
        // Additional logic to regenerate or update the maze with the new depth
    }

    private void GenerateMazeRecursive(MazeCell currentCell)
    {
        currentCell.Visit();

        // Shuffle the directions for randomization
        List<Direction> directions = new List<Direction> { Direction.Left, Direction.Right, Direction.Front, Direction.Back };
        directions.Shuffle();

        foreach (var direction in directions)
        {
            int x = (int)currentCell.transform.position.x;
            int z = (int)currentCell.transform.position.z;

            MazeCell nextCell = null;

            switch (direction)
            {
                case Direction.Left:
                    if (x > 0 && !_mazeGrid[x - 1, z].IsVisited)
                    {
                        nextCell = _mazeGrid[x - 1, z];
                        currentCell.ClearLeftWall();
                        nextCell.ClearRightWall();
                    }
                    break;
                case Direction.Right:
                    if (x < _mazeWidth - 1 && !_mazeGrid[x + 1, z].IsVisited)
                    {
                        nextCell = _mazeGrid[x + 1, z];
                        currentCell.ClearRightWall();
                        nextCell.ClearLeftWall();
                    }
                    break;
                case Direction.Front:
                    if (z < _mazeDepth - 1 && !_mazeGrid[x, z + 1].IsVisited)
                    {
                        nextCell = _mazeGrid[x, z + 1];
                        currentCell.ClearFrontWall();
                        nextCell.ClearBackWall();
                    }
                    break;
                case Direction.Back:
                    if (z > 0 && !_mazeGrid[x, z - 1].IsVisited)
                    {
                        nextCell = _mazeGrid[x, z - 1];
                        currentCell.ClearBackWall();
                        nextCell.ClearFrontWall();
                    }
                    break;
            }

            if (nextCell != null)
            {
                GenerateMazeRecursive(nextCell);
            }
        }
    }

     public MazeCell GetMazeCellAtPosition(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x);
        int z = Mathf.RoundToInt(position.z);

        if (x >= 0 && x < _mazeWidth && z >= 0 && z < _mazeDepth)
        {
            return _mazeGrid[x, z];
        }
        else
        {
            Debug.LogWarning("Attempted to access maze cell outside the bounds of the maze.");
            return null;
        }
    }


    private MazeCell GetRandomUnvisitedCell()
    {
        List<MazeCell> unvisitedCells = new List<MazeCell>();
        foreach (var cell in _mazeGrid)
        {
            if (!cell.IsVisited)
            {
                unvisitedCells.Add(cell);
            }
        }

        if (unvisitedCells.Count > 0)
        {
            return unvisitedCells[Random.Range(0, unvisitedCells.Count)];
        }

        return null;
    }

    // Draw Gizmos for better visualization
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(_mazeWidth - 1, 0, _mazeDepth - 1), Vector3.one);
    }
}

public enum Direction
{
    Left,
    Right,
    Front,
    Back
}

public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
