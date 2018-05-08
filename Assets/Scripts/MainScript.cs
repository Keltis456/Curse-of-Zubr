using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject authMenu;
    public GameObject singUpMenu;
    public GameObject adminMenu;
    public GameObject userMenu;
    public GameObject errorMenu;

    public InputField[] fields;

    public bool isAuthForAdmin = false;

    User currUser;

    User admin;

    public static MainScript instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start () {
        admin = new User("admin", "admin");
        Database.users.Add(new User("0","0", true));
        Database.users.Add(new User("1", "1", false, 40, "UAH"));
        Database.users.Add(new User("2", "2", false));
        Database.users.Add(new User("3", "3", false, 1000f, "EUR"));
        Database.users.Add(new User("4", "4", true, 9000f, "EUR"));
        ShowMainMenu();
	}

    public void ShowMainMenu()
    {
        HideAllMenus();
        mainMenu.SetActive(true);
        fields = mainMenu.GetComponentsInChildren<InputField>();
    }

    public void ShowAdminMenu()
    {
        HideAllMenus();
        adminMenu.SetActive(true);
        fields = adminMenu.GetComponentsInChildren<InputField>();
        if (currUser != null)
            adminMenu.transform.GetComponentInChildren<Text>().text = 
                "Welcome to Admin Menu, " + currUser.login;
        UserListManager.instance.ListUpdate();
    }

    public void ShowUserMenu()
    {
        HideAllMenus();
        userMenu.SetActive(true);
        fields = userMenu.GetComponentsInChildren<InputField>();
        if (currUser != null)
            userMenu.transform.GetComponentInChildren<Text>().text = 
                "Welcome to User Menu, " + currUser.login 
                + "\n\nВаш логин : " + currUser.login 
                + "\n\nВаш пароль : " + currUser.pass
                + "\n\nБаланс : " + currUser.money + " " + currUser.currency;
    }

    public void ShowAuthMenu()
    {
        HideAllMenus();
        authMenu.SetActive(true);
        fields = authMenu.GetComponentsInChildren<InputField>();
        isAuthForAdmin = false;
    }

    public void ShowAuthAdminMenu()
    {
        HideAllMenus();
        authMenu.SetActive(true);
        fields = authMenu.GetComponentsInChildren<InputField>();
        isAuthForAdmin = true;
    }

    public void AddMoney()
    {
        if(currUser != null)
        {
            try
            {
                if (fields[0].text == "" || fields[0].text == null) return;
                currUser.money += float.Parse(fields[0].text);

            }
            catch (System.Exception exeption)
            {
                SendError(exeption.Message);
                throw;
            }
            
        }
        ShowUserMenu();
    }

    public void WithdrawMoney()
    {
        if (currUser != null)
        {
            try
            {
                if (fields[0].text == "" || fields[0].text == null) return;
                if (currUser.money - float.Parse(fields[0].text) >= 0)
                {
                    currUser.money -= float.Parse(fields[0].text);
                }
                else
                {
                    SendError("Insufficient funds");
                    return;
                }
            }
            catch (System.Exception exeption)
            {
                SendError(exeption.Message);
                throw;
            }
        }
        ShowUserMenu();
    }

    public void HideAllMenus()
    {
        mainMenu.SetActive(false);
        authMenu.SetActive(false);
        adminMenu.SetActive(false);
        userMenu.SetActive(false);
        errorMenu.SetActive(false);
    }

    public void SendError(string _error)
    {
        HideAllMenus();
        errorMenu.SetActive(true);
        if (_error != null) errorMenu.transform.GetComponentInChildren<Text>().text = _error;
    }

    public void Auth()
    {
        HideAllMenus();
        if (isAuthForAdmin)
        {
            if (admin.login == fields[0].text)
            {
                if (admin.pass == fields[1].text)
                {
                    currUser = admin;
                    ShowAdminMenu();
                    return;
                }
                else SendError("Wrong password!");
            }
            else SendError("User does not exist!");
        }
        else
        {
            User _user = Database.FindByLogin(fields[0].text);
            if (_user != null)
            {
                if (_user.pass == fields[1].text)
                {
                    currUser = _user;
                    Debug.Log("Auth!");
                    if (!_user.isBlocked)
                        if (isAuthForAdmin) ShowAdminMenu();
                        else ShowUserMenu();
                    else SendError("User blocked!");
                    return;
                }
                else SendError("Wrong password!");
            }
            else SendError("User does not exist!");
        }
    }

    public void ExitApp()
    {
        Debug.Log("Application quit!");
        Application.Quit();
    }

    public void CreateUser()
    {
        if (fields[0].text == null || fields[0].text == "") return;
        if (fields[1].text == null || fields[1].text == "") return;
        Database.users.Add(new User(fields[0].text, fields[1].text));
        ShowAdminMenu();
    }

    public void BlockUser(string _login)
    {
        Database.FindByLogin(_login).isBlocked = !Database.FindByLogin(_login).isBlocked;
        ShowAdminMenu();
    }

    public void DeleteUser(string _login)
    {
        Database.users.Remove(Database.FindByLogin(_login));
        ShowAdminMenu();
    }
}