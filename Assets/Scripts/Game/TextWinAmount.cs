using TMPro;
using UnityEngine;

public class TextWinAmount : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;

    private void Start()
    {
        // Get the TextMeshPro component
        textMeshPro = GetComponent<TextMeshProUGUI>();

        // Subscribe to the GameManager's WinLevel event
        GameManager.instance.OnWinLevel += UpdateWinAmountText;

        // Update the win amount text initially
        UpdateWinAmountText();
    }

    private void OnDestroy()
    {
        // Unsubscribe from the GameManager's WinLevel event to prevent memory leaks
        if (GameManager.instance != null)
            GameManager.instance.OnWinLevel -= UpdateWinAmountText;
    }

    private void UpdateWinAmountText()
    {
        // Update the win amount text with the value from GameManager
        textMeshPro.text = "+" + GameManager.instance.textWinAmounta.ToString();
    }
}
