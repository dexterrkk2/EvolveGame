using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{
    //public creatureUI test;
    void Start()
    {
        getDate();
    }
    public void showUserItems(System.Action<string> CallBack)
    {
        StartCoroutine(GetItemIds("http://localhost/UnityBackendTutorial/getitemids.php", Main.instance.userInfo.getUserID(), CallBack, true));
        //StartCoroutine(GetItemIds("http://evolvegame.iceiy.com/getitemids.php", Main.instance.userInfo.getUserID(), CallBack, true));6

    }
    public void getItemImage(string itemID, System.Action<byte[]> callback)
    {
        StartCoroutine(GetItemIcon("http://localhost/UnityBackendTutorial/GetItemIcon.php", itemID, callback));
    }
    public void getitem(System.Action<string> CallBack, string itemId)
    {
        StartCoroutine(GetItemIds("http://localhost/UnityBackendTutorial/getitem.php", itemId, CallBack, false));
        //StartCoroutine(GetItemIds("http://evolvegame.iceiy.com/getitem.php", itemId, CallBack, false));
    }
    public void getCreature(System.Action<string> CallBack, string creatureId)
    {
        StartCoroutine(GetCreatureId("http://localhost/EvolveGame/GetCreature.php", creatureId, CallBack));
        //StartCoroutine(GetItemIds("http://evolvegame.iceiy.com/getitem.php", itemId, CallBack, false));
    }
    public void getGene(System.Action<string> CallBack, string geneId)
    {
        StartCoroutine(GetGen("http://localhost/EvolveGame/getGene.php", geneId, CallBack));
        //StartCoroutine(GetItemIds("http://evolvegame.iceiy.com/getitem.php", itemId, CallBack, false));
    }
    public void sellItem( string userId, string itemId, string inventoryID)
    {
        StartCoroutine(SellItem("http://localhost/UnityBackendTutorial/SellItem.php",userId, itemId, inventoryID));
        //StartCoroutine(SellItem("http://evolvegame.iceiy.com/SellItem.php", userId, itemId, inventoryID));
    }
    public void getCreatureFromPlayer(string userID, Action<string> callback )
    {
        StartCoroutine(GetplayersCreature("http://localhost/EvolveGame/getCreaturesFromUser.php", userID, callback));
    }
    public void getUsers()
    {
        StartCoroutine(GetRequest("http://localhost/UnityBackendTutorial/getUsers.php"));
        //StartCoroutine(GetRequest("http://evolvegame.iceiy.com/getUsers.php"));
    }
    public void getDate()
    {
        StartCoroutine(GetRequest("http://localhost/EvolveGame/getDate.php"));
        //StartCoroutine(GetRequest("http://evolvegame.iceiy.com/getDate.php"));

    }
    public void getMaxNum(System.Action<string> CallBack)
    {
        StartCoroutine(GetCreatureNum("http://localhost/EvolveGame/getMaxCreatureNum.php",CallBack));
        //StartCoroutine(GetRequest("http://evolvegame.iceiy.com/getDate.php"));

    }
    public void getGeneNum(System.Action<string> CallBack)
    {
        StartCoroutine(GetGeneNum("http://localhost/EvolveGame/getMaxGeneNum.php", CallBack));
        //StartCoroutine(GetRequest("http://evolvegame.iceiy.com/getDate.php"));
    }
    public void createCreatur(string creatureName, string creatureDiet, string userID, string creatureID)
    {
        StartCoroutine(createCreature(creatureName, creatureDiet, userID, creatureID, "http://localhost/EvolveGame/createCreature.php"));
    }
    public void registerUser(string username, string displayName, string password,string confirmPassword)
    {
        if (password == confirmPassword)
        {
            StartCoroutine(register(username, password,displayName, "http://localhost/EvolveGame/RegisterUser.php"));
        }
        else if(username == displayName)
        {
            Debug.Log("usernmame cannot be display name");
        }
        else
        {
            Debug.Log("Passwords must match");
        }
    }
    public void giveGene(string creatureid, string geneID)
    {
        StartCoroutine(AddGene(geneID, creatureid, "http://localhost/EvolveGame/AddGeneToCreature.php"));
    }
    public void loginCall(string username, string password)
    {
        StartCoroutine(login(username, password, "http://localhost/EvolveGame/login.php"));
        //StartCoroutine(login(username, password, "http://evolvegame.iceiy.com/login.php"));
    }
    void aeonwebRequest(UnityWebRequest webRequest)
    {
        webRequest.SetRequestHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
        webRequest.SetRequestHeader("Accept-Encoding", "gzip, deflate");
        webRequest.SetRequestHeader("Accept-Language", "en");
        webRequest.SetRequestHeader("Cache-Control", "max-age=0");
        webRequest.SetRequestHeader("Cookie", "__test=d4f16507ae75e677830d2f5a3f570eca");
        webRequest.SetRequestHeader("Upgrade-Insecure-Requests", "1");
        webRequest.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 11.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
        webRequest.SetRequestHeader("Referer", "http://evolvegame.iceiy.com");
    }
    IEnumerator GetItemIcon(string url, string itemID, System.Action<byte[]> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);
        //Debug.Log("Get item icon");
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
        {
            yield return webRequest.SendWebRequest();
            if(webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                Debug.Log("Called server to get image" + itemID);
                byte[] bytes = webRequest.downloadHandler.data;
               
                callback(bytes);
            }
        }
    }
    IEnumerator GetRequest(string uri)
    {

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            //aeonwebRequest(webRequest);
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    Debug.Log(webRequest.downloadHandler.text);
                    break;
            }
        }
    }
    IEnumerator GetCreatureNum(string uri, System.Action<string> CallBack)
    {

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            //aeonwebRequest(webRequest);
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    Debug.Log(webRequest.downloadHandler.text);
                    CallBack(webRequest.downloadHandler.text);
                    break;
            }
        }
    }
    IEnumerator GetGeneNum(string uri, System.Action<string> CallBack)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            //aeonwebRequest(webRequest);
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    Debug.Log(webRequest.downloadHandler.text);
                    CallBack(webRequest.downloadHandler.text);
                    break;
            }
        }
    }
    IEnumerator GetItemIds(string uri, string userID, System.Action<string> CallBack, bool items)
    {
        Debug.Log("got items");
        WWWForm form = new WWWForm();
        if(items)
        {
            form.AddField("user", userID);
        }
        else
        {
            form.AddField("itemID", userID);
        }
        //Debug.Log(userID);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            //aeonwebRequest(webRequest);
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    Debug.Log(webRequest.downloadHandler.text);
                    string jsonArray = webRequest.downloadHandler.text;
                    CallBack(jsonArray);
                    break;
            }
        }
    }
    IEnumerator GetCreatureId(string uri, string creature, System.Action<string> CallBack)
    {
        Debug.Log("got creatures");
        WWWForm form = new WWWForm();
        Debug.Log("Creature " +creature);
        form.AddField("creatureID", creature);
        //Debug.Log(userID);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            //aeonwebRequest(webRequest);
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    Debug.Log(webRequest.downloadHandler.text);
                    string jsonArray = webRequest.downloadHandler.text;
                    CallBack(jsonArray);
                    break;
            }
        }
    }
    IEnumerator GetGen(string uri, string geneID, System.Action<string> CallBack)
    {
        Debug.Log("got creatures");
        WWWForm form = new WWWForm();
        Debug.Log("Gene " + geneID);
        form.AddField("id", geneID);
        //Debug.Log(userID);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            //aeonwebRequest(webRequest);
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    Debug.Log(webRequest.downloadHandler.text);
                    string jsonArray = webRequest.downloadHandler.text;
                    CallBack(jsonArray);
                    break;
            }
        }
    }
    IEnumerator GetplayersCreature(string uri, string user, System.Action<string> CallBack)
    {
        Debug.Log("got creatures");
        WWWForm form = new WWWForm();
        form.AddField("userID", user);
        //Debug.Log(userID);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            //aeonwebRequest(webRequest);
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    Debug.Log(webRequest.downloadHandler.text);
                    string jsonArray = webRequest.downloadHandler.text;
                    Debug.Log(jsonArray);
                    if (jsonArray == "0 creatures")
                    {
                        Main.instance.createCreatureScreen.gameObject.SetActive(true);
                    }
                    else
                    {
                        CallBack(jsonArray);
                    }
                    break;
            }
        }
    }
    IEnumerator SellItem(string uri, string userID, string itemID, string inventoryID)
    {
        //Debug.Log("got items");
        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);
        form.AddField("userID",userID);
        form.AddField("ID", inventoryID);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            //aeonwebRequest(webRequest);
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    Debug.Log(webRequest.downloadHandler.text);
                    break;
            }
        }
    }
    IEnumerator login(string username, string password, string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            //aeonwebRequest(www);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                //Callback Function to get resulrs
                string id = www.downloadHandler.text;
                Main.instance.userInfo.setInfo(id, username, password);
                if (id != "wrong creditinals" && id != "user does not exit")
                {
                    //LoginCorrect
                    Main.instance.userProfile.SetActive(true);
                    Main.instance.login.gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("try again");
                }
            }
        }
    }
    IEnumerator createCreature(string creaturename, string diet, string userId, string creatureID, string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("creatureName", creaturename);
        form.AddField("CreatureDiet", diet);
        form.AddField("PlayerID", userId);
        form.AddField("CreaturID", creatureID);
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            //aeonwebRequest(www);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                //Callback Function to get resulrs
                string id = www.downloadHandler.text;
                //Main.instance.userInfo.setInfo(id, creaturename, diet);
                if (id != "wrong creditinals" && id != "user does not exit" && id != "creature already taken")
                {
                    id = creatureID;
                    //LoginCorrect

                    Main.instance.userProfile.SetActive(true);
                    //Main.instance.userInfo.setId(id);
                    Main.instance.createCreatureScreen.gameObject.SetActive(false);
                    //assign creature to user could be in other script
                }
                else
                {
                    Debug.Log("did not add");
                }
            }
        }
    }
    IEnumerator AddGene(string geneId, string creatureID, string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("CreaturID", creatureID);
        form.AddField("geneID", geneId);
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            //aeonwebRequest(www);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                //Callback Function to get resulrs
                string id = www.downloadHandler.text;
                //Main.instance.userInfo.setInfo(id, creaturename, diet);
                if (id != "wrong creditinals" && id != "user does not exit" && id != "did not give gene")
                {
                    //id = creatureID;
                    //LoginCorrect
                    Debug.Log("worked");
                    //Main.instance.userProfile.SetActive(true);
                    //Main.instance.userInfo.setId(id);
                    //Main.instance.createCreatureScreen.gameObject.SetActive(false);
                    //assign creature to user could be in other script
                }
                else
                {
                    Debug.Log("try again");
                }
            }
        }
    }
    IEnumerator register(string username, string password, string displayName, string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);
        form.AddField("displayName", displayName);
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            //aeonwebRequest(www);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                //Callback Function to get resulrs
                string id = www.downloadHandler.text;
                Main.instance.userInfo.setInfo(id, username, password);
                if (id != "wrong creditinals" && id != "user does not exit")
                {
                    //start new creature creation
                    //loginCall(username, password);
                    Main.instance.createCreatureScreen.gameObject.SetActive(true);
                    Main.instance.registerAccount.gameObject.SetActive(false);
                    Main.instance.userProfile.gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("try again");
                }
            }
        }
    }
}
