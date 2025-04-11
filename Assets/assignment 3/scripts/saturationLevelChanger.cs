using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class saturationLevelChanger : MonoBehaviour
{
    //variables being referenced by other scripts
    public GameObject JJ;
    
    //floats
    float saturationLevel;

    //float variables from yarn scripts
    VariableStorageBehaviour variableStorage;
    float mirrorInteractions;
    float doorInteractions;
    float wardrobeInteractions;
    float pillsInteractions;
    float showerInteractions;
    float momInteractions;
    float fridgeInteractions;

    //booleans
    bool mirrorInteract = false;
    bool doorInteract = false;
    bool fridgeInteract = false;
    bool momInteract = false;
    bool wardrobeInteract = false;
    bool pillsInteract = false;
    bool showerInteract = false;

    //boolean from yarn script
    bool isChanged;

    public void Start()
    {
        variableStorage = GameObject.FindObjectOfType<InMemoryVariableStorage>(); 
        interactionCheck jjScript = JJ.GetComponent<interactionCheck>(); //creating a script variable reference to access the custom Unity Event
        jjScript.onSpacePress.AddListener(SaturationLevelChanger); //adding a listener to reference a function in this script when the Unity Event is triggered in another script

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
    }


    public void SaturationLevelChanger()
    {
        //Debug.Log("listener added and working");
        //checking which interaction type
        if (mirrorInteract == true)
        {
            //check which interaction it is
            if(mirrorInteractions == 1) //first
            {
                //check if the player interacted with the wardrobe first
                if (isChanged == false) //if they did not
                {
                    saturationLevel -= 1;

                }
                mirrorInteract = false;
            }

            else if(mirrorInteractions == 2) //second
            {
                //no saturation change
                mirrorInteract = false;
            }

            else if(mirrorInteractions >= 3) //third+
            {
                //no saturation change
                mirrorInteract = false;
            }
        }


        else if (wardrobeInteract == true)
        {
            //check which interaction it is
            if (wardrobeInteractions == 1) //first
            {
                saturationLevel += 1; //update saturation level
                wardrobeInteract = false;
            }

            else if(wardrobeInteractions >= 2) //second+
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
            if (pillsInteractions == 1) //first
            {
                saturationLevel += 1; //update saturation level
                pillsInteract = false;
            }

            else if(pillsInteractions == 2) //second
            {
                saturationLevel -= 1; //update saturation level
                pillsInteract = false;
            }

            else if(pillsInteractions == 3) //third
            {
                saturationLevel -= 2; //update saturation level
                pillsInteract = false;
            }

            else if(pillsInteractions >= 4) //fourth+
            {
                //no saturation change
                pillsInteract = false;
            }
        }

        else if (showerInteract == true)
        {
            //check which interaction it is
            if(showerInteractions == 1) //first
            {
                saturationLevel += 1; //update saturation level
                showerInteract = false;
            }

            else if(showerInteractions >= 2) //second+
            {
                //no saturation change
                showerInteract = false;
            }
        }

        else if (fridgeInteract == true)
        {
            //check which interaction it is
            if(fridgeInteractions == 1) //first
            {
                saturationLevel += 1;
                fridgeInteract = false;
            }

            else if(fridgeInteractions >= 2) //second
            {
                saturationLevel -= 1;
                fridgeInteract = false;
            }
        }

        else if (momInteract == true)
        {
            //check which interaction it is
            if(momInteractions == 1) //first
            {
                //no saturation change 
                momInteract = false;
            }

            else if(momInteractions == 2) //second
            {
                saturationLevel -= 1;
                momInteract = false;
            }

            else if(momInteractions >= 3) //third+
            {
                //no saturation change
                momInteract = false;
            }  
        }


    }
}
