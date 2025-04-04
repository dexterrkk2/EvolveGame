using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureManager : MonoBehaviour
{
    Action<string> createCreatureCallback;
    // Start is called before the first frame update
    void Start()
    {
        createCreatureCallback = (jsonArray) =>
        {
            Debug.Log("helo");
            StartCoroutine(CreateCreaturesRountine(jsonArray));
        };
        createCreature();
    }
    public void createCreature()
    {
        string id = Main.instance.userInfo.getUserID();
        Main.instance.web.getCreatureFromPlayer(id, createCreatureCallback);
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
