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
    Action<string> spawnCreatureCallback;
    public GameObject creatureUiObject;
    public GameObject creaturePrefab;
    public List<GameObject> creatureUIList;
    public BattleRunner battleRunner;
    public Transform opponentSpawnPoint;
    public Transform playerSpawnPoint;
    public bool isOpponent = true;
    public int maxCreatureNum;
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
        spawnCreatureCallback = (jsonarray) =>
        {
            StartCoroutine(spawnCreature(jsonarray));
        };
        createCreature();
        Main.instance.web.getMaxNum(CreatureNumCallback);
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
        maxCreatureNum = jsonArray[0].AsObject["Count(*)"];
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
        Main.instance.web.getCreature(spawnCreatureCallback, randomID);
        Main.instance.web.getCreature(spawnCreatureCallback, id);
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
                Debug.Log(itemInfo);
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0].AsObject;
                Debug.Log(itemInfoJson);
            };
            Main.instance.web.getCreature(getItemInfoCallback, creatureID);
            yield return new WaitUntil(() => isdone == true);
            Creature creature = new Creature(itemInfoJson);
            creature.DebugStats();
            GameObject me;
            Beast spawnedCreature;
            if (isOpponent)
            {
                me = Instantiate(creaturePrefab, opponentSpawnPoint);
                spawnedCreature = me.GetComponent<Beast>();
                spawnedCreature.create(creature);
                battleRunner.addOpponent(spawnedCreature);
                isOpponent = false;
            }
            else
            {
                me = Instantiate(creaturePrefab, playerSpawnPoint);
                spawnedCreature = me.GetComponent<Beast>();
                spawnedCreature.create(creature);
                battleRunner.addPlayer(spawnedCreature);
                isOpponent = true;
            }
        }
        yield return new WaitUntil(() => isOpponent == true);
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
            GameObject uiObject = Instantiate(creatureUiObject, transform);
            creatureUIList.Add(uiObject);
            creatureUI creatureUI = uiObject.GetComponent<creatureUI>();
            creatureUI.Create(creature);
            creatureUI.loadCreature.onClick.AddListener(() => spawnRandomCreature(creature.id));
            //creature.ItemID = creatureID;
            //item.ID = inventoryID;
/*
            creatureObject.transform.SetParent(transform);
            creatureObject.transform.localScale = Vector3.one;
            creatureObject.transform.localPosition = Vector3.zero;*/

            
          /*  int imageVer = itemInfoJson["imgVer"].AsInt;

            byte[] bytes = ImageManager.Instance.imageLoad(creatureID, imageVer);
            if (bytes.Length == 0)
            {
                Action<byte[]> getItemIconCallback = (downloadedBytes) =>
                {
                    //Debug.Log(webRequest.downloadHandler.data);

                    //Debug.Log(itemInfo);
                    //creatureObject.transform.Find("Image").GetComponent<Image>().sprite = ImageManager.Instance.convertImage(downloadedBytes);
                    ImageManager.Instance.imageSave(creatureID, downloadedBytes, imageVer);
                    ImageManager.Instance.saveVersionJson();
                    //itemInfoJson = tempArray[0].AsObject;
                    //Debug.Log(itemInfoJson);
                };
                Main.instance.web.getItemImage(creatureID, getItemIconCallback);
            }
            //load from device
            else
            {
                //creatureObject.transform.Find("Image").GetComponent<Image>().sprite = ImageManager.Instance.convertImage(bytes);
            }*/
            //set sell button
            //creatureObject.transform.Find("SellButton").GetComponent<Button>().onClick.AddListener(() =>
            //{
                /*string userId = Main.instance.userInfo.getUserID();
                string itemID = creatureID;
                //string id = inventoryID;
                //GameObject self = creatureObject;
                //Debug.Log(id);
                Main.instance.web.sellItem(userId, itemID, id);*/
                //Destroy(self);
            //});
        }
    }
}
