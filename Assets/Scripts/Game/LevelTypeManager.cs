using UnityEngine;
using UnityEngine.UI;

public class LevelTypeManager : MonoBehaviour
{
    // Reference to the buttons for selecting game types
    public Button easyButton;
    public Button hardButton;
    public Button veryHardButton;
    public Button insaneButton;
    public Button impossibleButton; // Adding the missing button

    private Color selectedColor = Color.green;
    private Color deselectedColor = Color.yellow;

    // Start is called before the first frame update
    void Start()
    {
        // Load the selected game type from PlayerPrefs
        GameType selectedGameType = (GameType)PlayerPrefs.GetInt("SelectedGameType", (int)GameType.Easy);
        SetButtonColors(selectedGameType);

        // Add listeners to the buttons
        easyButton.onClick.AddListener(() => SelectGameType(GameType.Easy));
        hardButton.onClick.AddListener(() => SelectGameType(GameType.Hard));
        veryHardButton.onClick.AddListener(() => SelectGameType(GameType.VeryHard));
        insaneButton.onClick.AddListener(() => SelectGameType(GameType.Insane));
        impossibleButton.onClick.AddListener(() => SelectGameType(GameType.Impossible)); // Adding listener for the new button
    }

    // Method to handle selecting a game type
    void SelectGameType(GameType gameType)
    {
        // Save the selected game type to PlayerPrefs
        PlayerPrefs.SetInt("SelectedGameType", (int)gameType);

        // Set button colors based on the selected game type
        SetButtonColors(gameType);

        // Set the game type in the GameManager
        GameManager.instance.SetGameType(gameType);
    }

    // Method to set button colors based on the selected game type
    void SetButtonColors(GameType selectedGameType)
    {
        easyButton.image.color = selectedGameType == GameType.Easy ? selectedColor : deselectedColor;
        hardButton.image.color = selectedGameType == GameType.Hard ? selectedColor : deselectedColor;
        veryHardButton.image.color = selectedGameType == GameType.VeryHard ? selectedColor : deselectedColor;
        insaneButton.image.color = selectedGameType == GameType.Insane ? selectedColor : deselectedColor;
        impossibleButton.image.color = selectedGameType == GameType.Impossible ? selectedColor : deselectedColor; // Setting color for the new button
    }
}
