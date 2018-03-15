using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject authMenu;
    public GameObject adminMenu;
    public GameObject userMenu;
    public GameObject errorMenu;

    public InputField[] fields;

    User currUser;

	void Start () {
        Database.users.Add(new User("0","0pass", true, false));
        Database.users.Add(new User("1", "1pass", false, false));
        Database.users.Add(new User("2", "2pass", false, true));
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
    }

    public void ShowUserMenu()
    {
        HideAllMenus();
        userMenu.SetActive(true);
        fields = userMenu.GetComponentsInChildren<InputField>();
        if (currUser != null)
            userMenu.transform.GetComponentInChildren<Text>().text = 
                "Welcome to User Menu, " + currUser.login + "\n\nMoney : " + currUser.money + " " + currUser.currency;
    }

    public void ShowAuthMenu()
    {
        HideAllMenus();
        authMenu.SetActive(true);
        fields = authMenu.GetComponentsInChildren<InputField>();
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
        User _user = Database.FindByLogin(fields[0].text);
        if (_user != null)
        {
            if (_user.pass == fields[1].text)
            {
                currUser = _user;
                Debug.Log("Auth!");
                if (!_user.isBlocked)
                    if (_user.admin) ShowAdminMenu();
                    else ShowUserMenu();
                else SendError("User blocked!");
                return;
            }
            else SendError("Wrong password!");
        }
        else SendError("User does not exist!");
    }

    public void ExitApp()
    {
        Debug.Log("Application quit!");
        Application.Quit();
    }

    

        /*
    static void AdminMenu()
    {

        while (true)
        {
            Console.WriteLine();
            //Console.Clear();
            Console.WriteLine("Здравствуйте админ");
            Console.WriteLine("Если вы хотите посмотреть лист пользователей нажмите 1");
            Console.WriteLine("Если вы хотите добавить нового пользователя нажмите 2");
            Console.WriteLine("Если вы хотите удалить пользователя нажмите 3");
            Console.WriteLine("Если вы хотите заблокировать пользователя нажмите 4");
            Console.WriteLine("Если вы хотите разблокировать пользователя нажмите 5");
            Console.WriteLine("Если вы хотите выйти из аккаунта нажмите 0");

            string a = Console.ReadLine();

            if (a == "1")
            {
                ShowList();
            }
            else
            {
                if (a == "2")
                {
                    AddUser();
                }
                else
                {
                    if (a == "3")
                    {
                        DeleteUser();
                    }
                    else
                    {
                        if (a == "4")
                        {
                            Block();
                        }

                        else
                        {
                            if (a == "5")
                            {
                                Unblock();
                            }

                            else
                            {
                                if (a == "0")
                                {
                                    Console.Clear();
                                    PublicMenu();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("Неправильный ввод!!!");
                                    Console.WriteLine();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    */
    /*
    static void ShowList()
    {
        Console.Clear();
        for (int i = 0; i < UserName.Length; i++)
        {
            Console.WriteLine("Пользователь " + UserName[i]);
            Console.WriteLine("ID пользователя " + i);
            Console.WriteLine("Логин и Пароль пользователя ");
            Console.WriteLine(UserLoginandPassword[i]);
            Console.WriteLine("На его/её счету " + accounts[i] + " долларов");
            Console.WriteLine("Статус " + statuc[i]);
            Console.WriteLine();
        }
    }

    static void AddUser()
    {
        Console.Clear();
        Console.WriteLine("Введите логин нового пользователя ");
        string newUserLogin = Console.ReadLine();

        Console.WriteLine("Введите пароль нового пользователя ");
        string newUserPassword = Console.ReadLine();

        Console.WriteLine("Введите имя пользователя ");
        string NameUser = Console.ReadLine();

        
        Console.WriteLine("Введите начальное количество денег пользователя ");

        int summa = int.Parse(Console.ReadLine());

        Console.WriteLine("Статус Активный");

        /* string cash = Console.ReadLine();

         int money = 0;
         bool result = Int32.TryParse(cash, out money);
         if (result)
         {
             int summa = int.Parse(cash);
             flag = false;
         }
         else
         {
             Console.WriteLine("Неправильный ввод!!!");
         }

        // }
        string newLoginPassword = newUserLogin + " " + newUserPassword;

        string[] newUserLoginandPassword = new string[UserLoginandPassword.Length + 1];

        string[] newUserName = new string[UserName.Length + 1];

        int[] newaccounts = new int[accounts.Length + 1];

        string[] newstatus = new string[statuc.Length + 1];

        for (int i = 0; i < UserName.Length; i++)
        {
            newUserLoginandPassword[i] = UserLoginandPassword[i];

            newUserName[i] = UserName[i];

            newaccounts[i] = accounts[i];

            newstatus[i] = statuc[i];
        }

        newUserLoginandPassword[newUserLoginandPassword.Length - 1] = newLoginPassword;
        UserLoginandPassword = newUserLoginandPassword;

        newUserName[newUserName.Length - 1] = NameUser;
        UserName = newUserName;

        newaccounts[newaccounts.Length - 1] = summa;
        accounts = newaccounts;

        newstatus[newstatus.Length - 1] = "Активный";
        statuc = newstatus;

    }

    static void DeleteUser()
    {
        //bool flag = true;
        //while (flag)
        //{
        Console.Clear();
        Console.WriteLine("Введите ID пользователя, которого нужно удалить ");

        int IdUser = int.Parse(Console.ReadLine());
        if (IdUser >= 0 && IdUser < UserName.Length)
        {

            string[] newUserLoginandPassword = new string[UserLoginandPassword.Length - 1];

            string[] newUserName = new string[UserName.Length - 1];

            int[] newaccounts = new int[accounts.Length - 1];

            string[] newstatus = new string[statuc.Length - 1];

            int j = 0;
            for (int i = 0; i < UserName.Length; i++)
            {
                if (i != IdUser)
                {
                    newUserLoginandPassword[j] = UserLoginandPassword[i];

                    newUserName[j] = UserName[i];

                    newaccounts[j] = accounts[i];

                    newstatus[j] = statuc[i];

                    j++;
                }
            }

            UserLoginandPassword = newUserLoginandPassword;

            UserName = newUserName;

            accounts = newaccounts;

            statuc = newstatus;

            //flag = false;
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Неправильный ввод!!!");
            AdminMenu();

        }
        //}
    }

    static void Block()
    {
        Console.Clear();

        //while (true)
        //{
        Console.WriteLine("Введите ID пользователя, которого нужно заблокировать ");
        int IdUser = int.Parse(Console.ReadLine());

        if (IdUser >= 0 && IdUser < UserName.Length)
        {
            for (int i = 0; i < UserName.Length; i++)
            {
                if (IdUser == i)
                {
                    statuc[i] = "Заблокирован";
                    //Console.Clear();
                    AdminMenu();

                }
            }
        }
        else
        {
            Console.WriteLine("Неправильный ввод!!!");
            AdminMenu();
        }
        //}

    }

    static void Unblock()
    {

        Console.Clear();

        Console.WriteLine("Введите ID пользователя, которого нужно разблокировать ");
        int IdUser = int.Parse(Console.ReadLine());

        if (IdUser >= 0 && IdUser < UserName.Length)
        {
            for (int i = 0; i < UserName.Length; i++)
            {
                if (IdUser == i)
                {
                    statuc[i] = "Активный";
                    //Console.Clear();
                    AdminMenu();

                }
            }
        }
        else
        {
            Console.WriteLine("Неправильный ввод!!!");
            AdminMenu();
        }

    }
        */
}