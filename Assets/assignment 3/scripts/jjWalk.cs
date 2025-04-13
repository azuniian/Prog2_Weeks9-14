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
    public Vector3 posBeforeDialogue;
    public Vector3Int positionOnGrid;

    public Tilemap floorTiles;
    public List<Tile> hardwood = new List<Tile>();

    IEnumerator pause;
    bool dialogueIsRunning;


    void Start()
    {
        
        
    }

    void Update()
    {
        interactionCheck jjScript = JJ.GetComponent<interactionCheck>();
        jjScript.onSpacePress.AddListener(stopFunction);
        dialogueIsRunning = jjScript.isDialogueRunning;

        position = transform.position;
        direction = Input.GetAxis("Horizontal");
        upOrDown = Input.GetAxis("Vertical");

        if ((direction != 0 || upOrDown != 0) && dialogueIsRunning == false)
        {
            jjMovement();
            if(pause != null)
            {
                StopCoroutine(pause);
            }
        }
        else if(dialogueIsRunning == true)
        {
            stopFunction();
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

    public void stopFunction()
    {
        posBeforeDialogue = position;
        pause = pauseMovement();
        StartCoroutine(pause);
        
        interactionCheck jjScript = JJ.GetComponent<interactionCheck>();
        jjScript.onDialogueEnd.AddListener(jjScript.endDialogue);
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
        dialogueIsRunning = false;

        interactionCheck jjScript = JJ.GetComponent<interactionCheck>();
        jjScript.onDialogueEnd.Invoke();
    }
}
