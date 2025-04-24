using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneManager : MonoBehaviour
{
    public int numTimes;
    Action<string> creategeneCallback;
    Action<string> geneNumCallback;
    public BattleRunner runner;
    public GameObject geneUiObject;
    public GameObject genePrefab;
    public List<GameObject> geneUIList;
    public static int maxGeneNum;
    // Start is called before the first frame update
    public void OnEnable()
    {
        Debug.Log("GeneManager");
        geneNumCallback = (string jsonstring) =>
        {
            getMaxGeneNum(jsonstring);
            for (int i = 0; i < numTimes; i++)
            {
                CreatReandomGene();
            }
        };
        creategeneCallback = (string jsonstring) =>
        {
            StartCoroutine(CreateGeneRountine(jsonstring, runner.getPlayerCreature()));
        };
        Main.instance.web.getGeneNum(geneNumCallback);
       
    }
    public void OnDisable()
    {
        for (int i = 0; i < geneUIList.Count; i++)
        {
            Destroy(geneUIList[i]);
        }
    }
    public void applyCreaturegenes(string genes, Creature creature)
    {
        StartCoroutine(getGeneRountine(genes, creature));
    }
    public void CreatReandomGene()
    {
        int randomCreature = UnityEngine.Random.Range(1, maxGeneNum + 1);
        string randomID = randomCreature.ToString();
        Debug.Log("random Creature" + randomID);
        Main.instance.web.getGene(creategeneCallback, randomID);
    }
    public void getMaxGeneNum(string jsonString)
    {
        JSONArray jsonArray = JSON.Parse(jsonString) as JSONArray;
        Debug.Log(jsonArray.Count);
        maxGeneNum = jsonArray[0].AsObject["Max(id)"];
        Debug.Log("Max Genes" + maxGeneNum);
    }
    IEnumerator CreateGeneRountine(string jsonString, Creature creature)
    {
        JSONArray jsonArray = JSON.Parse(jsonString) as JSONArray;
        Debug.Log(jsonArray.Count);
        for (int i = 0; i < jsonArray.Count; i++)
        {
            bool isdone = false;
            string GeneID = jsonArray[i].AsObject["id"];
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
            Main.instance.web.getGene(getItemInfoCallback, GeneID);

            //wait for callback
            yield return new WaitUntil(() => isdone == true);
            Debug.Log("got here");
            //GameObject creatureObject = Instantiate(Resources.Load("Prefabs/item") as GameObject);
            Gene gene = new Gene(itemInfoJson);
            gene.DebugStats();
            GameObject uiObject = Instantiate(geneUiObject, transform);
            geneUIList.Add(uiObject);
            GeneUI geneUI = uiObject.GetComponent<GeneUI>();
            geneUI.Create(gene);
            geneUI.acceptGene.onClick.AddListener(() => gene.modifyCreature(creature));
            geneUI.acceptGene.onClick.AddListener(() => Main.instance.web.giveGene(creature.id, gene.id));
            geneUI.acceptGene.onClick.AddListener(() => Main.instance.userProfile.SetActive(true));
            geneUI.acceptGene.onClick.AddListener(() => runner.geneScreen.gameObject.SetActive(false));
            geneUI.acceptGene.onClick.AddListener(() => Main.instance.creatureManager.spawnEnemy(creature.id));
            
        }
    }
    IEnumerator getGeneRountine(string jsonString, Creature creature)
    {
        JSONArray jsonArray = JSON.Parse(jsonString) as JSONArray;
        Debug.Log(jsonArray.Count);
        for (int i = 0; i < jsonArray.Count; i++)
        {
            bool isdone = false;
            string GeneID = jsonArray[i].AsObject["id"];
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
            Main.instance.web.getGene(getItemInfoCallback, GeneID);

            //wait for callback
            yield return new WaitUntil(() => isdone == true);
            Debug.Log("got here");
            //GameObject creatureObject = Instantiate(Resources.Load("Prefabs/item") as GameObject);
            Gene gene = new Gene(itemInfoJson);
            //gene.DebugStats();
            gene.modifyCreature(creature);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
