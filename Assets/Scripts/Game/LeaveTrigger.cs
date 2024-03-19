using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LeaveTrigger : MonoBehaviour
{
    
    public static LeaveTrigger instance ; 

private bool levelGenerated = false;
    public int numberOfLevels = 0 ;
    void Awake(){
        instance = this ; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player")&&(!levelGenerated)))
        {
            if(PlayerControl.instance.speed < -8){
                Generate3Levels();
            }
            else{
                GenerateLevel();
            }
            
            levelGenerated = true;
        }
     }

    private void GenerateLevel(){
        if(LevelGenerator.instance.pieceIsAdded == false){
                LevelGenerator.instance.AddPiece();
                numberOfLevels ++;
                if(PlayerControl.instance.speed > -12){
                    PlayerControl.instance.speed -= 0.5f; 
                }
        }
        if(numberOfLevels >= 1){
            LevelGenerator.instance.pieceIsAdded = false ;
            LevelGenerator.instance.RemoveOldestPiece();
            numberOfLevels = 0 ;
        }
    } 

    private void Generate3Levels(){
        if(LevelGenerator.instance.pieceIsAdded == false){
                LevelGenerator.instance.AddPiece();
                LevelGenerator.instance.AddPiece();
                LevelGenerator.instance.AddPiece();
                numberOfLevels ++;
                if(PlayerControl.instance.speed > -10){
                    PlayerControl.instance.speed -= 0.5f; 
                }
        }
        if(numberOfLevels >= 1){
            LevelGenerator.instance.pieceIsAdded = false ;
            LevelGenerator.instance.RemoveOldestPiece();
            numberOfLevels = 0 ;
        }
    } 
}
