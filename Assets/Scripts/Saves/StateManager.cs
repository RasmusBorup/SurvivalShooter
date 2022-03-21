using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class StateManager
{
    public static int CurrentScore {get; set;} = 100;
    public static int DamageUpgrades{get; set;} = 0;
    public static int FireRateUpgrades {get; set;} = 0;
    public static int MagazineSizeUpgrades {get; set;} = 0;
    public static int ReloadSpeedUpgrades {get; set;} = 0;
    public static int HealthUpgrades {get; set;} = 0;
    public static int MoveSpeedUpgrades {get; set;} = 0;
    static string saveDestination = Application.persistentDataPath + "/saves/test";

    public static void CreateGame(string fileName)
    {
        saveDestination = Application.persistentDataPath + "/saves/" + fileName;

        Save();
    }

    public static void Save()
    {
        SaveFile saveFile = new SaveFile();
        saveFile.currentScore = CurrentScore;
        saveFile.shootDamageUpgrades = DamageUpgrades;
        saveFile.shootSpeedUpgrades = FireRateUpgrades;
        saveFile.magazineSizeUpgrades = MagazineSizeUpgrades;
        saveFile.reloadSpeedUpgrades = ReloadSpeedUpgrades;
        saveFile.healthUpgrades = HealthUpgrades;
        saveFile.moveSpeedUpgrades = MoveSpeedUpgrades;
        
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
        DamageUpgrades = saveFile.shootDamageUpgrades;
        FireRateUpgrades = saveFile.shootSpeedUpgrades;
        MagazineSizeUpgrades = saveFile.magazineSizeUpgrades;
        HealthUpgrades = saveFile.healthUpgrades;
        ReloadSpeedUpgrades = saveFile.reloadSpeedUpgrades;
        MoveSpeedUpgrades = saveFile.moveSpeedUpgrades;
    }
}
