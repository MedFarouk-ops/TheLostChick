using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCollect : MonoBehaviour
{   
    [Header("Components")] 
    public AudioSource audioSource;
    public AudioClip audioClip;
    
    [Header("Options")]
    public bool useAudio;

 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.timer.AddTime(3f); // Add 5 seconds to the timer
             // Update the total coin amount
            Debug.Log("Collected Time!");      	    
            
            if (useAudio)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
            }
            
            Destroy(gameObject, 0.1f);
            
        }
    }
}
