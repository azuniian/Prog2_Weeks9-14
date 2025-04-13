using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Yarn.Unity;

public class jjWalk : MonoBehaviour
{
    public GameObject JJ;
    public DialogueRunner dialogueRunner;

    //movement-related variables
    public float speed = 0.5f;
    public float direction;
    public float upOrDown;
    public Vector3 position;
    public Vector3 posBeforeDialogue;
    public Vector3Int positionOnGrid;

    public Tilemap floorTiles;
    public List<Tile> hardwoodTiles = new List<Tile>();

    IEnumerator pause;
    bool dialogueIsRunning;
    public bool onFloor;


    void Update()
    {
        interactionCheck jjScript = JJ.GetComponent<interactionCheck>();
        jjScript.onSpacePress.AddListener(stopMoving);
        dialogueIsRunning = jjScript.isDialogueRunning;

        position = transform.position;
        positionOnGrid = floorTiles.WorldToCell(position);
        direction = Input.GetAxis("Horizontal");
        upOrDown = Input.GetAxis("Vertical");

        if (dialogueIsRunning == false)
        {
            foreach (Tile tile in hardwoodTiles)
            {
                Debug.Log("checking for floor tile");
                if (floorTiles.GetTile(positionOnGrid) == tile)
                {
                    jjMovement();
                    onFloor = true;
                    break;
                }
            }




        }
    }

    public void jjMovement()
    {
        if (direction > 0)
        {
            position.x += speed * Time.deltaTime;
        }
        else if (direction < 0)
        {
            position.x -= speed * Time.deltaTime;
        }
        if (upOrDown > 0)
        {
            position.y += speed * Time.deltaTime;
        }
        else if (upOrDown < 0)
        {
            position.y -= speed * Time.deltaTime;
        }
            
        position.z = 0;

        JJ.transform.position = position;
        positionOnGrid = floorTiles.WorldToCell(position);
        //Debug.Log(positionOnGrid);
    }

    public void stopMoving()
    {
        if(dialogueIsRunning == true)
        {
            posBeforeDialogue = position;
            pause = pauseMovement();
            StartCoroutine(pause);

            interactionCheck jjScript = JJ.GetComponent<interactionCheck>();
            jjScript.onDialogueEnd.AddListener(jjScript.endDialogue);
        }
    }

    public IEnumerator pauseMovement()
    {
        while(dialogueIsRunning == true)
        {
            JJ.transform.position = posBeforeDialogue;
            direction = 0;
            upOrDown = 0;
            yield return null;
        }
    }
}
