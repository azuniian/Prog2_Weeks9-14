using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class saturationLevelChanger : MonoBehaviour
{
    //variables being referenced by other scripts
    public GameObject JJ;

    //variables relating to the player gameObject
    public SpriteRenderer jjSprite;
    public Sprite leftLow1;
    public Sprite leftMedium1;
    public Sprite leftHigh1;
    
    //floats
    public float saturationLevel;

    //float variables from yarn scripts
    VariableStorageBehaviour variableStorage;
    public float mirrorInteractions;
    public float doorInteractions;
    public float wardrobeInteractions;
    public float pillsInteractions;
    public float showerInteractions;
    public float momInteractions;
    public float fridgeInteractions;

    //booleans
    public bool bedInteract = false;
    public bool mirrorInteract = false;
    public bool doorInteract = false;
    public bool fridgeInteract = false;
    public bool momInteract = false;
    public bool wardrobeInteract = false;
    public bool pillsInteract = false;
    public bool showerInteract = false;

    //boolean from yarn script
    public bool isChanged;

    public void Start()
    {
        jjSprite = JJ.GetComponent<SpriteRenderer>(); //gets the JJ sprite
        variableStorage = GameObject.FindObjectOfType<InMemoryVariableStorage>(); 
        interactionCheck jjScript = JJ.GetComponent<interactionCheck>(); //creating a script variable reference to access the custom Unity Event
        jjScript.onSpacePress.AddListener(SaturationLevelChanger); //adding a listener to reference a function in this script when the Unity Event is triggered in another script
        //jjScript.onSpacePress.AddListener(changeSprite);

    }

    void Update()
    {
        variableStorage.TryGetValue("$mirrorInteractions", out mirrorInteractions); //translating yarn variable for number of mirror interactions to C# float
        variableStorage.TryGetValue("$doorInteractions", out doorInteractions); //translating yarn variable for number of door interactions to C# float
        variableStorage.TryGetValue("$wardrobeInteractions", out wardrobeInteractions); //translating yarn variable for number of wardrobe interactions to C# float
        variableStorage.TryGetValue("$pillsInteractions", out pillsInteractions); //translating yarn variable for number of pill interactions to C# float
        variableStorage.TryGetValue("$showerInteractions", out showerInteractions); //translating yarn variable for number of shower interactions to C# float
        variableStorage.TryGetValue("$momInteractions", out momInteractions); //translating yarn variable for number of mom interactions to C# float
        variableStorage.TryGetValue("$fridgeInteractions", out fridgeInteractions); //translating yarn variable for number of fridge interactions to C# float
        variableStorage.TryGetValue("$isChanged", out isChanged); //translating yarn variable which checks which path was taken to a C# boolean
        variableStorage.TryGetValue("$bedInteract", out bedInteract); //translating if the bed has been interacted with from a yarn variable to a C# boolean
    }


    public void SaturationLevelChanger()
    {
        //checking which interaction type
        if (mirrorInteract == true)
        {
            //check which interaction it is
            if(mirrorInteractions == 0) //first
            {
                //check if the player interacted with the wardrobe first
                if (isChanged == false) //if they did not
                {
                    saturationLevel -= 1;

                }
                mirrorInteract = false;
            }

            else if(mirrorInteractions == 1) //second
            {
                //no saturation change
                mirrorInteract = false;
            }

            else if(mirrorInteractions >= 2) //third+
            {
                //no saturation change
                mirrorInteract = false;
            }
        }


        else if (wardrobeInteract == true)
        {
            //check which interaction it is
            if (wardrobeInteractions == 0) //first
            {
                saturationLevel += 1; //update saturation level
                wardrobeInteract = false;
            }

            else if(wardrobeInteractions >= 1) //second+
            {
                //no saturation change
                wardrobeInteract = false;
            }
        }


        else if (doorInteract == true)
        {
            //no saturation change, no matter what interaction it is
            doorInteract = false;
        }


        else if (pillsInteract == true)
        {
            //check which interaction it is
            if (pillsInteractions == 0) //first
            {
                saturationLevel += 1; //update saturation level
                pillsInteract = false;
            }

            else if(pillsInteractions == 1) //second
            {
                saturationLevel -= 1; //update saturation level
                pillsInteract = false;
            }

            else if(pillsInteractions == 2) //third
            {
                saturationLevel -= 2; //update saturation level
                pillsInteract = false;
            }

            else if(pillsInteractions >= 3) //fourth+
            {
                //no saturation change
                pillsInteract = false;
            }
        }

        else if (showerInteract == true)
        {
            //check which interaction it is
            if(showerInteractions == 0) //first
            {
                saturationLevel += 1; //update saturation level
                showerInteract = false;
            }

            else if(showerInteractions >= 1) //second+
            {
                //no saturation change
                showerInteract = false;
            }
        }

        else if (fridgeInteract == true)
        {
            //check which interaction it is
            if(fridgeInteractions == 0) //first
            {
                saturationLevel += 1;
                fridgeInteract = false;
            }

            else if(fridgeInteractions >= 1) //second
            {
                saturationLevel -= 1;
                fridgeInteract = false;
            }
        }

        else if (momInteract == true)
        {
            //check which interaction it is
            if(momInteractions == 0) //first
            {
                //no saturation change 
                momInteract = false;
            }

            else if(momInteractions == 1) //second
            {
                saturationLevel -= 1;
                momInteract = false;
            }

            else if(momInteractions >= 2) //third+
            {
                //no saturation change
                momInteract = false;
            }  
        }
        
        //after checking which interaction it is, need to ensure that the current sprite matches the saturation level
        if (saturationLevel == 1)
        {
            Debug.Log("sprite high");
            jjSprite.sprite = leftHigh1;
        }

        else if (saturationLevel == 0)
        {
            Debug.Log("sprite regular");
            jjSprite.sprite = leftMedium1;
        }

        else if (saturationLevel == -1)
        {
            Debug.Log("sprite low");
            jjSprite.sprite = leftLow1;
        }
    }
}
