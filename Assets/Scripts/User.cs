public class User {
    public string name = "";
    public string surname = "";
    public string patronymic = "";
    public string login = "";
    public string pass = "";
    public bool isBlocked = false;
    public float money = 0;
    public string currency = "UAH";

    public User(string _login, string _pass)
    {
        name = "";
        surname = "";
        patronymic = "";
        login = _login;
        pass = _pass;
        isBlocked = false;
        money = 0;
    }

    public User(string _login, string _pass, bool _isBlocked)
    {
        name = "";
        surname = "";
        patronymic = "";
        login = _login;
        pass = _pass;
        isBlocked = _isBlocked;
        money = 0;
    }

    public User(string _login, string _pass, bool _isBlocked, float _money)
    {
        name = "";
        surname = "";
        patronymic = "";
        login = _login;
        pass = _pass;
        isBlocked = _isBlocked;

        if (_money > 0) money = _money;
        else money = 0;
    }

    public User(string _login, string _pass, bool _isBlocked, float _money, string _name, string _surname, string _patronymic)
    {
        name = _name;
        surname = _surname;
        patronymic = _patronymic;
        login = _login;
        pass = _pass;
        isBlocked = _isBlocked;

        if (_money > 0) money = _money;
        else money = 0;
    }
}
