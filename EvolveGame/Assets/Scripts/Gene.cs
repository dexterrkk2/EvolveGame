using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gene
{
    public string id;
    public string name;
    public string effect;
    public int cost;
    public bool recessive;
    public string PartToEffect;
    public string GeneType;
    public string GeneFamily;
    public Gene(JSONObject jsonObject)
    {
        id = jsonObject["id"];
        name = jsonObject["name"];
        effect = jsonObject["effect"];
        cost = jsonObject["Cost"];
        recessive = jsonObject["Recessive"];
        PartToEffect = jsonObject["PartToEffect"];
        GeneType = jsonObject["GeneType"];
        GeneFamily = jsonObject["GeneFamily"];
    }
    public void DebugStats()
    {
        Debug.Log("id: " +id);
        Debug.Log("name: " + name);
        Debug.Log("effect: " + effect);
        Debug.Log("cost: " + cost);
        Debug.Log("Recessive: " + recessive);
        Debug.Log("Part to effect: " + PartToEffect);
        Debug.Log("GeneType: " + GeneType);
        Debug.Log("GeneFamily: " + GeneFamily);
    }
    public void modifyCreature(Creature creature)
    {
        string[] split = effect.Split(' ');
        string effectType = split[0];
        int amount = int.Parse(split[1]);
        if(effectType == "damage")
        {
            creature.damage += amount;
        }
        else if(effectType == "health")
        {
            creature.health += amount;
        }
        else if (effectType == "attackspeed")
        {
            creature.attackSpeed += amount;
        }
        else if (effectType == "breed")
        {
            creature.population *=2;
        }
    }
}
