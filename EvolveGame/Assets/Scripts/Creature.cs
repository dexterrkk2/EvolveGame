using JetBrains.Annotations;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
[System.Serializable]
public class Creature
{
    public string name;
    public string id;
    public string diet;
    public int health;
    public int damage;
    public int population;
    public float attackSpeed;
    public float moveSpeed;
    public List<Gene> genes;
    public float survivalNum;
    public Creature(string name, int health, int damage, float attackSpeed, float moveSpeed, List<Gene> genes, float survivalNum, string id)
    {
        this.name = name;
        this.health = health;
        this.damage = damage;
        this.attackSpeed = attackSpeed;
        this.moveSpeed = moveSpeed;
        this.genes = genes;
        this.survivalNum = survivalNum;
    }
    public Creature(string name, int health, int damage, float attackSpeed, float moveSpeed, string id)
    {
        this.name = name;
        this.health = health;
        this.damage = damage;
        this.attackSpeed = attackSpeed;
        this.moveSpeed = moveSpeed;
        this.id = id;
    }
    public Creature(JSONObject jason)
    {
        //Debug.Log(jason);
        name = jason["name"];
        health = jason["Health"];
        damage = jason["Damage"];
        attackSpeed = jason["AttackSpeed"];
        moveSpeed = jason["MoveSpeed"];
        id = jason["id"];
        population = jason["Population"];
        diet = jason["diet"];
        //survivalNum = jason["survivalNum"];
    }
    public void DebugStats()
    {
        Debug.Log("name: " + name);
        Debug.Log("health: " + health);
        Debug.Log("damage: " + damage);
        Debug.Log("attackSpeed: " + attackSpeed);
        Debug.Log("moveSpeed: " + moveSpeed);
        Debug.Log("ID:" + id);
        Debug.Log("population: " + population);
    }
    public void addGene(Gene gene)
    {
        genes.Add(gene);
    }
    public int getDamage() { return damage; }
    public float getAttackSpeed() { return attackSpeed; }
    public float getMoveSpeed() { return moveSpeed; }
    public string getId() { return id; }
    public void Die()
    {
        population--;
    }
    public void onWin()
    {
        if(diet != "plants")
        {
            population++;
        }
        else
        {
            //passive herbivore population buff

        }
    }
}
