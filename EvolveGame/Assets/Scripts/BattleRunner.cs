using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRunner : MonoBehaviour
{
    Beast playerBeast;
    Beast opponentBeast;
    GeneManager geneManager;
    public float turnTime;
    public GameObject creatureManager;
    public GameObject geneScreen;
    public GameObject loseScreen;
    // Start is called before the first frame update
    public void Create()
    {
        InvokeRepeating("runTurn", 1, turnTime);
        creatureManager.SetActive(false);
    }
    public void addPlayer(Beast beast)
    {
        playerBeast = beast;
    }
    public void addOpponent(Beast beast)
    {
        opponentBeast = beast;
    }
    void runTurn()
    {
        playerBeast.attackCreature(opponentBeast);
        opponentBeast.attackCreature(playerBeast);
        if (playerBeast.isDead())
        {
            loseGame();
        }
        if (opponentBeast.isDead())
        {
            winGame();
        }
    }
    void winGame()
    {
        playerBeast.onWin();
        opponentBeast.gameObject.SetActive(false);
        geneScreen.SetActive(true);
        CancelInvoke();
        Debug.Log("You have won");
    }
    public Creature getPlayerCreature()
    {
        return playerBeast.getCreature();
    }
    void loseGame()
    {
        opponentBeast.onWin();
        opponentBeast.gameObject.SetActive(false);
        playerBeast.gameObject.SetActive(false);
        loseScreen.SetActive(true);
        CancelInvoke();
        Debug.Log("You have died");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
