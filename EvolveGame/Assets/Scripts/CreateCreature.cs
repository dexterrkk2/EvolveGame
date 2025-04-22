using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCreature : MonoBehaviour
{
    public Creature creature;
    public InputField creatureNameInput;
    public InputField creatureDietInput;
    public Button createCreatureButton;
    // Start is called before the first frame update
    void Start()
    {
        createCreatureButton.onClick.AddListener(() => createCreature(creatureNameInput.text, creatureDietInput.text));
    }
    public void createCreature(string name, string diet, int health =5, int damage =1, float moveSpeed =1, float attackSpeed =1, int population =5)
    {
        string id = (CreatureManager.maxCreatureNum +1).ToString();
        Debug.Log(id);
        creature = new Creature(name, health, damage, moveSpeed, attackSpeed, id,population, diet);
        Main.instance.web.createCreatur(name, diet, Main.instance.userInfo.getUserID(), id);
        //Main.instance.userProfile.SetActive(true);
        //assign creature to user
        //go to trait picker
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
