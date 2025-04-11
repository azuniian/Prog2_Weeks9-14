using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class jjWalk : MonoBehaviour
{
    public GameObject JJ;

    //movement-related variables
    public float speed = 0.5f;
    public float direction;
    public float upOrDown;
    public Vector3 position;
    public Vector3Int positionOnGrid;

    public Tilemap floorTiles;
    public List<Tile> hardwood = new List<Tile>();

    

    void Start()
    {
        interactionCheck jjScript = JJ.GetComponent<interactionCheck>();
        jjScript.onSpacePress.AddListener(pauseMovement);
    }

    void Update()
    {
        position = transform.position;
        direction = Input.GetAxis("Horizontal");
        upOrDown = Input.GetAxis("Vertical");

        if(direction > 0)
        {
            position.x += speed * Time.deltaTime;
        }
        else if(direction < 0)
        {
            position.x -= speed * Time.deltaTime;
        }
        if(upOrDown > 0)
        {
            position.y += speed * Time.deltaTime;
        }
        else if(upOrDown < 0)
        {
            position.y -= speed * Time.deltaTime;
        }

        position.z = 0;

        JJ.transform.position = position;
        positionOnGrid = floorTiles.WorldToCell(position);
            
    }

    public void pauseMovement()
    {
        Debug.Log("Stopping");
    }
}
