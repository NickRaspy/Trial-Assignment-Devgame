using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ScoreRecord
{
    public static int GetRecord()
    {
        if (PlayerPrefs.HasKey("Record")) return PlayerPrefs.GetInt("Record");
        else if (File.Exists(Application.persistentDataPath + "/data.json")) return JsonUtility.FromJson<Record>(File.ReadAllText(Application.persistentDataPath + "/data.json")).record;
        else return 0;
    }
    public static void SetRecord(int value)
    {
        PlayerPrefs.SetInt("Record", value);
        Record record = new() {record = value};
        if (!File.Exists(Application.persistentDataPath + "/data.json")) File.Create(Application.persistentDataPath + "/data.json").Close();
        string json = JsonUtility.ToJson(record);
        File.WriteAllText(Application.persistentDataPath + "/data.json", json);
    }
}
public class Record
{
    public int record;
}