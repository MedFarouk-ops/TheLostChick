using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isRaining = false;
    private int rainDensity = 20; // Number of raindrops spawned per frame
    private float rainForce = 1f; // Force applied to raindrops
    private float rainLifetime = 4f; // Lifetime of raindrops
    private float timeUntilPuddle = 2f; // Time until water puddles appear
    private bool puddlesSpawned = false; // Flag to track if water puddles have been spawned
    private List<GameObject> raindrops = new List<GameObject>(); // List to store raindrop objects
    private List<GameObject> clouds = new List<GameObject>(); // List to store cloud objects
    private GameObject weatherParent; // Parent object for weather effects

    public Transform playerPosition;
    private float weatherLength;
    private float weatherWidth;

    public Material cloudMaterial; // Material for the clouds

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        weatherWidth = (float)(GameManager.instance.MazeDepth - 10);
        weatherLength = (float)(GameManager.instance.MazeWidth - 2);
        weatherParent = new GameObject("Weather"); // Create a parent object for weather effects
        // Start with random weather
        RandomizeWeather();
        // SpawnClouds();

    }

    void Update()
    {
        // Check if weather needs to change (e.g., based on level progress)
        // For demonstration, we'll just change the weather randomly every 20 seconds
        if (Time.timeSinceLevelLoad % 20f == 0f)
        {
            RandomizeWeather();
        }

        // Check if it's time to spawn water puddles
        if (isRaining && !puddlesSpawned && Time.timeSinceLevelLoad >= timeUntilPuddle)
        {
            SpawnLakes();
            puddlesSpawned = true;
        }
    }

   void RandomizeWeather()
    {
        // Stop any existing rain
        StopRain();

        float rand = Random.Range(0f, 1f);
        // Randomly decide whether it will rain, snow, or be clear
        if (rand < 0.33f)
            StartRain();
        else if (rand < 0.66f)
            StartSnow();
        // If rand >= 0.66f, do nothing (clear weather)
    }

    void StopRain()
    {
    StopAllCoroutines();
}


    void StartRain()
    {
        StopAllCoroutines(); // Stop any existing weather coroutine
        isRaining = true;

        StartCoroutine(RainCoroutine());

        // Disable rain shadows
        Shader.SetGlobalFloat("_GlobalRainIntensity", 0f);

        // Spawn clouds
        SpawnClouds();
    }

    void StartSnow()
    {
        StopAllCoroutines(); // Stop any existing weather coroutine
        isRaining = true;

        StartCoroutine(SnowCoroutine());
    }

    IEnumerator RainCoroutine()
    {
        // Ensure that it's raining before starting the rain coroutine
        if (!isRaining)
            yield break;

        while (true) // Infinite loop
        {
            for (int i = 0; i < rainDensity; i++)
            {
                // Create raindrop
                GameObject raindrop = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                raindrop.transform.parent = weatherParent.transform; // Set parent object
                Vector3 spawnPosition = new Vector3(Random.Range(-weatherLength, weatherLength), 10f, Random.Range(-weatherWidth, weatherWidth)); // Set spawn position
                raindrop.transform.position = spawnPosition;
                raindrop.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f); // Set scale
                Rigidbody rb = raindrop.AddComponent<Rigidbody>(); // Add Rigidbody component
                rb.useGravity = true; // Enable gravity
                Material rainMaterial = new Material(Shader.Find("Standard"));
                rainMaterial.color = new Color(0.6f, 0.5f, 1f, 0.5f); // Blue color with transparency
                raindrop.GetComponent<Renderer>().material = rainMaterial;
                rb.AddForce(Vector3.down * rainForce, ForceMode.Impulse); // Apply downward force
                Destroy(raindrop, rainLifetime); // Destroy raindrops after a delay
                raindrops.Add(raindrop); // Add raindrop to the list
            }

            // Wait for the specified interval before the next iteration
            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator SnowCoroutine()
    {
        if (!isRaining)
            yield break;

        while (true) // Infinite loop
        {
            for (int i = 0; i < rainDensity; i++)
            {
                // Create raindrop
                GameObject raindrop = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                raindrop.transform.parent = weatherParent.transform; // Set parent object
                Vector3 spawnPosition = new Vector3(Random.Range(-weatherLength, weatherLength), 10f, Random.Range(-weatherWidth, weatherWidth)); // Set spawn position
                raindrop.transform.position = spawnPosition;
                raindrop.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f); // Set scale
                Rigidbody rb = raindrop.AddComponent<Rigidbody>(); // Add Rigidbody component
                rb.useGravity = true; // Enable gravity
                Material rainMaterial = new Material(Shader.Find("Standard"));
                rainMaterial.color = new Color(1f, 1f, 1f, 0.5f); // Blue color with transparency
                raindrop.GetComponent<Renderer>().material = rainMaterial;
                rb.AddForce(Vector3.down * rainForce, ForceMode.Impulse); // Apply downward force
                Destroy(raindrop, rainLifetime); // Destroy raindrops after a delay
                raindrops.Add(raindrop); // Add raindrop to the list
            }

            // Wait for the specified interval before the next iteration
            yield return new WaitForSeconds(0.02f);
        }
    }

    void SpawnLakes()
    {
        // Create a parent GameObject for the lakes if it doesn't exist
        GameObject lakesParent = GameObject.Find("LakesParent");
        if (lakesParent == null)
        {
            lakesParent = new GameObject("LakesParent");
        }

        foreach (GameObject raindrop in raindrops)
        {
            Vector3 lakePosition = new Vector3(raindrop.transform.position.x, playerPosition.position.y, raindrop.transform.position.z); // Place lake on the ground

            // Create a new game object for the lake
            GameObject lake = new GameObject("Lake");
            lake.transform.parent = lakesParent.transform; // Set parent object to the lakesParent
            lake.transform.position = lakePosition;

            // Generate the mesh for the lake surface
            MeshFilter meshFilter = lake.AddComponent<MeshFilter>();
            meshFilter.mesh = GenerateLakeMesh();

            // Set water color to blue and transparent
            Material lakeMaterial = new Material(Shader.Find("Standard"));
            lakeMaterial.color = new Color(0f, 0f, 1f, 0.5f); // Blue color with transparency
            lake.GetComponent<Renderer>().material = lakeMaterial;

            Destroy(lake, 30f); // Destroy the lake after a delay
        }
    }


    Mesh GenerateLakeMesh()
    {
        Mesh mesh = new Mesh();

        // Define vertices for the lake surface (a simple plane)
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(-0.5f, 0f, -0.5f),
            new Vector3(0.5f, 0f, -0.5f),
            new Vector3(0.5f, 0f, 0.5f),
            new Vector3(-0.5f, 0f, 0.5f)
        };

        // Define triangles for the lake surface
        int[] triangles = new int[]
        {
            0, 1, 2,
            0, 2, 3
        };

        // Define normals for the lake surface (all pointing up)
        Vector3[] normals = new Vector3[]
        {
            Vector3.up,
            Vector3.up,
            Vector3.up,
            Vector3.up
        };

        // Assign vertices, triangles, and normals to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;

        return mesh;
    }


    void SpawnClouds()
    {
        // Spawn clouds in the sky
        for (int i = 0; i < 5; i++)
        {
            Vector3 cloudPosition = new Vector3(Random.Range(-8f, 8f), Random.Range(7f, 15f), Random.Range(-8f, 8f)); // Set spawn position
            GameObject cloud = CreateCloud(cloudPosition); // Create cloud at the specified position
            clouds.Add(cloud); // Add cloud to the list
        }
    }

    GameObject CreateCloud(Vector3 position)
    {
        // Create a new cloud game object
        GameObject cloud = new GameObject("Cloud");
        cloud.transform.parent = weatherParent.transform; // Set parent object
        cloud.transform.position = position;

        // Add a mesh filter component
        MeshFilter meshFilter = cloud.AddComponent<MeshFilter>();
        meshFilter.mesh = GenerateCloudMesh(); // Generate cloud mesh

        // Add a mesh renderer component
        MeshRenderer meshRenderer = cloud.AddComponent<MeshRenderer>();
        meshRenderer.material = cloudMaterial; // Set cloud material

        // Add CloudMovement script to enable cloud movement
        CloudMovement cloudMovement = cloud.AddComponent<CloudMovement>();

        return cloud;
    }



   Mesh GenerateCloudMesh()
{
    Mesh mesh = new Mesh();

    // Define the number of divisions for the sphere
    int divisions = 16;

    // Calculate the number of vertices and triangles
    int vertexCount = (divisions + 1) * (divisions + 1);
    int triangleCount = divisions * divisions * 6;

    // Create arrays to hold vertices, triangles, and normals
    Vector3[] vertices = new Vector3[vertexCount];
    int[] triangles = new int[triangleCount];
    Vector3[] normals = new Vector3[vertexCount];

    // Generate vertices for the sphere
    int index = 0;
    float step = 1.0f / divisions;
    for (int i = 0; i <= divisions; i++)
    {
        for (int j = 0; j <= divisions; j++)
        {
            float u = i * step;
            float v = j * step;

            float theta = Mathf.PI * u;
            float phi = Mathf.PI * 2 * v;

            // Apply Perlin noise to create irregularities in the cloud shape
            float noise = Mathf.PerlinNoise(u * 3f, v * 3f) * 0.5f;

            float x = Mathf.Sin(theta) * Mathf.Cos(phi) + Random.Range(-0.1f, 0.1f) * noise;
            float y = Mathf.Cos(theta) + Random.Range(-0.1f, 0.1f) * noise;
            float z = Mathf.Sin(theta) * Mathf.Sin(phi) + Random.Range(-0.1f, 0.1f) * noise;

            vertices[index] = new Vector3(x, y, z);
            normals[index] = vertices[index].normalized;
            index++;
        }
    }

    // Generate triangles
    index = 0;
    for (int i = 0; i < divisions; i++)
    {
        for (int j = 0; j < divisions; j++)
        {
            int first = i * (divisions + 1) + j;
            int second = first + divisions + 1;

            triangles[index++] = first;
            triangles[index++] = second + 1;
            triangles[index++] = first + 1;

            triangles[index++] = first;
            triangles[index++] = second;
            triangles[index++] = second + 1;
        }
    }

    // Assign vertices, triangles, and normals to the mesh
    mesh.vertices = vertices;
    mesh.triangles = triangles;
    mesh.normals = normals;

    return mesh;
}




}

