using System.IO;
using UnityEngine;

public static class DataSerializer {

    static string output;
    static string input;
    static string[] vs;
    static string[] vsvs;
    static string savePath;

    public static void SaveData()
    {
        savePath = Application.persistentDataPath + @"\Users.txt";
        output = "";
        foreach (User item in Database.users)
        {
            output += item.login + "/" + item.pass + "/" + item.isBlocked.ToString() + "/" + item.money + "/" + item.name + "/" + item.surname + "/" + item.patronymic + "|";
        }
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }
        File.WriteAllText(savePath, output);
    }

    public static void LoadData()
    {
        savePath = Application.persistentDataPath + @"\Users.txt";
        if (File.Exists(savePath))
        {
            input = File.ReadAllText(savePath);
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
