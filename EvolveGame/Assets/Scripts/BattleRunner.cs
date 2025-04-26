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
    public GameObject canvas;
    public Text log;
    // Start is called before the first frame update
    public void Create()
    {
        InvokeRepeating("runTurn", turnTime, turnTime);
        creatureManager.SetActive(false);
        canvas.gameObject.SetActive(true);
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
        Main.instance.turnOFFGenes();
        runOpponent();
        Invoke("runPlayer", turnTime);
    }
    public void runOpponent()
    {
        log.text = playerBeast.attackCreature(opponentBeast);
        if (opponentBeast.isDead())
        {
            canvas.SetActive(false);
            winGame();
        }
    }
    public void runPlayer()
    {
        log.text = opponentBeast.attackCreature(playerBeast);
        if (playerBeast.isDead())
        {
            canvas.gameObject.SetActive(false);
            loseGame();
        }
    }
    public bool hasBoth()
    {
        if (hasPlayer() && hasOpponent())
        {
            return true;
        }
        return false;
    }
    public bool hasPlayer()
    {
        if (playerBeast)
        {
            return true;
        }
       return false;
    }
    public bool hasOpponent()
    {
        if (opponentBeast)
        {
            return true;
        }
        return false;
    }
    void winGame()
    {
        CancelInvoke();
        playerBeast.onWin();
        opponentBeast.gameObject.SetActive(false);
        opponentBeast = null;
        Main.instance.turnOnGenes();
        Debug.Log("You have won");
    }
    public Creature getPlayerCreature()
    {
        return playerBeast.getCreature();
    }
    void loseGame()
    {
        CancelInvoke();
        opponentBeast.onWin();
        opponentBeast.gameObject.SetActive(false);
        playerBeast.gameObject.SetActive(false);
        loseScreen.SetActive(true);
        Debug.Log("You have died");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
