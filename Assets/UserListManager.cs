using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserListManager : MonoBehaviour {

    public GameObject userEntryEthalon;
    List<GameObject> entrys = new List<GameObject>();

    public static UserListManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    // Use this for initialization
    void Start () {
        ListUpdate();
    }

    public void ListUpdate()
    {
        for (int i = 0; i < entrys.Count; i++)
        {
            Destroy(entrys[i]);
        }
        entrys.Clear();
        for (int i = 0; i < Database.users.Count; i++)
        {
            GameObject obj = Instantiate(userEntryEthalon, transform);
            obj.GetComponentInChildren<Text>().text = "User: " + Database.users[i].login + 
                "\nMoney: " + Database.users[i].money + 
                " " + Database.users[i].currency + 
                "\nisAdmin: " + Database.users[i].admin +
                "\nisBlocked: " + Database.users[i].isBlocked;
            string _login = Database.users[i].login;
            obj.GetComponentsInChildren<Button>()[0].onClick.AddListener(delegate { MainScript.instance.BlockUser(_login); } );
            obj.GetComponentsInChildren<Button>()[1].onClick.AddListener(delegate { MainScript.instance.DeleteUser(_login); });
            entrys.Add(obj);
            Debug.Log(obj);
        }
    }
}
