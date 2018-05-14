using System.Collections.Generic;

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

    public static User FindBySNP(string _snp)
    {
        for (int i = 0; i < users.Count; i++)
        {
            if (users[i].surname + " " + users[i].name + " " + users[i].patronymic == _snp)
            {
                return users[i];
            }
        }
        return null;
    }
}
