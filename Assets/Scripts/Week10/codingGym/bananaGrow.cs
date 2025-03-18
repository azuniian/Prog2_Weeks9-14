using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bananaGrow : MonoBehaviour
{
    public Transform banana;
    public AnimationCurve curve;
    public float t;
    public Button attackButton;

    
    public void bananamationStart()
    {
        StartCoroutine(bananamation());
    }

    public IEnumerator bananamation()
    {
        attackButton.interactable = false;
        t = 0.5f;
        while(t < 1)
        {
            t += Time.deltaTime;
            banana.transform.localScale = (Vector3.one * t);
            yield return null;
        }

        while(t > 0.75)
        {
            t -= Time.deltaTime;
            banana.transform.localScale = (Vector3.one * t);
            yield return null;
        }
        attackButton.interactable = true;
    }
}
