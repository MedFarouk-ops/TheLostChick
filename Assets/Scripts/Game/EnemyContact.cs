using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContact : MonoBehaviour
{
    [Header("Components")] 
    public AudioSource audioSource;
    public AudioClip audioClip;
    public GameObject explosionObject;
    [Header("Options")]
    public bool useAudio;
    private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                ExplodeAndEndGame();
                PlayerControl.instance.speed = 0f;
            }
        }

        private void ExplodeAndEndGame()
        {
            Vector3 explosionPosition = transform.position;
        
            explosionObject.transform.position = explosionPosition;
            explosionObject.GetComponent<ParticleSystem>().Play();

            if (useAudio)
            {
                    audioSource.clip = audioClip;
                    audioSource.Play();
                    useAudio = false;
            }


            Debug.Log("Died!");
            GameManager.instance.EndGame();
        }
}
