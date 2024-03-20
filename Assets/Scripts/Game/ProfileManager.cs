using UnityEngine;
using TMPro;

public class ProfileManager : MonoBehaviour
{
    public Transform playerStartPosition;
    public GameObject[] players; // Array of player prefabs
    public TMP_Text levelText; // TextMeshPro component to display the current level

    private GameObject currentPlayer; // Reference to the currently spawned player

    void Start()
    {
        // Get the current level from PlayerPrefs or default to 1 if not set
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        UpdateLevelText(currentLevel); // Update level text with the current level
        SpawnPlayer(playerStartPosition);
    }

    private void UpdateLevelText(int level)
    {
        levelText.text = "Level " + level.ToString(); // Update text to display current level
    }

    public void IncreaseLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1); // Get the current level from PlayerPrefs
        currentLevel++; // Increase the level
        PlayerPrefs.SetInt("CurrentLevel", currentLevel); // Store the updated level in PlayerPrefs
        PlayerPrefs.Save(); // Save the PlayerPrefs
        UpdateLevelText(currentLevel); // Update level text
    }

    private void SpawnPlayer(Transform playerStartPosition)
    {
        if (PlayerPrefs.GetInt("Chick1Selected") == 1)
        {
            currentPlayer = Instantiate(players[0], playerStartPosition.position, Quaternion.identity);
        }
        else if (PlayerPrefs.GetInt("Chick2Selected") == 1)
        {
            currentPlayer = Instantiate(players[1], playerStartPosition.position, Quaternion.identity);
        }
        else if (PlayerPrefs.GetInt("Chick3Selected") == 1)
        {
            currentPlayer = Instantiate(players[2], playerStartPosition.position, Quaternion.identity);
        }
        else if (PlayerPrefs.GetInt("Chick4Selected") == 1)
        {
            currentPlayer = Instantiate(players[3], playerStartPosition.position, Quaternion.identity);
        }
        else
        {
            currentPlayer = Instantiate(players[0], playerStartPosition.position, Quaternion.identity);
        }

          // Set the scale of the player to match the size of the position GameObject
    // Vector3 scaleDifference = playerStartPosition.lossyScale / currentPlayer.transform.localScale;
    // currentPlayer.transform.localScale = Vector3.Scale(currentPlayer.transform.localScale, scaleDifference);

        // Camera.main.GetComponent<CameraFollowSimple>().SetTarget(currentPlayer.transform);
    }
}
