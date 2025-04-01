using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class playerMove : MonoBehaviour
{
    public Vector2 mousePos;
    public Vector2 knightPos;
    public Transform knight;
    
    public float speed = 3;

    public Tilemap tiles;
    public Tile grass;
    public Tile stone;


    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        knightPos = Vector2.one * knight.position;

        if (Input.GetMouseButtonDown(0))
        {
            if (mousePos != knightPos)
            {
                //check if tile clicked on is grass
                Vector3Int gridPosition = tiles.WorldToCell(mousePos);

                if (tiles.GetTile(gridPosition) != grass)
                {
                    knightPos = mousePos;
                    knight.position = mousePos;
                }

            }
        }
        
    }
}
