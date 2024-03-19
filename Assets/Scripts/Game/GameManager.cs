using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameType
{
    Easy,
    Hard,
    VeryHard,
    Insane,
    Nightmare,
    Impossible
}

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;
    public Timer timer;

    public GameObject gameOverUI;
    public GameObject completeLevelUI;
    public GameObject ControlGameUI;
    public ScoreTextScript ScoreTextScript;
    public int currentCoins;
    public int newCoins = 0;
    public int textWinAmounta;
    public static GameManager instance;

    public GameObject[] players;
    public Transform PlayerStartPosition;
    public GameObject currentPlayer;
    public static int totalIncrement;

    public int MazeWidth => _mazeWidth;
    public int MazeDepth => _mazeDepth;

    public static int depthIncreasAmount = 1;

    public static int _mazeWidth = 5;
    public static int _mazeDepth = 4;

    private int currentLevel = 1; // Initialize the current level

    public delegate void WinLevelAction();
    public event WinLevelAction OnWinLevel;


    public static GameType SelectedGameType = GameType.Easy; // Default to Easy game type

    void Awake()
    {
        instance = this;
        // Load the totalIncrement value from PlayerPrefs
        totalIncrement = PlayerPrefs.GetInt("TotalIncrement");
    }

    // Method to set the selected game type
    public void SetGameType(GameType gameType)
    {
        SelectedGameType = gameType;
        PlayerPrefs.SetInt("SelectedGameType", (int)gameType); // Save selected game type to PlayerPrefs
    }


	
    void Start()
    {
        // Fetch the selected game type from PlayerPrefs
        SelectedGameType = (GameType)PlayerPrefs.GetInt("SelectedGameType", (int)GameType.Easy);

        // Adjust maze parameters based on the selected game type
        switch (SelectedGameType)
        {
            case GameType.Easy:
                _mazeDepth = 4;
                timer.AddTime(30f);
                break;
            case GameType.Hard:
                _mazeDepth = 6;
                timer.AddTime(60f);

                break;
            case GameType.VeryHard:
                _mazeDepth = 8;
                timer.AddTime(90f);

                break;
            case GameType.Insane:
                _mazeDepth = 10;
                timer.AddTime(120f);

                break;
            case GameType.Nightmare:
                _mazeDepth = 15;
                timer.AddTime(120f);

                break;
            case GameType.Impossible:
                _mazeDepth = 50;
                timer.AddTime(480f);

                break;
        }
		

        SpawnPlayer(PlayerStartPosition);
        ScoreTextScript.ResetScore();
        GameIsOver = false;
        AdManager.instance.RequestInterstitial();
        currentCoins = PlayerPrefs.GetInt("Coins");
        // Increment maze parameters based on totalIncrement
        IncrementMazeParams(totalIncrement);
    }

    // Rest of the GameManager class methods...

    private void IncrementMazeParams(int incrementAmount)
    {
        _mazeWidth += incrementAmount;

        // // Adjust the maze depth based on the selected game type
        // switch (SelectedGameType)
        // {
        //     case GameType.Easy:
        //         _mazeDepth = 4;
        //         break;
        //     case GameType.Hard:
        //         _mazeDepth = 6;
        //         break;
        //     case GameType.VeryHard:
        //         _mazeDepth = 8;
        //         break;
        //     case GameType.Insane:
        //         _mazeDepth = 10;
        //         break;
        //     case GameType.Nightmare:
        //         _mazeDepth = 15;
        //         break;
        //     case GameType.Impossible:
        //         _mazeDepth = 25;
        //         break;
        // }
    }

    private void SpawnPlayer(Transform PlayerStartPosition)
    {
        if (PlayerPrefs.GetInt("Chick1Selected") == 1)
        {
            currentPlayer = Instantiate(players[0], PlayerStartPosition.position, Quaternion.identity);
        }
        else if (PlayerPrefs.GetInt("Chick2Selected") == 1)
        {
            currentPlayer = Instantiate(players[1], PlayerStartPosition.position, Quaternion.identity);
        }
        else if (PlayerPrefs.GetInt("Chick3Selected") == 1)
        {
            currentPlayer = Instantiate(players[2], PlayerStartPosition.position, Quaternion.identity);
        }
        else if (PlayerPrefs.GetInt("Chick4Selected") == 1)
        {
            currentPlayer = Instantiate(players[3], PlayerStartPosition.position, Quaternion.identity);
        }
        else
        {
            currentPlayer = Instantiate(players[0], PlayerStartPosition.position, Quaternion.identity);
        }

        Camera.main.GetComponent<CameraFollowSimple>().SetTarget(currentPlayer.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameIsOver)
            return;
    
        // Update the timer
        timer.UpdateTimer(Time.deltaTime);

        // Check if the timer has reached zero
        if (timer.GetTimeRemaining() <= 0)
        {
            LoseGame(); // Call the LoseGame method when the timer is up
        }
    }
    public void EndGame()
    {
        newCoins = currentCoins + ScoreTextScript.coinAmount;
        PlayerPrefs.SetInt("Coins", newCoins);
        GameIsOver = true;
        gameOverUI.SetActive(true);
        ControlGameUI.SetActive(false);
        LoadNextLevel();
    }

	public void LoseGame()
    {
        // End the game
        GameIsOver = true;
        gameOverUI.SetActive(true);
        ControlGameUI.SetActive(false);
        // You may add additional logic here such as reducing player lives, displaying a game over message, etc.
    }

    public void WinLevel()
	{
		int moneyGained = 0;
        newCoins = currentCoins;
		switch (SelectedGameType)
		{
			case GameType.Easy:
				moneyGained = 500;
				break;
			case GameType.Hard:
				moneyGained = 1000;
				break;
			case GameType.VeryHard:
				moneyGained = 1500;
				break;
			case GameType.Insane:
				moneyGained = 2000;
				break;
			case GameType.Nightmare:
				moneyGained = 2500;
				break;
			case GameType.Impossible:
				moneyGained = 3000;
				break;
		}
        timer.AddTime(200f);
        textWinAmounta = moneyGained;
		newCoins = currentCoins + moneyGained;
		PlayerPrefs.SetInt("Coins", newCoins);
		// Save the current level progress
		PlayerPrefs.SetInt("CurrentLevel", currentLevel + 1);
		// Increment totalIncrement and save it
		totalIncrement += 1;
		PlayerPrefs.SetInt("TotalIncrement", totalIncrement);
		IncrementMazeParams(totalIncrement);
		// Load the next level
        LoadNextLevel();
        
	}


    public void ContinueFromAds()
    {
        gameOverUI.SetActive(false);
        timer.AddTime(20f);
    }

    public void WatchAdsRewarded(){
        currentCoins = PlayerPrefs.GetInt("Coins");
        int anewCoins = currentCoins + 200;
        PlayerPrefs.SetInt("Coins", anewCoins);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CompleteLevel()
    {
        int moneyGained = 0;
		switch (SelectedGameType)
		{
			case GameType.Easy:
				moneyGained = 500;
				break;
			case GameType.Hard:
				moneyGained = 1000;
				break;
			case GameType.VeryHard:
				moneyGained = 1500;
				break;
			case GameType.Insane:
				moneyGained = 2000;
				break;
			case GameType.Nightmare:
				moneyGained = 2500;
				break;
			case GameType.Impossible:
				moneyGained = 3000;
				break;
		}
        textWinAmounta = moneyGained;
        newCoins = currentCoins + moneyGained;
		PlayerPrefs.SetInt("Coins", newCoins);
        OnWinLevel?.Invoke();
        completeLevelUI.SetActive(true);
    }

   
}