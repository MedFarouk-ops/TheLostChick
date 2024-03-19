using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI counterText;
    float totalTime; // Total time for the countdown
    float currentTime; // Current time remaining

    void Start()
    {
        counterText = GetComponent<TextMeshProUGUI>();
        Initialize(0); // Initialize the timer with 0 seconds initially
    }

    void Update()
    {
        if (currentTime > 0)
        {
            UpdateTimer(Time.deltaTime);
            UpdateUIText();
        }
        else
        {
            counterText.text = "00:00"; // Display 00:00 when time is up
        }
    }

    // Method to initialize the timer with a specified duration in seconds
    public void Initialize(float duration)
    {
        totalTime = duration;
        currentTime = totalTime;
    }

    // Method to update the timer
    public void UpdateTimer(float deltaTime)
    {
        if (currentTime > 0)
        {
            currentTime -= deltaTime;
            if (currentTime < 0)
            {
                currentTime = 0;
            }
        }
    }

    // Method to update the UI text to display the current time
    void UpdateUIText()
    {
        int minutes = (int)(currentTime / 60f);
        int seconds = (int)(currentTime % 60f);
        counterText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    // Method to add time to the timer
    public void AddTime(float timeToAdd)
    {
        currentTime += timeToAdd;
    }

    // Method to get the remaining time
    public float GetTimeRemaining()
    {
        return currentTime;
    }

    // Other methods as needed...
}
