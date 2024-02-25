[System.Serializable]
public class SaveFile
{
    public int currentScore = 0;
    public int wavesCleared = 0;
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

    public void SetState()
    {
        currentScore = StateManager.CurrentScore;
        wavesCleared = StateManager.WavesCleared;
        shootDamageUpgrades = StateManager.DamageUpgrades;
        shootSpeedUpgrades = StateManager.FireRateUpgrades;
        magazineSizeUpgrades = StateManager.MagazineSizeUpgrades;
        reloadSpeedUpgrades = StateManager.ReloadSpeedUpgrades;
        healthUpgrades = StateManager.HealthUpgrades;
        moveSpeedUpgrades = StateManager.MoveSpeedUpgrades;
        greedUpgrades = StateManager.GreedUpgrades;
        regenUpgrades = StateManager.RegenUpgrades;
        critChanceUpgrades = StateManager.CritChanceUpgrades;
        critMultiplierUpgrades = StateManager.CritMultiplierUpgrades;
    }

    public void GetState()
    {
        StateManager.CurrentScore = currentScore;
        StateManager.WavesCleared = wavesCleared;
        StateManager.DamageUpgrades = shootDamageUpgrades;
        StateManager.FireRateUpgrades = shootSpeedUpgrades;
        StateManager.MagazineSizeUpgrades = magazineSizeUpgrades;
        StateManager.HealthUpgrades = healthUpgrades;
        StateManager.ReloadSpeedUpgrades = reloadSpeedUpgrades;
        StateManager.MoveSpeedUpgrades = moveSpeedUpgrades;
        StateManager.GreedUpgrades = greedUpgrades;
        StateManager.RegenUpgrades = regenUpgrades;
        StateManager.CritChanceUpgrades = critChanceUpgrades;
        StateManager.CritMultiplierUpgrades = critMultiplierUpgrades;
    }
}
