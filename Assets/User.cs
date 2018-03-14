using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User {
    string login = "";
    string pass = "";
    bool admin = false;
    bool isBlocked = false;

    public User(string _login, string _pass, bool _admin, bool _isBlocked)
    {
        login = _login;
        pass = _pass;
        admin = _admin;
        isBlocked = _isBlocked;
    }
}
