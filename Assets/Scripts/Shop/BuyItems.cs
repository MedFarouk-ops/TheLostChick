using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItems : MonoBehaviour
{
	public int currentCoins;

    public int Chicken2Price = 5000;
    public int Chicken3Price = 10000;
    public int Chicken4Price = 25000;

    int newCoins =0;
	public GameObject NoBatteries; 
    public Button BuyChicken2Btn;
    public Button BuyChicken3Btn;
    public Button BuyChicken4Btn;

    public Button SelectChicken1Btn;
    public Button SelectChicken2Btn;
    public Button SelectChicken3Btn;
    public Button SelectChicken4Btn;

    public GameObject SelectChicken1Box;
    public GameObject SelectChicken2Box;
    public GameObject SelectChicken3Box;
    public GameObject SelectChicken4Box;


    // Start is called before the first frame update
    void Start()
    {
		currentCoins = PlayerPrefs.GetInt("Coins", 0);
        

        if(PlayerPrefs.GetInt("Chick2") == 1){
            if(BuyChicken2Btn != null){
                BuyChicken2Btn.interactable = false;
                SelectChicken2Btn.gameObject.SetActive(true);
                ActivateSelectChick();
                }
        }

        if(PlayerPrefs.GetInt("Chick3") == 1){
            if(BuyChicken3Btn != null){
                BuyChicken3Btn.interactable = false;
                SelectChicken3Btn.gameObject.SetActive(true);
                ActivateSelectChick();

                }
        }

        if(PlayerPrefs.GetInt("Chick4") == 1){
            if(BuyChicken4Btn != null){
                BuyChicken4Btn.interactable = false;
                SelectChicken4Btn.gameObject.SetActive(true);
                ActivateSelectChick();
                }
        }


    }

 
      public void ActivateSelectChick(){

        if( PlayerPrefs.GetInt("Chick1Selected")== 1){
            SelectChicken1Btn.interactable = false;
            SelectChicken2Btn.interactable = true;
            SelectChicken3Btn.interactable = true;
            SelectChicken4Btn.interactable = true;

            SelectChicken1Box.SetActive(false);
            SelectChicken2Box.SetActive(true);
            SelectChicken3Box.SetActive (true);
            SelectChicken4Box.SetActive (true);

        }
        if( PlayerPrefs.GetInt("Chick2Selected")== 1){
             SelectChicken1Btn.interactable = true;
            SelectChicken2Btn.interactable = false;
            SelectChicken3Btn.interactable = true;
            SelectChicken4Btn.interactable = true;

            SelectChicken1Box.SetActive(true);
            SelectChicken2Box.SetActive(false);
            SelectChicken3Box.SetActive (true);
            SelectChicken4Box.SetActive (true);

        }
        if( PlayerPrefs.GetInt("Chick3Selected")== 1){
            SelectChicken1Btn.interactable = true;
            SelectChicken2Btn.interactable = true;
            SelectChicken3Btn.interactable = false;
            SelectChicken4Btn.interactable = true;

             SelectChicken1Box.SetActive(true);
            SelectChicken2Box.SetActive(true);
            SelectChicken3Box.SetActive (false);
            SelectChicken4Box.SetActive (true);

        }
        if( PlayerPrefs.GetInt("Chick4Selected")== 1){
            SelectChicken1Box.SetActive(true);
            SelectChicken2Box.SetActive(true);
            SelectChicken3Box.SetActive (true);
            SelectChicken4Box.SetActive (false);

            SelectChicken1Btn.interactable = true;
            SelectChicken2Btn.interactable = true;
            SelectChicken3Btn.interactable = true;
            SelectChicken4Btn.interactable = false;

            
        }
    }

    public void BuyChicken(string chickenName){
        if(chickenName == "Chick2"){
            if(currentCoins >= Chicken2Price){
                newCoins = currentCoins - Chicken2Price;
                PlayerPrefs.SetInt("Coins", newCoins);
                PlayerPrefs.SetInt("Chick2", 1);
                BuyChicken2Btn.interactable = false;
                currentCoins = newCoins;
                SelectChicken2Btn.gameObject.SetActive(true);
                CoinAmountScript.instance.coinAmount = currentCoins;
            }
            else{
                NoBatteries.SetActive(true);
            }
        }
        else if(chickenName == "Chick3"){
                if(currentCoins >= Chicken3Price){
                newCoins = currentCoins - Chicken3Price;
                PlayerPrefs.SetInt("Coins", newCoins);
                PlayerPrefs.SetInt("Chick3", 1);
                BuyChicken3Btn.interactable = false;
                currentCoins = newCoins;
                SelectChicken3Btn.gameObject.SetActive(true);
                CoinAmountScript.instance.coinAmount = currentCoins;
            }
            else{
                NoBatteries.SetActive(true);
            }
        }
        else if(chickenName == "Chick4"){
            if(currentCoins >= Chicken4Price){
                newCoins = currentCoins - Chicken4Price;
                PlayerPrefs.SetInt("Coins", newCoins);
                PlayerPrefs.SetInt("Chick4", 1);
                BuyChicken4Btn.interactable = false;
                currentCoins = newCoins;
                SelectChicken4Btn.gameObject.SetActive(true);
                CoinAmountScript.instance.coinAmount = currentCoins;
            }
            else{
                NoBatteries.SetActive(true);
            }
        }
  
    }


  

    public void OkButton(){
		    NoBatteries.SetActive(false);
    }
}
