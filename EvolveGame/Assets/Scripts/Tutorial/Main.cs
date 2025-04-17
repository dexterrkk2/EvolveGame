using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main instance;
    public Web web;
    public UserInfo userInfo;
    public Login login;
    public RegisterAccount registerAccount;
    public CreatureManager creatureManager;
    public GameObject userProfile;
    public GameObject createCreatureScreen;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        web = GetComponent<Web>();
        userInfo = GetComponent<UserInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
