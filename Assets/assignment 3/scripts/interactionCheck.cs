using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class interactionCheck : MonoBehaviour
{
    public UnityEvent onSpacePress; //custom unity event which is triggered by a key press
    public Tilemap furnitureTiles; //tilemap which contains all of the furniture tiles that can be interacted with
    public List<Tile> bedTiles = new List<Tile>(); //list of bed tiles
    public List<Tile> wardrobeTiles = new List<Tile>(); //list of wardrobe tiles
    public List<Tile> vanityTiles = new List<Tile>(); //list of vanity tiles
    public List<Tile> insideBedroomDoorTiles = new List<Tile>(); //list of door tiles
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space");
            StartCoroutine(checkInteraction());
        }
    }

    public IEnumerator checkInteraction()
    {
        foreach(Tile tile in bedTiles)
        {
            
        }


        yield return null;
    }
}
