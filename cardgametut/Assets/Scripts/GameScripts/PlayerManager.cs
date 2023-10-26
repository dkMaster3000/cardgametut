using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{

    public GameObject Ping;

    public GameObject PlayerArea;
    public GameObject EnemyArea;
    public GameObject PlayerYard;
    public GameObject EnemyYard;

    public GameObject PlayerSlot1;
    public GameObject PlayerSlot2;
    public GameObject PlayerSlot3;
    public GameObject PlayerSlot4;
    public GameObject PlayerSlot5;

    public GameObject EnemySlot1;
    public GameObject EnemySlot2;
    public GameObject EnemySlot3;
    public GameObject EnemySlot4;
    public GameObject EnemySlot5;

    public List<GameObject> PlayerSockets  = new List<GameObject>();
    public List<GameObject> EnemySockets = new List<GameObject>();

    public int CardsPlayed = 0;
    public bool IsMyTurn = false;


    private List<GameObject> cards = new List<GameObject>();

    // Start is called before the first frame update
    public override void OnStartClient()
    {
        base.OnStartClient();

        PlayerArea = GameObject.Find("PlayerArea");
        EnemyArea = GameObject.Find("EnemyArea");
        PlayerYard = GameObject.Find("PlayerYard");
        EnemyYard = GameObject.Find("EnemyYard");

        PlayerSlot1 = GameObject.Find("PlayerSlot1");
        PlayerSlot2 = GameObject.Find("PlayerSlot2");
        PlayerSlot3 = GameObject.Find("PlayerSlot3");
        PlayerSlot4 = GameObject.Find("PlayerSlot4");
        PlayerSlot5 = GameObject.Find("PlayerSlot5");

        EnemySlot1 = GameObject.Find("EnemySlot1");
        EnemySlot2 = GameObject.Find("EnemySlot2");
        EnemySlot3 = GameObject.Find("EnemySlot3");
        EnemySlot4 = GameObject.Find("EnemySlot4");
        EnemySlot5 = GameObject.Find("EnemySlot5");

        PlayerSockets.Add(PlayerSlot1);
        PlayerSockets.Add(PlayerSlot2);
        PlayerSockets.Add(PlayerSlot3);
        PlayerSockets.Add(PlayerSlot4);
        PlayerSockets.Add(PlayerSlot5);

        EnemySockets.Add(EnemySlot1);
        EnemySockets.Add(EnemySlot2);
        EnemySockets.Add(EnemySlot3);
        EnemySockets.Add(EnemySlot4);
        EnemySockets.Add(EnemySlot5);

        if (isClientOnly)
        {
            IsMyTurn = true;
        }
     
    }

    [Server]
    public override void OnStartServer()
    {
        cards.Add(Ping);
    }

    [Command]
    public void CmdDealCards()
    {
        for (var i = 0; i < 5; i++)
        {
            GameObject card = Instantiate(cards[Random.Range(0, cards.Count)], new Vector2(0, 0), Quaternion.identity);
            NetworkServer.Spawn(card, connectionToClient);
            RpcShowCard(card, "Dealt");
        }
    }

    public void PlayCard(GameObject card)
    {
        CmdPlayCard(card);
    }

    [Command]
    void CmdPlayCard(GameObject card)
    {
        RpcShowCard(card, "Played");
    }

    [ClientRpc]
    void RpcShowCard(GameObject card, string type)
    {
        if(type == "Dealt")
        {
            if (isOwned)
            {
                card.transform.SetParent(PlayerArea.transform, false);
            } else
            {
                card.transform.SetParent(EnemyArea.transform, false);
                card.GetComponent<CardFlipper>().Flip();
            }
        }
        else if (type == "Played")
            {

            card.transform.SetParent(PlayerSlot1.transform, false);
            if (!isOwned)
                {
                    card.GetComponent<CardFlipper>().Flip();
                }
              
            }
        
    }
}
