using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCoinsBySpeed : MonoBehaviour
{
    public GameObject[] Coins;
    

    // Update is called once per frame
    void Update()
    {
        if(Coins.Length >=3)
            if(Mathf.Abs(PlayerControl.instance.speed)<5){
                Coins[0].SetActive(true);
                Coins[1].SetActive(false);
                Coins[2].SetActive(false);
            }
            else if (Mathf.Abs(PlayerControl.instance.speed)<8){
                Coins[0].SetActive(false);
                Coins[2].SetActive(false);
                Coins[1].SetActive(true);
            }
            else{
                Coins[2].SetActive(true);
                Coins[0].SetActive(false);
                Coins[1].SetActive(false);

            }
    }
}
