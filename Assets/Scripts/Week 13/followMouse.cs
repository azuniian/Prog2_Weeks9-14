using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followMouse : MonoBehaviour
{
    Vector3 mousePos;
    public SpriteRenderer sprite;

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            sprite.color = Random.ColorHSV();
        }
    }
}
