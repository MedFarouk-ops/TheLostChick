using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Runtime;

public class LevelGenerator : MonoBehaviour
{

    public bool pieceIsAdded = false ;
    public bool pieceIsRemoved = false ;
    public bool isGenerateInitialPieces = false ;
    //this is an instance of our LevelGenerator so we can use it in our gameManager 
    public static LevelGenerator instance ; 
    // this list will contain all the levelpieces we create
    public List<LevelPiece> levelPrefabs = new List<LevelPiece>();
    // this is the staring point of the first level
    public Transform levelStartPoint ; 
    // this will have all the level pieces that are currently in the game
    public List<LevelPiece> pieces = new List<LevelPiece>();
    private Vector3 spawnPosition = Vector3.zero ; 

    void Awake(){
        instance = this ; 
    }
    
    void Start(){
        if(isGenerateInitialPieces == false){
            GenerateInitialPieces();
        }
    }


    public void GenerateInitialPieces(){
        for(int i=0 ; i<5 ; i++){
            AddPiece();
            pieceIsAdded = false;
        }
        isGenerateInitialPieces = true;
    }

    // we will use this method to add pieces to owr game 
    public void AddPiece(){
        if(pieces.Count != 0 ){
            spawnPosition = pieces[pieces.Count-1].exitPoint.position;
           
        }
        else{
            spawnPosition = levelStartPoint.position;
        }
        SpawnLevel(spawnPosition);        
    }


    public void SpawnLevel(Vector3 SpawnPosition){
        int randomIndex = Random.Range( 0 , levelPrefabs.Count-1);
        LevelPiece piece = (LevelPiece)Instantiate(levelPrefabs[randomIndex] , new Vector3(0f, 0f ,spawnPosition.z), Quaternion.identity); 
        piece.transform.SetParent(this.transform , false);
        pieceIsAdded = true;
        pieces.Add(piece);
    }

    public void RemoveOldestPiece(){
        Destroy(pieces[0].gameObject);
        pieceIsRemoved = true;
        pieces.Remove(pieces[0]);
    }

}

