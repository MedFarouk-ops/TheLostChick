using System.Collections;
using UnityEngine;

public class RandomAdCaller : MonoBehaviour
{
    public float minTimeBetweenAds = 30f; // Minimum time between ad displays (in seconds)
    public float maxTimeBetweenAds = 60f; // Maximum time between ad displays (in seconds)

    private float nextAdTime;

    public float timess;

    private void Start()
    {
        // Set the initial time for displaying the first ad
        SetNextAdTime();
    }

    private void Update()
    {

        timess = Time.time - nextAdTime;
        // Check if it's time to show an ad
        if (Time.time >= nextAdTime)
        {
            // Call the ShowAd method (you can replace this with your ad display logic)
            ShowAd();

            // Set the time for displaying the next ad
            SetNextAdTime();
        }
    }

    private void ShowAd()
    {
        if (AdManager.instance.AdIsReady())
        {
            // Show the interstitial ad
            AdManager.instance.ShowInterstitial();
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

    private void SetNextAdTime()
    {
        // Calculate a random time between minTimeBetweenAds and maxTimeBetweenAds
        nextAdTime = Time.time + Random.Range(minTimeBetweenAds, maxTimeBetweenAds);
    }
}