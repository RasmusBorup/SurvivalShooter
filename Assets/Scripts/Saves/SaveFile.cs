using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveFile
{
    public int currentScore = 100;
    public int shootDamageUpgrades = 0;
    public int shootSpeedUpgrades = 0;
    public int magazineSizeUpgrades = 0;
    public int reloadSpeedUpgrades = 0;
    public int healthUpgrades = 0;
    public int moveSpeedUpgrades = 0;
    public int greedUpgrades = 0;
    public int regenUpgrades = 0;
    public int critChanceUpgrades = 0;
    public int critMultiplierUpgrades = 0;
}
