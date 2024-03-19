using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareCoin : MonoBehaviour
{
    [Header("Components")] 
    public AudioSource audioSource;
    public AudioClip audioClip;
    
    [Header("Options")]
    public bool useAudio;

    int coinAmount = 0;
    void Start(){
        coinAmount = PlayerPrefs.GetInt("RareCoins", 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ScoreTextScript.instance.coinAmount ++ ;
            coinAmount+=ScoreTextScript.instance.coinAmount;
            Debug.Log("Collected!");      	    
	    if (useAudio)
	    {
	    	audioSource.clip = audioClip;
            	audioSource.Play();
	    }
            Destroy(gameObject,0.1f);
            PlayerPrefs.SetInt("RareCoins", coinAmount);

        }
    }
}

