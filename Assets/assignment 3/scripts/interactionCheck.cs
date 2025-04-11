using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class interactionCheck : MonoBehaviour
{
    public GameObject JJ;

    
    public GameObject dialogueRunner; //yarn prefab

    public UnityEvent onSpacePress; //custom unity event which is triggered by a key press
    public Tilemap furnitureTiles; //tilemap which contains all of the furniture tiles that can be interacted with
    public List<Tile> bedTiles = new List<Tile>(); //list of bed tiles
    public List<Tile> wardrobeTiles = new List<Tile>(); //list of wardrobe tiles
    public List<Tile> vanityTiles = new List<Tile>(); //list of vanity tiles
    public List<Tile> insideBedroomDoorTiles = new List<Tile>(); //list of door tiles

    public Vector3Int jjPos;

    public bool isDialogueRunning = false;

    void Start()
    {
        jjWalk moveScript = JJ.GetComponent<jjWalk>();
        jjPos = moveScript.positionOnGrid;
        
    }

    void Update()
    {
        Debug.Log(furnitureTiles.GetTile(jjPos));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //StartCoroutine(checkInteraction()); 
            checkInteraction();
        }
    }

    public void checkInteraction()
    {

        foreach(Tile tile in bedTiles)
        {
            if(furnitureTiles.GetTile(jjPos) == tile)
            {
                //dialogueRunner
                Debug.Log("bed tile");
                onSpacePress.Invoke();
            }
            //yield return null;
        }

        foreach(Tile tile in wardrobeTiles)
        {
            if (furnitureTiles.GetTile(jjPos) == tile)
            {
                //dialogueRunner
                Debug.Log("wardrobe tile");
                onSpacePress.Invoke();
            }
            //yield return null;
        }

        foreach (Tile tile in vanityTiles)
        {
            if (furnitureTiles.GetTile(jjPos) == tile)
            {
                //dialogueRunner
                Debug.Log("mirror tile");
                onSpacePress.Invoke();
            }
            //yield return null;
        }
       
        foreach(Tile tile in insideBedroomDoorTiles)
        {
            if (furnitureTiles.GetTile(jjPos) == tile)
            {
                //dialogueRunner
                Debug.Log("door tile");
                onSpacePress.Invoke();
            }
            //yield return null;
        }
        
        
    }
}
