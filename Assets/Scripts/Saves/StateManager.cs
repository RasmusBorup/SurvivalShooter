using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class StateManager
{
    public static int CurrentScore {get; set;} = 0;
    public static int ShootDamageUpgrades{get; set;} = 0;
    public static int ShootSpeedUpgrades {get; set;} = 0;
    public static int MagazineSizeUpgrades {get; set;} = 0;
    public static int HealthUpgrades {get; set;} = 0;
    static string saveDestination = Application.persistentDataPath + "/saves";

    public static void CreateGame(string fileName)
    {
        saveDestination = Application.persistentDataPath + "/saves/" + fileName;

        Save();
    }

    public static void Save()
    {
        SaveFile saveFile = new SaveFile();
        saveFile.currentScore = CurrentScore;
        saveFile.shootDamageUpgrades = ShootDamageUpgrades;
        saveFile.shootSpeedUpgrades = ShootSpeedUpgrades;
        saveFile.magazineSizeUpgrades = MagazineSizeUpgrades;
        saveFile.healthUpgrades = HealthUpgrades;
        
        string json = UnityEngine.JsonUtility.ToJson(saveFile); 
        Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        File.WriteAllText(saveDestination, json);
    }

    public static void Load(string fileName)
    {
        saveDestination = Application.persistentDataPath + "/saves/" + fileName;
        string json = File.ReadAllText(saveDestination);
        SaveFile saveFile = UnityEngine.JsonUtility.FromJson<SaveFile>(json);

        CurrentScore = saveFile.currentScore;
        ShootDamageUpgrades = saveFile.shootDamageUpgrades;
        ShootSpeedUpgrades = saveFile.shootSpeedUpgrades;
        MagazineSizeUpgrades = saveFile.magazineSizeUpgrades;
        HealthUpgrades = saveFile.healthUpgrades;
    }
}
