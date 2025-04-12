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
    public Tilemap furnitureTiles; //tilemap which contains all of the furniture tiles that can be interacted with
    public List<Tile> bedTiles = new List<Tile>(); //list of bed tiles
    public List<Tile> wardrobeTiles = new List<Tile>(); //list of wardrobe tiles
    public List<Tile> vanityTiles = new List<Tile>(); //list of vanity tiles
    public List<Tile> insideBedroomDoorTiles = new List<Tile>(); //list of door tiles

    public Vector3Int jjPos;

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
        //get a reference to the position of JJ on the grid in the walking script
        jjWalk moveScript = JJ.GetComponent<jjWalk>();
        jjPos = moveScript.positionOnGrid;

        //get a reference to the interaction number taken from the yarn script within the saturation level script
        saturationLevelChanger saturationScript = JJ.GetComponent<saturationLevelChanger>();
        mirrorInt = saturationScript.mirrorInteractions;
        doorInt = saturationScript.doorInteractions;
        wardrobeInt = saturationScript.wardrobeInteractions;
        bedInt = saturationScript.bedInteract;
        isChanged = saturationScript.isChanged;
        

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
                bedInt = true;
                isDialogueRunning = true;
                dialogueRunner.StartDialogue("bedMorning");
            }
            onSpacePress.Invoke();
        }

        foreach(Tile tile in wardrobeTiles)
        {
            if (furnitureTiles.GetTile(jjPos) == tile)
            {
                if(wardrobeInt == 0)
                {
                    isDialogueRunning = true;
                    dialogueRunner.StartDialogue("wardrobeMorningInteraction1");
                }
                else if(wardrobeInt > 0)
                {
                    isDialogueRunning = true;
                    dialogueRunner.StartDialogue("wardrobeMorningUnlimited");
                }
                onSpacePress.Invoke();
            }
        }

        foreach (Tile tile in vanityTiles)
        {
            if (furnitureTiles.GetTile(jjPos) == tile)
            {
                if(mirrorInt == 0 && isChanged == true)
                {
                    isDialogueRunning = true;
                    dialogueRunner.StartDialogue("mirrorMorningInteraction1");
                }
                else if(mirrorInt == 0 && isChanged == false)
                {
                    isDialogueRunning = true;
                    dialogueRunner.StartDialogue("mirrorMorningNotChanged");
                }
                else if(mirrorInt == 1)
                {
                    isDialogueRunning = true;
                    dialogueRunner.StartDialogue("mirrorMorningInteraction2");
                }
                else if(mirrorInt > 1)
                {
                    isDialogueRunning = true;
                    dialogueRunner.StartDialogue("mirrorMorningUnlimited");
                }
                onSpacePress.Invoke();
            }
        }
       
        foreach(Tile tile in insideBedroomDoorTiles)
        {
            if (furnitureTiles.GetTile(jjPos) == tile)
            {
                if(doorInt == 0)
                {
                    isDialogueRunning = true;
                    dialogueRunner.StartDialogue("bedroomDoorExit");
                }
                onSpacePress.Invoke();
            }
        }
        
        
    }

    public void endDialogue() //once all lines of dialogue are finished, remove the listener
    {
        isDialogueRunning = false;
        onSpacePress.RemoveListener(endDialogue);
    }
}
