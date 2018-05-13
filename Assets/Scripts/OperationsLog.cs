using System.Collections.Generic;
using System.IO;

public static class OperationsLog {

    static string output;

    struct Log
    {
        public string fromLogin;
        public float money;
        public string toLogin;
        
        public Log(string _from, float _money, string _to) : this()
        {
            fromLogin = _from;
            money = _money;
            toLogin = _to;
        }
    }

    static List<Log> logs = new List<Log>();
    static string input;
    static string[] vs;
    static string[] vsvs;

    public static void SaveData()
    {
        output = "";
        if (logs != null)
        {
            foreach (Log item in logs)
            {
                output += item.fromLogin + "/" + item.money.ToString() + "/" + item.toLogin + "|";
            }
            if (File.Exists(@"C:\Users\Public\Logs.txt"))
            {
                File.Delete(@"C:\Users\Public\Logs.txt");
            }
            File.WriteAllText(@"C:\Users\Public\Logs.txt", output);
        }
    }

    public static void LoadData()
    {
        if (File.Exists(@"C:\Users\Public\Logs.txt"))
        {
            input = File.ReadAllText(@"C:\Users\Public\Logs.txt");
        }
        else
        {
            return;
        }
        logs.Clear();
        vs = input.Split((char)124);
        foreach (string item in vs)
        {
            if (item != null && item != "")
            {
                vsvs = item.Split((char)47);
                logs.Add(new Log(vsvs[0], float.Parse(vsvs[1]), vsvs[2]));
            }
        }
    }

    public static void AddToLog(string _from, float _money, string _to)
    {
        logs.Add(new Log(_from, _money, _to));
    }

    public static string GetUserLog(string _user)
    {
        output = "";
        int i = 1;
        foreach (Log item in logs)
        {
            if (item.fromLogin == _user)
            {
                output += i + ". Переслав " + item.money + " грн користувачу з логіном " + item.toLogin + "\n";
                i++;
            }
            if (item.toLogin == _user)
            {
                output += i + ". Отримав " + item.money + " грн від користувача з логіном " + item.fromLogin + "\n";
                i++;
            }
        }
        if (output == "" || output == null)
        {
            return "Цей користувач ще не робив і не приймав ніяких переказів";
        }
        return output;
    }
}
