using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KitClock : MonoBehaviour
{
    public Transform hourHand;
    public Transform minuteHand;

    public float timeAnHourTakes = 5;

    public float t;
    public int hour = 0;

    public UnityEvent<int> OnTheHour;

    Coroutine clockIsRunning;
    IEnumerator doOneHour;

    public void Start()
    {
        clockIsRunning = StartCoroutine(moveTheClock());
    }

    private IEnumerator moveTheClock()
    {
        while (true)
        {
            doOneHour = moveForOneHour();
            yield return StartCoroutine(doOneHour);
        }
    } 

    public IEnumerator moveForOneHour()
    {
        t = 0;
        while (t < timeAnHourTakes)
        {
            t += Time.deltaTime;
            minuteHand.Rotate(0, 0, -(360/timeAnHourTakes)*Time.deltaTime);
            hourHand.Rotate(0, 0, -(30 / timeAnHourTakes) * Time.deltaTime);
            yield return null; //forces coroutine to come back at this exact line of code on the next frame
        } //this line forces code to check if while statement is still active each frame; then the loop will stop on next yieldS

        hour++;
        if (hour == 13)
        {
            hour = 1;
        }
        OnTheHour.Invoke(hour);

    }

    public void StopTheClock()
    {
        if(clockIsRunning != null)
        {
            StopCoroutine(clockIsRunning);
        }
        if(doOneHour != null)
        {
            StopCoroutine(doOneHour);
        }
       
    }
}
