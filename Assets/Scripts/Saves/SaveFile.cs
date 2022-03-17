using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveFile
{
    public int currentScore = 100;
    public int shootDamageUpgrades = 0;
    public int shootSpeedUpgrades = 0;
    public int magazineSizeUpgrades = 0;
    public int healthUpgrades = 0;

    // string saveDestination;

    // void Awake()
    // {
    //     saveDestination = Application.persistentDataPath + "/save.json";
    // }

    // public void Save()
    // {
    //     string json = UnityEngine.JsonUtility.ToJson(this); 
    //     File.WriteAllText(saveDestination, json);
    // }

    // public void Load()
    // {
    //     string json = File.ReadAllText(saveDestination);
    //     SaveFile saveFile = UnityEngine.JsonUtility.FromJson<SaveFile>(json);
    //     currentScore = saveFile.currentScore;
    //     shootDamageUpgrades = saveFile.shootDamageUpgrades;
    //     shootSpeedUpgrades = saveFile.shootSpeedUpgrades;
    //     magazineSizeUpgrades = saveFile.magazineSizeUpgrades;
    //     healthUpgrades = saveFile.healthUpgrades;
    // }
}
