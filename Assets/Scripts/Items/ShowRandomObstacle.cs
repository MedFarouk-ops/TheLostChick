using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRandomObstacle : MonoBehaviour
{
    public GameObject[] Obstacles;

    // Start is called before the first frame update
    void Start()
    {
        if (Obstacles.Length > 0)
        {
            ShowRandomObstacleMethod();
        }
    }

    void ShowRandomObstacleMethod()
    {
        int randomIndex = Random.Range(0, Obstacles.Length);
        GameObject randomObstacle = Obstacles[randomIndex];
        randomObstacle.SetActive(true);
    }
}
