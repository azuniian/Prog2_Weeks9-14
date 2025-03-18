using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bananaGrow : MonoBehaviour
{
    public Transform banana;
    public AnimationCurve curve;
    public float t;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(bananamation());
    }

    public IEnumerator bananamation()
    {
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
        
    }
}
