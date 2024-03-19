using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCountText : MonoBehaviour
{
    [SerializeField] private int totalCoins = 0;

    private int coinsCollected = 0;

    // private TextMeshProUGUI coinsCollectedText;

    private void Awake() 
    {
        // coinsCollectedText = this.GetComponent<TextMeshProUGUI>();
    }

    private void Start() 
    {
        // subscribe to events
        // GameEventsManager.instance.onCoinCollected += OnCoinCollected;
    }

    private void OnDestroy() 
    {
        // unsubscribe from events
        // GameEventsManager.instance.onCoinCollected -= OnCoinCollected;
    }

    private void OnCoinCollected() 
    {
        coinsCollected++;
    }


}
