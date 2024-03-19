using UnityEngine;
using UnityEngine.SceneManagement;
//using GoogleMobileAds.Api;


public class MainMenu : MonoBehaviour {

	public string levelToLoad = "MainLevel";

	public string mainMenu = "MainMenu" ; 
	
	public string helpPage = "HelpPage" ;
	public string controlMenu = "ControlSelection";
	public string shopPage = "Shop" ;
	public string settingPage = "OptionMenu" ;

	public string freecitymode = "FreeCity";

	public string Graffics = "Settings";



	public SceneFader sceneFader;
	
	

	 string currentLevel ;

    void Start(){
		//AdManager.instance.RequestInterstitial();
        currentLevel = SceneManager.GetActiveScene().name;
    }

	public void Retry ()
	{
		sceneFader.FadeTo(currentLevel);
    }
	
	public void Play ()
	{
		sceneFader.FadeTo(levelToLoad);
	}

	public void FreeCity ()
	{
		sceneFader.FadeTo(freecitymode);
	}

    public void Desert ()
	{
		sceneFader.FadeTo("Desert");
	}

    public void Ice ()
	{
		sceneFader.FadeTo("Ice");
	}

    public void Forest ()
	{
		sceneFader.FadeTo("Forest");
	}

    public void AllInOne ()
	{
		sceneFader.FadeTo("AllSeasons");
	}

	public void GoToOptionPage ()
	{
		sceneFader.FadeTo(settingPage);
	}
	public void GoToControlSelectionPage ()
	{
		sceneFader.FadeTo(controlMenu);
	}
	public void GoToGrafficOptionPage ()
	{
		sceneFader.FadeTo(Graffics);
	}

	public void retunrToMainMenue(){
		sceneFader.FadeTo(mainMenu);
	}
	
	public void goTohelpPage(){
		sceneFader.FadeTo(helpPage);
	}

	public void goToShopPage(){
		sceneFader.FadeTo(shopPage);
	}


	
	public void Quit ()
	{
		Debug.Log("Exciting...");
		Application.Quit();
	}


    public void RateFiveStars()
    {
        #if UNITY_ANDROID
            string packageName = Application.identifier;
            Application.OpenURL("market://details?id=" + packageName + "&reviewId=0");
        #endif
		if(PlayerPrefs.GetInt("Rated") != 1){
			int currentCoins = PlayerPrefs.GetInt("Coins", 0);
			int newCoins = currentCoins +500;
			PlayerPrefs.SetInt("Coins", newCoins);
			PlayerPrefs.SetInt("Rated", 1);
		}
    }

	public void WatchAdsAndContinue(){
		
        if (AdManager.instance.AdIsReady())
        {
            // Show the interstitial ad
            AdManager.instance.ShowInterstitial();

            // Add reward to the player's coins
            GameManager.instance.ContinueFromAds();
        }
        else
        {
			if (!AdManager.instance.AdIsReady())
			{
				AdManager.instance.RequestInterstitial();
			}
            Debug.LogWarning("Interstitial ad is not ready yet.");
        }
			
	}


	public void WatchAdsRewarded(){
		
        if (AdManager.instance.AdIsReady())
        {
            // Show the interstitial ad
            AdManager.instance.ShowInterstitial();

            // Add reward to the player's coins
            GameManager.instance.WatchAdsRewarded();
        }
        else
        {
			if (!AdManager.instance.AdIsReady())
			{
				AdManager.instance.RequestInterstitial();
			}
            Debug.LogWarning("Interstitial ad is not ready yet.");
        }
			
	}

}