using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Beast : MonoBehaviour
{
    public Creature creature;
    public Slider health;
    public Text creatureNameText;
    public Image creatureImage;
    //public string creatureName;
    public void create(Creature creature)
    {
        this.creature = creature;
        Debug.Log("Inside Beast script");
        creature.DebugStats();
        health.maxValue = creature.health;
        health.value = creature.health;
        creatureNameText.text = creature.name;
        getImage();
    }
    public void attackCreature(Beast beast)
    {
        float hitChance = beast.creature.moveSpeed / creature.attackSpeed;
        int rand = Random.Range(0, (int)hitChance) + 1;
        if (rand == 1)
        {
            Debug.Log(name + "hit enemy");
            beast.takeDamage(creature.damage);
        }
        else
        {
            Debug.Log(name + "missed enemy");
        }
    }

    public void takeDamage(int damage)
    {
        creature.health -= damage;
        health.value -= damage;
        if (creature.health <= 0)
        {
            creature.Die();
            gameObject.SetActive(false);
        }
    }
    public void getImage()
    {
        GameObject imageObject = Resources.Load("sprites/" + creature.name) as GameObject;
        //Debug.Log(imageObject);
        if (!imageObject)
        {
            imageObject = Resources.Load("sprites/Troll") as GameObject;
            //Debug.Log(imageObject);
        }
        SpriteRenderer spriteRenderer = imageObject.GetComponent<SpriteRenderer>();
        Sprite sprite = spriteRenderer.sprite;
        creatureImage.sprite = sprite;
    }
    public Creature getCreature() { return this.creature; }
    public void onWin()
    {
        creature.onWin();
    }
    public bool isDead()
    {
        if (creature.health < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
