using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public static class OperationsLog {

    static string output;

    struct Log
    {
        public string fromLogin;
        public float money;
        public string toLogin;
        public DateTime dateTime;
        
        public Log(string _from, float _money, string _to) : this()
        {
            fromLogin = _from;
            money = _money;
            toLogin = _to;
            dateTime = DateTime.Now;
        }

        public Log(string _from, float _money, string _to, string _dateTime) : this()
        {
            fromLogin = _from;
            money = _money;
            toLogin = _to;
            dateTime = DateTime.Parse(_dateTime);
        }
    }

    static List<Log> logs = new List<Log>();
    static string input;
    static string[] vs;
    static string[] vsvs;
    static string savePath;

    public static void SaveData()
    {
        savePath = Application.persistentDataPath + @"\Logs.txt";
        output = "";
        if (logs != null)
        {
            foreach (Log item in logs)
            {
                output += item.fromLogin + "//" + item.money.ToString() + "//" + item.toLogin + "//" + item.dateTime.ToString() + "|";
            }
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
            }
            File.WriteAllText(savePath, output);
        }
    }

    public static void LoadData()
    {
        savePath = Application.persistentDataPath + @"\Logs.txt";
        if (File.Exists(savePath))
        {
            input = File.ReadAllText(savePath);
        }
        else
        {
            return;
        }
        string[] separator = new string[1];
        separator[0] = "//";
        logs.Clear();
        vs = input.Split((char)124);
        foreach (string item in vs)
        {
            if (item != null && item != "")
            {
                vsvs = item.Split(separator, StringSplitOptions.None);
                logs.Add(new Log(vsvs[0], float.Parse(vsvs[1]), vsvs[2], vsvs[3]));
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
            if (item.fromLogin == item.toLogin && item.toLogin == _user)
            {
                if(item.money >= 0)
                {
                    output += i + ". Поклав " + item.money + " грн " + item.dateTime.ToString() + "\n";
                }
                else
                {
                    output += i + ". Зняв " + item.money * (-1) + " грн " + item.dateTime.ToString() + "\n";
                }
                
                i++;
            }
            else
            {
                if (item.fromLogin == _user)
                {
                    output += i + ". Переслав " + item.money + " грн користувачу з логіном " + item.toLogin + " "
                        + Database.FindByLogin(item.toLogin).surname + " " 
                        + Database.FindByLogin(item.toLogin).name + " " 
                        + Database.FindByLogin(item.toLogin).patronymic + " " 
                        + item.dateTime.ToString() + "\n";
                    i++;
                }
                if (item.toLogin == _user)
                {
                    output += i + ". Отримав " + item.money + " грн від користувача з логіном " + item.fromLogin + " " 
                        + Database.FindByLogin(item.fromLogin).surname + " " 
                        + Database.FindByLogin(item.fromLogin).name + " " 
                        + Database.FindByLogin(item.fromLogin).patronymic + " " 
                        + item.dateTime.ToString() + "\n";
                    i++;
                }
            }
        }
        if (output == "" || output == null)
        {
            return "Цей користувач ще не робив і не приймав ніяких переказів";
        }
        return output;
    }
}
