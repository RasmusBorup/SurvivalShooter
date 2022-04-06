using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class StateManager
{
    public static int CurrentScore {get; set;}
    public static int WavesCleared {get; set;}
    public static int DamageUpgrades{get; set;}
    public static int FireRateUpgrades {get; set;}
    public static int MagazineSizeUpgrades {get; set;}
    public static int ReloadSpeedUpgrades {get; set;}
    public static int HealthUpgrades {get; set;}
    public static int MoveSpeedUpgrades {get; set;}
    public static int GreedUpgrades {get; set;}
    public static int RegenUpgrades {get; set;}
    public static int CritChanceUpgrades {get; set;}
    public static int CritMultiplierUpgrades {get; set;}
    static string saveDestination = Application.persistentDataPath + "/saves/test";

    public static void CreateGame(string fileName)
    {
        saveDestination = Application.persistentDataPath + "/saves/" + fileName;
        SaveFile saveFile = new SaveFile();
        saveFile.GetState();

        Save();
    }

    public static void Save()
    {
        SaveFile saveFile = new SaveFile();
        saveFile.SetState();
        string json = UnityEngine.JsonUtility.ToJson(saveFile); 
        Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        File.WriteAllText(saveDestination, json);
    }

    public static void Load(string fileName)
    {
        saveDestination = Application.persistentDataPath + "/saves/" + fileName;
        string json = File.ReadAllText(saveDestination);
        SaveFile saveFile = UnityEngine.JsonUtility.FromJson<SaveFile>(json);
        saveFile.GetState();
    }
}
