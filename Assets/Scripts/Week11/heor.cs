using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heor : MonoBehaviour
{
    public float speed = 2;
    Animator a;
    SpriteRenderer sr;
    public bool canRun = true;
    

    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

        float direction = Input.GetAxis("Horizontal");
        sr.flipX = direction < 0;

        a.SetFloat("speed", Mathf.Abs(direction)); 

        if (Input.GetMouseButtonDown(0))
        {
            a.SetTrigger("attack");
            canRun = false;
        }

        if (canRun == true)
        {
            transform.position += transform.right * direction * speed * Time.deltaTime;
        }

    }

    public void AttackHasFinished()
    {
        Debug.Log("attack done");
        canRun = true;
    }
}
