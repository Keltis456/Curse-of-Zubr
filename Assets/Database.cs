using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Database {
    public static List<User> users = new List<User>();

    public static User FindByLogin(string _login)
    {
        for (int i = 0; i < users.Count; i++)
        {
            if (users[i].login == _login)
            {
                return users[i];
            }
        }
        return null;
    }
}
