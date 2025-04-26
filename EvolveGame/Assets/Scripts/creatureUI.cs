using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class creatureUI : MonoBehaviour
{
    public Creature creature;
    public Image creatureimage;
    public Text creatureName;
    public Text creatureDamage;
    public Text creatureHealth;
    public Text creatureAttackSpeed;
    public Text creatureMoveSpeed;
    public Button loadCreature;
    // Start is called before the first frame update
    public void Create(Creature creature)
    {
        //define get image
        this.creature = creature;
        creature.DebugStats();
        Invoke("Refresh", 1);
    }
    public void Refresh()
    {
        creatureName.text = creature.name;
        creatureDamage.text = "Damage: " + creature.damage;
        creatureHealth.text = "Health: " + creature.health;
        creatureAttackSpeed.text = "AttackSpeed: " + creature.attackSpeed;
        creatureMoveSpeed.text = "MoveSpeed: " + creature.moveSpeed;
        getImage();
    }
    public void getImage()
    {
        string path = "sprites/" + creatureName.text;
        Debug.Log(path);
        GameObject imageObject = Resources.Load(path) as GameObject;
        Debug.Log(imageObject);
        if (!imageObject)
        {
            imageObject = Resources.Load("sprites/Troll") as GameObject;
            Debug.Log(imageObject);
        }
        SpriteRenderer spriteRenderer = imageObject.GetComponent<SpriteRenderer>();
        Sprite sprite = spriteRenderer.sprite;
        creatureimage.sprite = sprite;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
