using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureManager : MonoBehaviour
{
    Action<string> createCreatureCallback;
    Action<string> CreatureNumCallback;
    Action<string> spawnSelfCallback;
    Action<string> spawnOpponentCallback;
    public GameObject creatureUiObject;
    public GameObject creaturePrefab;
    public List<GameObject> creatureUIList;
    public BattleRunner battleRunner;
    public GeneManager geneManager;
    public Transform opponentSpawnPoint;
    public Transform playerSpawnPoint;
    public bool isOpponent = true;
    public static int maxCreatureNum;
    // Start is called before the first frame update
    void OnEnable()
    {
        createCreatureCallback = (jsonArray) =>
        {
            //Debug.Log("helo");
            StartCoroutine(CreateCreaturesRountine(jsonArray));
        };
        CreatureNumCallback = (jsonarray) => 
        {
            getCreatureNum(jsonarray);
        };
        spawnSelfCallback = (jsonarray) =>
        {
            StartCoroutine(spawnCreature(jsonarray));
        };
        spawnOpponentCallback = (jsonarray) => 
        { 
            StartCoroutine(nextRound(jsonarray));
        };
        Main.instance.web.getMaxNum(CreatureNumCallback);
        createCreature();
    }
    private void OnDisable()
    {
       for(int i = 0; i < creatureUIList.Count; i++)
        {
            Destroy(creatureUIList[i]);
        }
       creatureUIList.Clear();
    }
    public void createCreature()
    {
        string id = Main.instance.userInfo.getUserID();
        Main.instance.web.getCreatureFromPlayer(id, createCreatureCallback);
    }
    public void getCreatureNum(string jsonString)
    {
        JSONArray jsonArray = JSON.Parse(jsonString) as JSONArray;
        Debug.Log(jsonArray.Count);
        maxCreatureNum = jsonArray[0].AsObject["Max(id)"];
        Debug.Log( "Max Creatures" +maxCreatureNum);
    }
    public void spawnRandomCreature(string id)
    {
        int randomCreature = UnityEngine.Random.Range(1, maxCreatureNum +1);
        int incomingCreature = Convert.ToInt32(id);
        while (randomCreature == incomingCreature)
        {
            randomCreature = UnityEngine.Random.Range(1, maxCreatureNum +1);
        }
        string randomID = randomCreature.ToString();
        Debug.Log("random Creature" + randomID);
        Main.instance.web.getCreature(spawnSelfCallback, id);
        Main.instance.web.getCreature(spawnOpponentCallback, randomID);
    }
    public void spawnEnemy(string id)
    {
        
        int randomCreature = UnityEngine.Random.Range(1, maxCreatureNum + 1);
        int incomingCreature = Convert.ToInt32(id);
        while (randomCreature == incomingCreature)
        {
            randomCreature = UnityEngine.Random.Range(1, maxCreatureNum + 1);
        }
        string randomID = randomCreature.ToString();
        Debug.Log("random Creature" + randomID);
        Main.instance.web.getCreature(spawnOpponentCallback, randomID);
    }
    IEnumerator spawnCreature(string jsonString)
    {
        JSONArray jsonArray = JSON.Parse(jsonString) as JSONArray;
        //Debug.Log(jsonArray.Count);
        for (int i = 0; i < jsonArray.Count; i++)
        {
            bool isdone = false;
            string creatureID = jsonArray[i].AsObject["id"];
            //string inventoryID = jsonArray[i].AsObject["ID"];
            JSONObject itemInfoJson = new JSONObject();

            Action<string> getItemInfoCallback = (itemInfo) =>
            {
                isdone = true;
                //Debug.Log(itemInfo);
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0].AsObject;
                //Debug.Log(itemInfoJson);
            };
            Main.instance.web.getCreature(getItemInfoCallback, creatureID);
            yield return new WaitUntil(() => isdone == true);
            Creature creature = new Creature(itemInfoJson);
            //get creatures genes and apply them
            creature.DebugStats();
            //Debug.Log("isOpponent: " + isOpponent);
            GameObject me;
            me = Instantiate(creaturePrefab, playerSpawnPoint);
            Beast spawnedCreature = me.GetComponent<Beast>();
            spawnedCreature.create(creature);
            battleRunner.addPlayer(spawnedCreature);
            isOpponent = true;
        }
        //yield return new WaitUntil(() => isOpponent == true);
        //battleRunner.Create();
    }
    IEnumerator nextRound(string jsonString)
    {
        JSONArray jsonArray = JSON.Parse(jsonString) as JSONArray;
        //Debug.Log(jsonArray.Count);
        for (int i = 0; i < jsonArray.Count; i++)
        {
            bool isdone = false;
            string creatureID = jsonArray[i].AsObject["id"];
            //string inventoryID = jsonArray[i].AsObject["ID"];
            JSONObject itemInfoJson = new JSONObject();

            Action<string> getItemInfoCallback = (itemInfo) =>
            {
                isdone = true;
                Debug.Log(itemInfo);
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0].AsObject;
                Debug.Log(itemInfoJson);
            };
            Main.instance.web.getCreature(getItemInfoCallback, creatureID);
            yield return new WaitUntil(() => isdone == true);
            Creature creature = new Creature(itemInfoJson);
            creature.DebugStats();
            //get creatures genes and apply them

            Debug.Log("isOpponent: " + isOpponent);
            GameObject me;
       
            isOpponent = false;
            me = Instantiate(creaturePrefab, opponentSpawnPoint);
            Beast spawnedCreature = me.GetComponent<Beast>();
            spawnedCreature.create(creature);
            battleRunner.addOpponent(spawnedCreature);
        }
        yield return new WaitUntil(() => battleRunner.hasBoth());
        isOpponent = true;
        battleRunner.Create();
    }
    IEnumerator CreateCreaturesRountine(string jsonString)
    {
        JSONArray jsonArray = JSON.Parse(jsonString) as JSONArray;
        //Debug.Log(jsonArray.Count);
        for (int i = 0; i < jsonArray.Count; i++)
        {
            bool isdone = false;
            string creatureID = jsonArray[i].AsObject["CreaturID"];
            //string inventoryID = jsonArray[i].AsObject["ID"];
            JSONObject itemInfoJson = new JSONObject();

            Action<string> getItemInfoCallback = (itemInfo) =>
            {
                isdone = true;
                Debug.Log(itemInfo);
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0].AsObject;
                Debug.Log(itemInfoJson);
            };
            Main.instance.web.getCreature(getItemInfoCallback,creatureID);

            //wait for callback
            yield return new WaitUntil(() => isdone == true);
            //Debug.Log("got here");
            //GameObject creatureObject = Instantiate(Resources.Load("Prefabs/item") as GameObject);
            Creature creature = new Creature(itemInfoJson);
            creature.DebugStats();
            //get creatures genes and apply them
            //isdone = false;
            /*Action<string> getgenesCallback = (itemInfo) =>
            {
               geneManager.applyCreaturegenes(itemInfo, creature);

            };
            Main.instance.web.getGenesFromCreature(creatureID, getgenesCallback);
            yield return new WaitUntil(() => isdone == true);*/
           
            //wait for callback
            yield return new WaitUntil(() => isdone == true);
            GameObject uiObject = Instantiate(creatureUiObject, transform);
             creatureUIList.Add(uiObject);
            creatureUI creatureUI = uiObject.GetComponent<creatureUI>();
            creatureUI.Create(creature);
            creatureUI.loadCreature.onClick.AddListener(() => spawnRandomCreature(creature.id));
        }
    }
}
