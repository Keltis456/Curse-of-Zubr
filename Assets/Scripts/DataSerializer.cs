using System.IO;

public static class DataSerializer {

    static string output;
    static string input;
    static string[] vs;
    static string[] vsvs;

    public static void SaveData()
    {
        output = "";
        foreach (User item in Database.users)
        {
            output += item.login + "/" + item.pass + "/" + item.isBlocked.ToString() + "/" + item.money + "/" + item.name + "/" + item.surname + "/" + item.patronymic + "|";
        }
        if (File.Exists(@"C:\Users\Public\Users.txt"))
        {
            File.Delete(@"C:\Users\Public\Users.txt");
        }
        File.WriteAllText(@"C:\Users\Public\Users.txt", output);
    }

    public static void LoadData()
    {
        if (File.Exists(@"C:\Users\Public\Users.txt"))
        {
            input = File.ReadAllText(@"C:\Users\Public\Users.txt");
        }
        else
        {
            return;
        }
        Database.users.Clear();
        vs = input.Split((char)124);
        foreach (string item in vs)
        {
            if (item != null && item != "")
            {
                vsvs = item.Split((char)47);
                Database.users.Add(new User(vsvs[0], vsvs[1], bool.Parse(vsvs[2]), float.Parse(vsvs[3]), vsvs[4], vsvs[5], vsvs[6]));
            }
        }
    }
}
