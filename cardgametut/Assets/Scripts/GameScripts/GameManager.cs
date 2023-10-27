using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public int TurnOrder = 0;
    public string GameState = "Initialize {}";
    public int PlayerBP = 0;
    public int OpponentBP = 0;
    public int PlayerVariables = 0;
    public int OpponentVariables = 0;


    private int ReadyClicks = 0;



    void Start()
    {
       
    }

    public void ChangeGameState(string stateRequest)
    {
        if (stateRequest == "Initialize {}")
        {
            ReadyClicks = 0;
            GameState = "Initialize {}";

        } else if (stateRequest == "Compile {}")
        {
            if(ReadyClicks == 1)
            {
                GameState = "Compile {}";
            }

        }   else if (stateRequest == "Execute {}")
        {
            GameState = "Execute {}";
            TurnOrder = 0;
        }
    }

    public void ChangeReadyClicks()
    {
        ReadyClicks++;
    }

    public void CardPlayed()
    {
        TurnOrder++;
        if(TurnOrder == 10) 
        {
            ChangeGameState("Execute {}");
        }
    }


}
