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
    
    public DialogueRunner dialogueRunner; //yarn prefab

    public UnityEvent onSpacePress; //custom unity event which is triggered by a key press
    public UnityEvent onDialogueEnd; //custom unity event which is triggered when dialogue is done

    public Tilemap furnitureTiles; //tilemap which contains all of the furniture tiles that can be interacted with
    public List<Tile> bedTiles = new List<Tile>(); //list of bed tiles
    public List<Tile> wardrobeTiles = new List<Tile>(); //list of wardrobe tiles
    public List<Tile> vanityTiles = new List<Tile>(); //list of vanity tiles
    public List<Tile> insideBedroomDoorTiles = new List<Tile>(); //list of door tiles

    public Vector3 jjPos;
    public Vector3Int jjPosOnGrid;

    public float mirrorInt;
    public float doorInt;
    public float wardrobeInt;
    public bool bedInt;
    public bool isChanged;


    public bool isDialogueRunning = false;

    void Start()
    {
        dialogueRunner.SetProject(bedroomDialogue);
        
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
        foreach(Tile tile in bedTiles)
        {
            if(furnitureTiles.GetTile(jjPosOnGrid) == tile)
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

        foreach(Tile tile in wardrobeTiles)
        {
            if (furnitureTiles.GetTile(jjPosOnGrid) == tile)
            {
                if(wardrobeInt == 0)
                {
                    saturationLevelChanger saturationScript = JJ.GetComponent<saturationLevelChanger>();
                    saturationScript.wardrobeInteract = true;
                    isDialogueRunning = true;
                    dialogueRunner.StartDialogue("wardrobeMorningInteraction1");
                }
                else if(wardrobeInt > 0)
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
                if(mirrorInt == 0 && isChanged == true)
                {
                    saturationLevelChanger saturationScript = JJ.GetComponent<saturationLevelChanger>();
                    saturationScript.mirrorInteract = true;
                    isDialogueRunning = true;
                    dialogueRunner.StartDialogue("mirrorMorningInteraction1");
                }
                else if(mirrorInt == 0 && isChanged == false)
                {
                    saturationLevelChanger saturationScript = JJ.GetComponent<saturationLevelChanger>();
                    saturationScript.mirrorInteract = true;
                    isDialogueRunning = true;
                    dialogueRunner.StartDialogue("mirrorMorningNotChanged");
                }
                else if(mirrorInt == 1)
                {
                    saturationLevelChanger saturationScript = JJ.GetComponent<saturationLevelChanger>();
                    saturationScript.mirrorInteract = true;
                    isDialogueRunning = true;
                    dialogueRunner.StartDialogue("mirrorMorningInteraction2");
                }
                else if(mirrorInt > 1)
                {
                    saturationLevelChanger saturationScript = JJ.GetComponent<saturationLevelChanger>();
                    saturationScript.mirrorInteract = true;
                    isDialogueRunning = true;
                    dialogueRunner.StartDialogue("mirrorMorningUnlimited");
                }
                onSpacePress.Invoke();
            }
        }
       
        foreach(Tile tile in insideBedroomDoorTiles) //checking for bedroom door interaction
        {
            if (furnitureTiles.GetTile(jjPosOnGrid) == tile) //if JJ is at the bedroom door
            {
                if(doorInt == 0) //so the dialogue line only plays the first time you interact with the door
                {
                    saturationLevelChanger saturationScript = JJ.GetComponent<saturationLevelChanger>();
                    saturationScript.doorInteract = true;
                    isDialogueRunning = true; //set the dialogue running boolean to true so the stop motion coroutine will start
                    dialogueRunner.StartDialogue("bedroomDoorExit"); //start specific dialogue
                }
                onSpacePress.Invoke(); //calls custom unity event
            }
        }
        
        
    }

    public void endDialogue() //once all lines of dialogue are finished, remove the listener
    {
        isDialogueRunning = false; //sets the dialogue running value to false so that the coroutine will end
        onSpacePress.RemoveListener(endDialogue);
        onDialogueEnd.RemoveAllListeners(); //removes all listeners relating to this unity event
    }
}
