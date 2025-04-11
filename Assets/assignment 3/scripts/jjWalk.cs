using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class jjWalk : MonoBehaviour
{
    //variables being referenced by other scripts
    public GameObject JJ;
    
    //movement-related variables
    Vector3 position;
    Vector3Int positionOnGrid;
    float speed = 3f;
    float direction;

    //tiles and tilemaps
    Tilemap floorTiles;
    Tile hardwood;

    //coroutines
    IEnumerator moveSprite;

    void Start()
    {
        interactionCheck jjScript = JJ.GetComponent<interactionCheck>(); //creating a script variable reference to access the custom Unity Event
        //jjScript.onSpacePress.AddListener(stopMovement); //adding a listener to reference a function in this script when the Unity Event is triggered in another script
        moveSprite = updatePos();
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        position.z = 0;

        direction = Input.GetAxis("Horizontal");

        if (direction > 0)
        {
            position.x += speed;
        }
        else if (direction < 0)
        {
            position.x -= speed;
        }
        
        if (Input.GetAxis("Vertical") == 1)
        {
            position.y += speed;
        }
        else if(Input.GetAxis("Vertical") == -1)
        {
            position.y -= speed;
        }

        while (true)
        {
            StartCoroutine(moveSprite);
        }
        
    }

    public IEnumerator updatePos()
    {
        positionOnGrid = floorTiles.WorldToCell(position);
        Debug.Log(positionOnGrid);
        yield return null;
    }

    public void stopMovement()
    {
        StopCoroutine(moveSprite);
    }
}
