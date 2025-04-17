using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using Yarn.Unity;

public class interactionCheck : MonoBehaviour
{
    public GameObject JJ;

    public YarnProject bedroomDialogue;
    public YarnProject bathroomDialogue;
    public YarnProject kitchenDialogue;
    public YarnProject transitionDialogue;
    public YarnProject gameEnds;
    
    public DialogueRunner dialogueRunner; //yarn prefab

    public UnityEvent onSpacePress; //custom unity event which is triggered by a key press
    public UnityEvent onDialogueEnd; //custom unity event which is triggered when dialogue is done
    public UnityEvent onRoomChange; //custom unity event which is triggered when specific interactions occur (changing rooms, moving cameras, etc)

    //bedroom tilemaps
    public Tilemap furnitureTiles; //tilemap which contains all of the furniture tiles that can be interacted with
    public List<Tile> bedTiles = new List<Tile>(); //list of bed tiles
    public List<Tile> wardrobeTiles = new List<Tile>(); //list of wardrobe tiles
    public List<Tile> vanityTiles = new List<Tile>(); //list of vanity tiles
    public List<Tile> insideBedroomDoorTiles = new List<Tile>(); //list of door tiles

    //hallway tilemaps
    public Tilemap hallwayFloor;
    public Tilemap hallwayJJDoor;
    public Tilemap hallwayBathroomDoor;
    public Tilemap hallwayBanister;
    public List<Tile> jjDoorTiles = new List<Tile>();
    public List<Tile> doorTiles = new List<Tile>();
    public List<Tile> stairTiles = new List<Tile>();

    //kitchen tilemaps
    public Tilemap kitchenFloor;
    public Tilemap kitchenAssets;
    public List<Tile> frontDoorTiles = new List<Tile>();
    public List<Tile> banisterTiles = new List<Tile>();

    //transition tilemaps
    public Tilemap transitionArea;
    public List<Tile> transitionTiles = new List<Tile>();

    public Vector3 jjPos;
    public Vector3Int jjPosOnGrid;

    //bedroom variables
    public float mirrorInt;
    public float doorInt;
    public float wardrobeInt;
    public bool bedInt;
    public bool isChanged;

    //booleans to check where the player is
    public bool inBedroom;
    public bool fromBedroom;
    public bool inHallway;
    public bool fromHallway;
    public bool inBathroom;
    public bool fromBathroom;
    public bool inKitchen;
    public bool fromKitchen;
    public bool inTransition;
    public bool fromTransition;

    //dialogue running checker
    public bool isDialogueRunning = false;

    //game end checker
    public bool gameEndEarly;
    public bool gameEnd;


    void Start()
    {
        dialogueRunner.SetProject(bedroomDialogue);
        inBedroom = true;
    }

