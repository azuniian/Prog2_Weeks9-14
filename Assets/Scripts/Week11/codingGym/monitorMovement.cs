using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monitorMovement : MonoBehaviour
{
    public float speed = 4;
    Vector2 position;
    public Vector3 width;
    public AnimationCurve yCurve;
    public float t;

    // Start is called before the first frame update
    void Start()
    {
        position.x = -11;
    }

    // Update is called once per frame
    void Update()
    {
        position += Vector2.one*(transform.right * speed * Time.deltaTime);
        position.y = yCurve.Evaluate(t);

        t += Time.deltaTime;
        
        //NEED to create a vector for the original screen size to be able to change it to world screen size
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        //changing it to screen size uses the vector for the original screen size as a POSITION to map
        width = Camera.main.ScreenToWorldPoint(screenSize);


        if (position.x == 0) 
        {
            t = 0;
        }


        if (position.x >= width.x)
        {
            position.x -= width.x*2;
            //transform.position = position;
            t = 0;
        }

        transform.position = position;
    }
}
