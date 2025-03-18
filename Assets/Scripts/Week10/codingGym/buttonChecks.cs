using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonChecks : MonoBehaviour
{
    public bananaGrow watermelon;
    public bananaGrow banana;
    public Button bananaButton;
    public Button watermelonButton;

    public bool player1Turn;
    public bool player2Turn;

    public IEnumerator turnChecker;

    // Start is called before the first frame update
    void Start()
    {
        player1Turn = true;
        StartCoroutine(startTurns());
    }

    private IEnumerator startTurns()
    {
        while (true)
        {
            turnChecker = checkTurn();
            yield return StartCoroutine(turnChecker);
        }
    }

    // Update is called once per frame
    public IEnumerator checkTurn()
    {
        while(player1Turn == true)
        {
            bananaButton.interactable = true;
            banana.bananamationStart();
            yield return null;
            player1Turn = false;
            player2Turn = true;
        }
        
        while (player2Turn == true)
        {
            watermelonButton.interactable = false;
            watermelon.bananamationStart();
            yield return null;
            player2Turn = false;
            player1Turn = true;
        }
    }
}