    void Update()
    {
        //find JJ's empty (used for dialogue sorting) position on the grid
        jjPos = JJ.transform.position;
        jjPosOnGrid = furnitureTiles.WorldToCell(jjPos);

        //get a reference to the interaction number taken from the yarn script within the saturation level script
        saturationLevelChanger saturationScript = JJ.GetComponent<saturationLevelChanger>();
        mirrorInt = saturationScript.mirrorInteractions;
        doorInt = saturationScript.doorInteractions;
        wardrobeInt = saturationScript.wardrobeInteractions;
        bedInt = saturationScript.bedInteract;
        isChanged = saturationScript.isChanged;
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space pressed");
            //StartCoroutine(checkInteraction()); 
            checkInteraction();
        }

        
    }

    public void checkInteraction()
    {
        //bedroom interactions
        if(inBedroom == true)
        {
            foreach (Tile tile in bedTiles)
            {
                if (furnitureTiles.GetTile(jjPosOnGrid) == tile)
                {
                    //change value of boolean in saturation script to ensure that the function runs properly
                    saturationLevelChanger saturationScript = JJ.GetComponent<saturationLevelChanger>();
                    saturationScript.bedInteract = true;

                    bedInt = true;
                    isDialogueRunning = true;
                    dialogueRunner.StartDialogue("bedMorning");
                }
                onSpacePress.Invoke();
            }

            foreach (Tile tile in wardrobeTiles)
            {
                if (furnitureTiles.GetTile(jjPosOnGrid) == tile)
                {
                    if (wardrobeInt == 0)
                    {
                        saturationLevelChanger saturationScript = JJ.GetComponent<saturationLevelChanger>();
                        saturationScript.wardrobeInteract = true;
                        isDialogueRunning = true;
                        dialogueRunner.StartDialogue("wardrobeMorningInteraction1");
                    }
                    else if (wardrobeInt > 0)
                    {
                        saturationLevelChanger saturationScript = JJ.GetComponent<saturationLevelChanger>();
                        saturationScript.wardrobeInteract = true;
                        isDialogueRunning = true;
                        dialogueRunner.StartDialogue("wardrobeMorningUnlimited");
                    }
                    onSpacePress.Invoke();
                }
            }

            foreach (Tile tile in vanityTiles)
            {
                if (furnitureTiles.GetTile(jjPosOnGrid) == tile)
                {
                    if (mirrorInt == 0 && isChanged == true)
                    {
                        saturationLevelChanger saturationScript = JJ.GetComponent<saturationLevelChanger>();
                        saturationScript.mirrorInteract = true;
                        isDialogueRunning = true;
                        dialogueRunner.StartDialogue("mirrorMorningInteraction1");
                    }
                    else if (mirrorInt == 0 && isChanged == false)
                    {
                        saturationLevelChanger saturationScript = JJ.GetComponent<saturationLevelChanger>();
                        saturationScript.mirrorInteract = true;
                        isDialogueRunning = true;
                        dialogueRunner.StartDialogue("mirrorMorningNotChanged");
                    }
                    else if (mirrorInt == 1)
                    {
                        saturationLevelChanger saturationScript = JJ.GetComponent<saturationLevelChanger>();
                        saturationScript.mirrorInteract = true;
                        isDialogueRunning = true;
                        dialogueRunner.StartDialogue("mirrorMorningInteraction2");
                    }
                    else if (mirrorInt > 1)
                    {
                        saturationLevelChanger saturationScript = JJ.GetComponent<saturationLevelChanger>();
                        saturationScript.mirrorInteract = true;
                        isDialogueRunning = true;
                        dialogueRunner.StartDialogue("mirrorMorningUnlimited");
                    }
                    onSpacePress.Invoke();
                }
            }

            foreach (Tile tile in insideBedroomDoorTiles) //checking for bedroom door interaction
            {
                if (furnitureTiles.GetTile(jjPosOnGrid) == tile) //if JJ is at the bedroom door
                {
                    if (doorInt == 0) //so the dialogue line only plays the first time you interact with the door
                    {
                        saturationLevelChanger saturationScript = JJ.GetComponent<saturationLevelChanger>();
                        saturationScript.doorInteract = true;
                        isDialogueRunning = true; //set the dialogue running boolean to true so the stop motion coroutine will start
                        dialogueRunner.StartDialogue("bedroomDoorExit"); //start specific dialogue
                        onSpacePress.Invoke(); //calls custom unity event
                    }
                    fromBedroom = true;
                    inBedroom = false;
                    inHallway = true;
                    onRoomChange.Invoke();
                }
            }
        }

        //hallway interactions
        else if(inHallway == true)
        {
            foreach(Tile tile in jjDoorTiles)
            {
                if(hallwayJJDoor.GetTile(jjPosOnGrid) == tile)
                {
                    inHallway = false;
                    fromHallway = true;
                    inBedroom = true;
                    onRoomChange.Invoke();
                }
            }

            foreach(Tile tile in doorTiles)
            {
                if(hallwayBathroomDoor.GetTile(jjPosOnGrid) == tile)
                {
                    inHallway = false;
                    fromHallway=true;
                    inBathroom = true;
                    onRoomChange.Invoke();
                }
            }

            foreach(Tile tile in stairTiles)
            {
                if(hallwayBanister.GetTile(jjPosOnGrid) == tile)
                {
                    inHallway = false;
                    fromHallway = true;
                    inKitchen = true;
                    onRoomChange.Invoke();
                }
            }
        }


        //bathroom interactions
        else if(inBathroom == true)
        {

        }


        //kitchen interactions
        else if(inKitchen == true)
        {
            foreach(Tile tile in frontDoorTiles)
            {
                if(kitchenAssets.GetTile(jjPosOnGrid) == tile)
                {
                    inKitchen = false;
                    inTransition = true;
                    onRoomChange.Invoke();
                }
            }

            foreach(Tile tile in banisterTiles)
            {
                if(kitchenAssets.GetTile(jjPosOnGrid) == tile)
                {
                    inKitchen = false;
                    fromKitchen = true;
                    inHallway = true;
                    onRoomChange.Invoke();
                }
            }
        }


        //transition interactions
        else if(inTransition == true)
        {
            dialogueRunner.SetProject(transitionDialogue);
            dialogueRunner.StartDialogue("transition1");
            dialogueRunner.StartDialogue("transition2");
            dialogueRunner.StartDialogue("transition3");
            gameEnd = true;
            dialogueRunner.SetProject(gameEnds);
            onGameEnd();
        }



        //game ends
        if(bedInt == true)
        {
            gameEndEarly = true;
            dialogueRunner.SetProject(gameEnds);
            onGameEnd();
        }
        
    }

    public void endDialogue() //once all lines of dialogue are finished, remove the listener
    {
        isDialogueRunning = false; //sets the dialogue running value to false so that the coroutine will end
        onDialogueEnd.RemoveAllListeners(); //removes all listeners relating to this unity event (i.e. hard reset of the game condition)
    }

    public void onGameEnd()
    {
        if (gameEndEarly == true)
        {
            
        }

        else if(gameEnd == true)
        {

        }
    }
}
