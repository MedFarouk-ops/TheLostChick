using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileManager : MonoBehaviour
{
    public Transform playerStartPosition;
    public GameObject[] players; // Array of player prefabs
    public TMP_Text levelText; // TextMeshPro component to display the current level
    private GameObject currentPlayer; // Reference to the currently spawned player

    private Slider liquidSlider; // Reference to the slider component
    private Image liquidFillImage; // Reference to the fill image component
    private RectTransform sliderRect; // Reference to the slider's RectTransform

    void Start()
    {
        // Create the liquid indicator

        // Get the current level from PlayerPrefs or default to 1 if not set
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        UpdateLevelText(currentLevel); // Update level text with the current level
        SpawnPlayer(playerStartPosition);

        //CreateLiquidIndicator();

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
    }

        private void CreateLiquidIndicator()
{
    // Find the canvas containing the text
    Canvas canvas = levelText.GetComponentInParent<Canvas>();
    if (canvas == null)
    {
        Debug.LogError("No Canvas found for the text component.");
        return;
    }

    // Create a new GameObject for the slider
    GameObject sliderObject = new GameObject("LiquidSlider");
    RectTransform sliderRect = sliderObject.AddComponent<RectTransform>();
    sliderRect.SetParent(canvas.transform, false);

    // Add Slider component
    liquidSlider = sliderObject.AddComponent<Slider>();
    liquidSlider.direction = Slider.Direction.LeftToRight;
    liquidSlider.minValue = 0f; // Set the minimum value of the slider
    liquidSlider.maxValue = 1f; // Set the maximum value of the slider

    // Create fill GameObject as child of the slider
    GameObject fillObject = new GameObject("Fill");
    RectTransform fillRect = fillObject.AddComponent<RectTransform>();
    fillRect.SetParent(sliderRect, false);

    // Add Image component
    liquidFillImage = fillObject.AddComponent<Image>();
    liquidFillImage.color = Color.blue; // Initial color
    liquidFillImage.type = Image.Type.Filled;
    liquidFillImage.fillMethod = Image.FillMethod.Horizontal;
    liquidFillImage.fillAmount = 0f; // Initial fill amount

    // Set fill image as slider's fill
    liquidSlider.fillRect = fillRect;

    // Set slider properties
    sliderRect.anchorMin = new Vector2(0.5f, 0.1f);
    sliderRect.anchorMax = new Vector2(0.5f, 0.1f);
    sliderRect.sizeDelta = new Vector2(200f, 20f); // Adjust size as needed
}



        private void CreateFillImage(GameObject sliderObject)
        {
            // Create fill GameObject as child of the slider
            GameObject fillObject = Instantiate(new GameObject("Fill"), sliderObject.transform);

            // Add Image component
            liquidFillImage = fillObject.AddComponent<Image>();
            liquidFillImage.color = Color.blue; // Initial color
            liquidFillImage.type = Image.Type.Filled;
            liquidFillImage.fillMethod = Image.FillMethod.Horizontal;
            liquidFillImage.fillAmount = 0f; // Initial fill amount

            // Set fill image as slider's fill
            liquidSlider.fillRect = fillObject.GetComponent<RectTransform>();
        }

        private void SetSliderProperties(GameObject sliderObject)
        {
            // Get slider's RectTransform
            sliderRect = sliderObject.GetComponent<RectTransform>();

            // Set position and size of the slider
            sliderRect.anchorMin = new Vector2(0.5f, 0.1f);
            sliderRect.anchorMax = new Vector2(0.5f, 0.1f);
            sliderRect.sizeDelta = new Vector2(200f, 20f); // Adjust size as needed
        }
        // Function to update the liquid fill amount and color
    private void UpdateLiquidFill(float fillAmount)
    {
        liquidSlider.value = fillAmount;

        // Change color based on fill amount
        liquidFillImage.color = Color.Lerp(Color.red, Color.green, fillAmount);
    }


    // Example usage: call this function to update the liquid fill
    private void Update()
    {
        // Example usage: update the liquid fill based on some progress
        float progress = CalculateProgress(); // Example progress calculation
        UpdateLiquidFill(progress);
    }

    // Example function to calculate progress
    private float CalculateProgress()
    {
        // Example: calculate progress based on current level and total levels
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        int totalLevels = 10; // Example total levels
        return (float)currentLevel / totalLevels;
    }
}
