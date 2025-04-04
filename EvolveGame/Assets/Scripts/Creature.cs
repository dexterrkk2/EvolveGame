using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature
{
    public string name;
    public int health;
    public int damage;
    public float attackSpeed;
    public float moveSpeed;
    public List<Gene> Genes;
    public float survivalNum;
    public Creature(string name, int health, int damage, float attackSpeed, float moveSpeed, List<Gene> genes, float survivalNum)
    {
        this.name = name;
        this.health = health;
        this.damage = damage;
        this.attackSpeed = attackSpeed;
        this.moveSpeed = moveSpeed;
        Genes = genes;
        this.survivalNum = survivalNum;
    }
    public Creature(JSONObject jason)
    {
        //Debug.Log(jason);
        name = jason["name"];
        health = jason["Health"];
        damage = jason["Damage"];
        attackSpeed = jason["AttackSpeed"];
        moveSpeed = jason["MoveSpeed"];
        //survivalNum = jason["survivalNum"];
    }
    public void DebugStats()
    {
        Debug.Log("name: " + name);
        Debug.Log("health: " + health);
        Debug.Log("damage: " + damage);
        Debug.Log("attackSpeed: " + attackSpeed);
        Debug.Log("moveSpeed: " + moveSpeed);
    }
}
