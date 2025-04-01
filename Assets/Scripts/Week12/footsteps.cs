using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footsteps : MonoBehaviour
{

    public AudioSource footstep1;
    public AudioSource footstep2;

    bool sound1;
    bool sound2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void playFootstep()
    {
        if (footstep1.isPlaying == false && footstep2.isPlaying == false)
        {
            //play a footstep sound randomly
            float soundChoice = Random.Range(0, 2);
            Debug.Log(soundChoice);
            if(soundChoice == 0)
            {
                sound1 = true;
            }
            else if(soundChoice == 1)
            {
                sound2 = true;
            }


            if(sound1 == true)
            {
                footstep1.Play();
                Debug.Log("Footstep 1 playing");
                sound1 = false;
            }
            else if(sound2 == true)
            {
                footstep2.Play();
                Debug.Log("Footstep 2 playing");
                sound2 = false;
            }
        }
    }
}
