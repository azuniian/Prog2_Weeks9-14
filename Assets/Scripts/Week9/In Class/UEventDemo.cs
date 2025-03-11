using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UEventDemo : MonoBehaviour
{
    public RectTransform teaImage;

    public UnityEvent TimerHasFinished;
    public float timerLength = 2f;
    public float t;

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > timerLength)
        {
            t = 0;

        }
    }

    public void buttonPushed()
    {
        Debug.Log("Button hast been pressed, my lord.");
    }

    public void buttonPushedAgain()
    {
        Debug.Log("Please stop pressing me.");
    }

    public void imageInside()
    {
        Debug.Log("We've intercepted their movement plans my lord!");
        teaImage.localScale = Vector3.one * 1.2f;
    }

    public void imageOutside()
    {
        Debug.Log("We've lost them...");
        teaImage.localScale = Vector3.one;
    }
}
