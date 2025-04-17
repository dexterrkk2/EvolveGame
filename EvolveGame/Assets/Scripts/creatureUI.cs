using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class creatureUI : MonoBehaviour
{
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
        creatureName.text = creature.name;
        creatureDamage.text = "Damage: " + creature.damage;
        creatureHealth.text = "Health: " + creature.health;
        creatureAttackSpeed.text = "AttackSpeed: " + creature.attackSpeed;
        creatureMoveSpeed.text = "MoveSpeed: " + creature.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
