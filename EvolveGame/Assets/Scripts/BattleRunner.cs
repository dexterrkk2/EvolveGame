using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleRunner : MonoBehaviour
{
    Beast playerBeast;
    Beast opponentBeast;
    GeneManager geneManager;
    public float turnTime;
    public GameObject creatureManager;
    public GameObject geneScreen;
    public GameObject loseScreen;
    public Text log;
    // Start is called before the first frame update
    public void Create()
    {
        InvokeRepeating("runTurn", 1, turnTime);
        creatureManager.SetActive(false);
        log.gameObject.SetActive(true);
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
        Invoke("runOpponent", turnTime);
        Invoke("runPlayer", turnTime * 2);
    }
    public void runOpponent()
    {
        log.text = playerBeast.attackCreature(opponentBeast);
        if (opponentBeast.isDead())
        {
            log.gameObject.SetActive(false);
            winGame();
        }
    }
    public void runPlayer()
    {
        log.text = opponentBeast.attackCreature(playerBeast);
        if (playerBeast.isDead())
        {
            log.gameObject.SetActive(false);
            loseGame();
        }
    }
    public bool hasBoth()
    {
        if (playerBeast && opponentBeast)
        {
            return true;
        }
        return false;
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
