using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeRoomCamera : MonoBehaviour
{
    public Transform JJ;

    public GameObject loadingText;

    public GameObject bedroomCam; 
    public GameObject hallwayCam;
    public GameObject bathroomCam;
    public GameObject kitchenCam;
    public GameObject transitionCam;
    public GameObject betweenRoomsCam;


    public bool isInBedroom;
    public bool isInHallway;
    public bool isInBathroom;
    public bool isInKitchen;
    public bool isInTransition;
    public bool comingFromBedroom;
    public bool comingFromHallway;
    public bool comingFromBathroom;
    public bool comingFromKitchen;
    public bool comingFromTransition;

    public Vector3 posHallToBed;
    public Vector3 posBedToHall;
    public Vector3 posHallToBath;
    public Vector3 posBathToHall;
    public Vector3 posHallToKit;
    public Vector3 posKitToHall;
    public Vector3 posKitToTrans;
    public Vector3 posTransToKit;

    void Start()
    {
        bedroomCam.SetActive(true);

        interactionCheck interactionScript = JJ.GetComponent<interactionCheck>();
        interactionScript.onRoomChange.AddListener(moveJJ);

        //hallway to bedroom position set
        posHallToBed.x = -22.4f;
        posHallToBed.y = -14.25f;
        posHallToBed.x = 0;

        //bedroom to hallway position set
        posBedToHall.x = -45.81f;
        posBedToHall.y = -14.8f;
        posBedToHall.z = 0;

        //hallway to bathroom position set
        posHallToBath.x = -116.9f;
        posHallToBath.y = -16.5f;
        posHallToBath.z = 0;

        //bathroom to hallway position set
        posBathToHall.x = -75.4f;
        posBathToHall.y = -14.8f;
        posBathToHall.z = 0;

        //hallway to kitchen position set
        posHallToKit.x = -165.3f;
        posHallToKit.y = -16f;
        posHallToKit.z = 0;

        //kitchen to hallway position set
        posKitToHall.x = -90f;
        posKitToHall.y = -16.1f;
        posKitToHall.z = 0;

        //kitchen to transition area position set
        posKitToTrans.x = -223.1f;
        posKitToTrans.y = -15.88f;
        posKitToTrans.z = 0;

        //transition area to kitchen position set
        posTransToKit.x = -215.96f;
        posTransToKit.y = -15.88f;
        posTransToKit.z = 0;
    }

    void Update()
    {
        interactionCheck interactionScript = JJ.GetComponent<interactionCheck>();
        isInBedroom = interactionScript.inBedroom;
        isInHallway = interactionScript.inHallway;
        isInBathroom = interactionScript.inBathroom;
        isInKitchen = interactionScript.inKitchen;
        isInTransition = interactionScript.inTransition;

        comingFromBedroom = interactionScript.fromBedroom;
        comingFromHallway = interactionScript.fromHallway;
        comingFromBathroom = interactionScript.fromBathroom;
        comingFromKitchen = interactionScript.fromKitchen;
        comingFromTransition = interactionScript.fromTransition;
    }

    public void moveJJ()
    {

        interactionCheck interactionScript = JJ.GetComponent<interactionCheck>();
        isInBedroom = interactionScript.inBedroom;
        isInHallway = interactionScript.inHallway;
        isInBathroom = interactionScript.inBathroom;
        isInKitchen = interactionScript.inKitchen;
        isInTransition = interactionScript.inTransition;

        comingFromBedroom = interactionScript.fromBedroom;
        comingFromHallway = interactionScript.fromHallway;
        comingFromBathroom = interactionScript.fromBathroom;
        comingFromKitchen = interactionScript.fromKitchen;
        comingFromTransition = interactionScript.fromTransition;


        if (isInBedroom == true)
        {

            if (comingFromHallway == true)
            {
                betweenRoomsCam.SetActive(true);
                hallwayCam.SetActive(false);
                //move JJ to the bedroom door
                JJ.position = posHallToBed;
                
            }

            //turn on bedroom camera
            bedroomCam.SetActive(true);

            //interactionCheck interactionScript = JJ.GetComponent<interactionCheck>();
            interactionScript.fromHallway = false;
        }

        else if(isInHallway == true)
        {
            Debug.Log("move JJ to hallway");
            if(comingFromBedroom == true)
            {
                //put JJ in front of bedroom
                bedroomCam.SetActive(false);
                JJ.position = posBedToHall;
                
            }

            else if(comingFromBathroom == true)
            {
                //put JJ in front of bathroom door
                bathroomCam.SetActive(false);
                JJ.position = posBathToHall;
                
            }

            else if(comingFromKitchen == true)
            {
                //put JJ by banisters
                kitchenCam.SetActive(false);
                JJ.position = posKitToHall;
                
            }

            //turn hallway camera on
            hallwayCam.SetActive(true);

            //interactionCheck interactionScript = JJ.GetComponent<interactionCheck>();
            interactionScript.fromBedroom = false;
            interactionScript.fromBathroom = false;
            interactionScript.fromKitchen = false;
        }

        else if(isInBathroom == true)
        {
            if(comingFromHallway == true)
            {
                //put JJ inside bathroom
                hallwayCam.SetActive(false);
                JJ.position = posHallToBath;
                
            }

            //turn on bathroom camera
            bathroomCam.SetActive(true);

            //interactionCheck interactionScript = JJ.GetComponent<interactionCheck>();
            interactionScript.fromHallway = false;
        }

        else if(isInKitchen == true)
        {
            if(comingFromHallway == true)
            {
                //put JJ in front of stairs
                hallwayCam.SetActive(false);
                JJ.position = posHallToKit;
                
            }

            else if(comingFromTransition == true)
            {
                //put JJ in front of front door
                transitionCam.SetActive(false);
                JJ.position = posTransToKit;
                
            }

            //turn on kitchen camera
            kitchenCam.SetActive(true);

            //interactionCheck interactionScript = JJ.GetComponent<interactionCheck>();
            interactionScript.fromHallway = false;
            interactionScript.fromTransition = false;
        }

        else if(isInTransition == true)
        {
            if(comingFromKitchen == true)
            {
                //put JJ on deck
                kitchenCam.SetActive(false);
                JJ.position = posKitToTrans;
                
            }

            //turn on transition camera
            transitionCam.SetActive(true);

            //interactionCheck interactionScript = JJ.GetComponent<interactionCheck>();
            interactionScript.fromKitchen = false;
        }

        loadingText.SetActive(false);
        betweenRoomsCam.SetActive(false);
    }

}
