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
    public bool onFloor= true;
    public bool coroutineRunning = false;


    void Update()
    {
        interactionCheck jjScript = JJ.GetComponent<interactionCheck>(); //get the interaction check script
        jjScript.onSpacePress.AddListener(stopMoving); //add a listener to the unity event in the interaction check script which will change a game condition and add a new listener
        dialogueIsRunning = jjScript.isDialogueRunning; //reference a bool value in the interaction check script

        //resetting JJ position every frame
        position = transform.position;
        positionOnGrid = floorTiles.WorldToCell(position); //position translated to position on grid
        
        //check for inputs which would trigger movement
        direction = Input.GetAxis("Horizontal");
        upOrDown = Input.GetAxis("Vertical");

        if (dialogueIsRunning == false) //check if dialogue is running, if not, check if movement can be done
        {
            foreach (Tile tile in hardwoodTiles) //checking for each tile asset in the list
            {
                if (floorTiles.GetTile(positionOnGrid) == tile) //if the tile that JJ is on is a 'hardwood' tile (on the floor vs the wall)
                {
                    jjMovement(); //start the movement function
                    onFloor = true; //tell the code that JJ is on the floor
                    break; //end the loop early
                }
                onFloor = false; //JJ is not on the floor if the code reaches this point
            }

            if (onFloor == false) //if JJ is not on the floor (somehow)
            {
                position.y -= 0.2f; //move JJ down
                JJ.transform.position = position; //reset JJ position
                onFloor = true; //swap the value of the onFloor bool so that this doesn't run again until needed
            }

            //check to ensure coroutine is actually turned off
            if(coroutineRunning == true)
            {
                //stopping the coroutine
                StopCoroutine(pause);

                //invokes the dialogue end event which ensures that dialogue running booleans are all false
                jjScript.onDialogueEnd.Invoke(); //forces the event to happen

                coroutineRunning = false;
            }
        }
    }

    public void jjMovement() //function that checks for movement
    {
        if (direction > 0) //right movement
        {
            position.x += speed * Time.deltaTime;
        }
        else if (direction < 0) //left movement
        {
            position.x -= speed * Time.deltaTime;
        }
        if (upOrDown > 0) //up movement
        {
            position.y += speed * Time.deltaTime;
        }
        else if (upOrDown < 0) //down movement
        {
            position.y -= speed * Time.deltaTime;
        }
            
        position.z = 0; //ensure JJ sprite is always visible

        JJ.transform.position = position; //update JJ position
        positionOnGrid = floorTiles.WorldToCell(position); //update JJ's grid position for other scripts to reference
        //Debug.Log(positionOnGrid);
    }

    public void stopMoving() //game condition to check if dialogue is running
    {
            posBeforeDialogue = position; //gets the position of the player when dialogue is started
            pause = pauseMovement(); //set the coroutine to a variable
            StartCoroutine(pause); //start the coroutine which forces the player to not be able to move

            interactionCheck jjScript = JJ.GetComponent<interactionCheck>();
            jjScript.onDialogueEnd.AddListener(jjScript.endDialogue); //adds a listener for the custom unity event relating to dialogue
    }

    public IEnumerator pauseMovement()
    {
        coroutineRunning = true;

        dialogueIsRunning = true;
        while(dialogueIsRunning == true) //while dialogue is running
        {
            JJ.transform.position = posBeforeDialogue; //continues to update the player's position as the position they had before starting dialogue
            direction = 0; //disregards any left/right button presses
            upOrDown = 0; //disregards any up/down button presses
            yield return null; //stops the coroutine so it can begin again next frame at this point
        }

    }
}
