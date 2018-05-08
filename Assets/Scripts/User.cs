public class User {
    public string login = "";
    public string pass = "";
    public bool isBlocked = false;
    public float money = 0;
    public string currency = "USD";

    public User(string _login, string _pass)
    {
        login = _login;
        pass = _pass;
        isBlocked = false;
        money = 0;
        currency = "USD";
    }

    public User(string _login, string _pass, bool _isBlocked)
    {
        login = _login;
        pass = _pass;
        isBlocked = _isBlocked;
        money = 0;
        currency = "USD";
    }

    public User(string _login, string _pass, bool _isBlocked, float _money)
    {
        login = _login;
        pass = _pass;
        isBlocked = _isBlocked;

        if (_money > 0) money = _money;
        else money = 0;

        currency = "USD";
    }

    public User(string _login, string _pass, bool _isBlocked, float _money, string _currency)
    {
        login = _login;
        pass = _pass;
        isBlocked = _isBlocked;

        if (_money > 0) money = _money;
        else money = 0;

        currency = _currency;
    }
}
