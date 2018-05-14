using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour {

    #region ShowInEditor
    public GameObject mainMenu;
    public GameObject authMenu;
    public GameObject singUpMenu;
    public GameObject adminMenu;
    public GameObject userMenu;
    public GameObject errorMenu;
    public GameObject transactionMenu;
    public GameObject dialogMenu;
    public GameObject logMenu;
    #endregion

    #region Private fields
    InputField[] fields;
    bool isAuthForAdmin = false;
    bool isAddMoney;
    Dropdown dropdown;

    Menu currentMenu;
    Menu lastMenu;

    User currUser;
    User admin;
    #endregion

    #region Singleton
    public static MainScript instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    #endregion

    #region Unity methods
    void Start () {
        admin = new User("admin", "admin");
        Database.users.Add(new User("0", "0", true, 50f, "Василий", "Васильев", "Васильевич"));
        Database.users.Add(new User("1", "1", false, 40f, "Евгений", "Евгеньев", "Евгеньевич"));
        Database.users.Add(new User("2", "2", false, 0f, "Максим", "Максимов", "Максимович"));
        Database.users.Add(new User("3", "3", false, 1000f, "Назар", "Кактус", "Вениаминович"));
        Database.users.Add(new User("4", "4", true, 9000f, "Карл", "Мягкий", "Васильевич"));
        DataSerializer.LoadData();
        OperationsLog.LoadData();
        ShowMainMenu();
	}

    private void OnApplicationFocus(bool focus)
    {
        if (focus == false)
        {
            DataSerializer.SaveData();
            OperationsLog.SaveData();
        }
    }

    private void OnApplicationQuit()
    {
        DataSerializer.SaveData();
        OperationsLog.SaveData();
    }
    #endregion

    #region MenuDisplay
    public void ShowMainMenu()
    {
        if (fields != null)
        {
            foreach (InputField item in fields)
            {
                item.text = "";
            }
        }
        HideAllMenus();
        mainMenu.SetActive(true);
        fields = mainMenu.GetComponentsInChildren<InputField>();
        lastMenu = currentMenu;
        currentMenu = Menu.mainMenu;
        ClearFields();
    }

    public void ShowAdminMenu()
    {
        HideAllMenus();
        adminMenu.SetActive(true);
        fields = adminMenu.GetComponentsInChildren<InputField>();
        if (currUser != null)
            adminMenu.transform.GetComponentInChildren<Text>().text =
                "Ласкаво просимо в адмін меню!";
        UserListManager.instance.ListUpdate();
        lastMenu = currentMenu;
        currentMenu = Menu.adminMenu;
        ClearFields();
    }

    public void ShowUserMenu()
    {
        HideAllMenus();
        userMenu.SetActive(true);
        fields = userMenu.GetComponentsInChildren<InputField>();
        if (currUser != null)
            userMenu.transform.GetComponentInChildren<Text>().text =
                "Ласкаво просимо в меню користувача, " + currUser.surname + " " + currUser.name + " " + currUser.patronymic
                + "\nВаш логін : " + currUser.login 
                + "\nВаш пароль : " + currUser.pass
                + "\nБаланс : " + currUser.money + " " + currUser.currency;
        lastMenu = currentMenu;
        currentMenu = Menu.userMenu;
        ClearFields();
    }

    public void ShowAuthMenu()
    {
        HideAllMenus();
        authMenu.SetActive(true);
        fields = authMenu.GetComponentsInChildren<InputField>();
        isAuthForAdmin = false;
        lastMenu = currentMenu;
        currentMenu = Menu.authMenu;
        ClearFields();
    }

    public void ShowSingUpMenu()
    {
        HideAllMenus();
        singUpMenu.SetActive(true);
        fields = singUpMenu.GetComponentsInChildren<InputField>();
        lastMenu = currentMenu;
        currentMenu = Menu.singUpMenu;
        ClearFields();
    }

    public void ShowAuthAdminMenu()
    {
        HideAllMenus();
        authMenu.SetActive(true);
        fields = authMenu.GetComponentsInChildren<InputField>();
        isAuthForAdmin = true;
        lastMenu = currentMenu;
        currentMenu = Menu.authAdminMenu;
        ClearFields();
    }
    
    public void ShowTransactionMenu()
    {
        transactionMenu.SetActive(true);
        transactionMenu.GetComponentsInChildren<Text>()[0].text = "На вашому рахунку : " + currUser.money.ToString() + " " + currUser.currency;
        fields = transactionMenu.GetComponentsInChildren<InputField>();
        dropdown = transactionMenu.GetComponentsInChildren<Dropdown>()[0];
        dropdown.options.Clear();
        foreach (User item in Database.users)
        {
            if (item != currUser)
            {
                dropdown.options.Add(new Dropdown.OptionData(item.surname + " " + item.name + " " + item.patronymic));
            }
        }
        ClearFields();
    }

    public void HideTransactionMenu()
    {
        transactionMenu.SetActive(false);
    }

    public void ShowDialogMenu(bool _isAddMoney)
    {
        dialogMenu.SetActive(true);
        isAddMoney = _isAddMoney;
        fields = dialogMenu.GetComponentsInChildren<InputField>();
        dialogMenu.GetComponentsInChildren<Text>()[0].text = "На вашому рахунку : " + currUser.money.ToString() + " " + currUser.currency;
        if (isAddMoney) dialogMenu.GetComponentsInChildren<Text>()[1].text = "Покласти гроші";
        else dialogMenu.GetComponentsInChildren<Text>()[1].text = "Зняти гроші";
        ClearFields();
    }

    public void HideDialogMenu()
    {
        dialogMenu.SetActive(false);
    }

    public void ShowLogMenu(string _user)
    {
        logMenu.SetActive(true);
        if (_user != null) logMenu.transform.GetComponentInChildren<Text>().text = OperationsLog.GetUserLog(_user);
        ClearFields();
    }

    public void HideLogMenu()
    {
        logMenu.SetActive(false);
    }

    public void HideAllMenus()
    {
        mainMenu.SetActive(false);
        authMenu.SetActive(false);
        adminMenu.SetActive(false);
        userMenu.SetActive(false);
        errorMenu.SetActive(false);
        singUpMenu.SetActive(false);
        transactionMenu.SetActive(false);
        dialogMenu.SetActive(false);
        logMenu.SetActive(false);
        ClearFields();
    }
    #endregion

    #region UserOptions
    public void AddMoney()
    {
        if(currUser != null)
        {
            try
            {
                if (fields[0].text == "" || fields[0].text == null) return;
                if (float.Parse(fields[0].text) < 0)
                {
                    SendError("Введіть позитивне значення");
                    return;
                }
                currUser.money += float.Parse(fields[0].text);

            }
            catch (System.Exception exeption)
            {
                SendError(exeption.Message);
                throw;
            }

        }
        HideDialogMenu();
    }

    public void WithdrawMoney()
    {
        if (currUser != null)
        {
            try
            {
                if (fields[0].text == "" || fields[0].text == null) return;
                if (float.Parse(fields[0].text) < 0)
                {
                    SendError("Введіть позитивне значення");
                    return;
                }
                if (currUser.money >= float.Parse(fields[0].text))
                {
                    currUser.money -= float.Parse(fields[0].text);
                }
                else
                {
                    SendError("Недостатньо коштів на рахунку");
                    return;
                }
            }
            catch (System.Exception exeption)
            {
                SendError(exeption.Message);
                throw;
            }
        }
        HideDialogMenu();
    }
    
    public void ChangeMoney()
    {
        if (isAddMoney)
        {
            AddMoney();
        }
        else
        {
            WithdrawMoney();
        }
        if (currUser != null)
            userMenu.transform.GetComponentInChildren<Text>().text =
                "Ласкаво просимо в меню користувача, " + currUser.surname + " " + currUser.name + " " + currUser.patronymic
                + "\nВаш логін : " + currUser.login
                + "\nВаш пароль : " + currUser.pass
                + "\nБаланс : " + currUser.money + " " + currUser.currency;
        SendError("Операція пройшла успішно!\nТепер на вашому рахунку : " + currUser.money + " " + currUser.currency);
    }

    public void TransactionMoney()
    {
        if (fields[0].text == null || fields[0].text == "") return;
        if (dropdown == null) return;
        if (float.Parse(fields[0].text) < 0)
        {
            SendError("Введіть позитивне значення");
            return;
        }
        if (currUser.money < float.Parse(fields[0].text))
        {
            SendError("Недостатньо коштів на рахунку");
            return;
        }
        if (currUser == Database.FindBySNP(dropdown.options[dropdown.value].text))
        {
            SendError("Ви не можете перевести гроші самому собі");
            return;
        }
        currUser.money -= float.Parse(fields[0].text);
        Database.FindBySNP(dropdown.options[dropdown.value].text).money += float.Parse(fields[0].text);
        OperationsLog.AddToLog(currUser.login, float.Parse(fields[0].text), Database.FindBySNP(dropdown.options[dropdown.value].text).login);
        HideTransactionMenu();
        if (currUser != null)
            userMenu.transform.GetComponentInChildren<Text>().text =
                "Ласкаво просимо в меню користувача, " + currUser.surname + " " + currUser.name + " " + currUser.patronymic
                + "\nВаш логін : " + currUser.login
                + "\nВаш пароль : " + currUser.pass
                + "\nБаланс : " + currUser.money + " " + currUser.currency;
        SendError("Операція пройшла успішно!\nТепер на вашому рахунку : " + currUser.money + " " + currUser.currency);
    }
    #endregion

    #region ErrorMenus
    public void SendError(string _error)
    {
        errorMenu.SetActive(true);
        if (_error != null) errorMenu.transform.GetComponentInChildren<Text>().text = _error;
    }

    public void HideError()
    {
        errorMenu.SetActive(false);
    }
    #endregion

    #region AdminOptions
    public void Auth()
    {
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
                else SendError("Неправильний пароль!");
            }
            else SendError("Користувача не існує!");
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
                    else SendError("Користувач заблокован!");
                    return;
                }
                else SendError("Неправильний пароль!");
            }
            else SendError("Користувача не існує!");
        }
    }
    
    public void CreateUser()
    {
        if (fields[0].text == null || fields[0].text == "") return;
        if (fields[1].text == null || fields[1].text == "") return;
        if (fields[0].text == null || fields[2].text == "") return;
        if (fields[1].text == null || fields[3].text == "") return;
        if (fields[1].text == null || fields[4].text == "") return;
        Database.users.Add(new User(fields[3].text, fields[4].text, false, 0, fields[0].text, fields[1].text, fields[2].text));
        BackToLastMenu();
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
    #endregion

    #region SystemOptions
    public void ClearFields()
    {
        if (fields != null)
            foreach (InputField item in fields)
                item.text = "";
    }

    public void BackToLastMenu()
    {
        ClearFields();
        switch (lastMenu)
        {
            case Menu.mainMenu:
                ShowMainMenu();
                break;
            case Menu.authMenu:
                ShowAuthMenu();
                break;
            case Menu.authAdminMenu:
                ShowAuthAdminMenu();
                break;
            case Menu.singUpMenu:
                ShowSingUpMenu();
                break;
            case Menu.adminMenu:
                ShowAdminMenu();
                break;
            case Menu.userMenu:
                ShowUserMenu();
                break;
            case Menu.errorMenu:
                ShowMainMenu();
                break;
            default:
                ShowMainMenu();
                break;
        }
    }

    public void ExitApp()
    {
        Debug.Log("Application quit!");
        Application.Quit();
    }
    #endregion
}

public enum Menu
{
    mainMenu,
    authMenu,
    authAdminMenu,
    singUpMenu,
    adminMenu,
    userMenu,
    errorMenu
}