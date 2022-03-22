using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MetaShopController : MonoBehaviour 
{
	int damagePriceIncrease = 50;
	int fireRatePriceIncrease = 50;
	int magazinePriceIncrease = 50;
	int reloadSpeedPriceIncrease = 50;
	int healthPriceIncrease = 50;
	int moveSpeedPriceIncrease = 50;
	int greedPriceIncrease = 200;
	int critChancePriceIncrease = 500;
	int critMultiplierPriceIncrease = 500;
	int regenPriceIncrease = 1000;

	Button damageButton;
	Button fireRateButton;
	Button magazineSizeButton;
	Button healthButton;
	Button reloadSpeedButton;
	Button moveSpeedButton;
	Button greedButton;
	Button regenButton;
	Button critChanceButton;
	Button critMultiplierButton;
	
	void Start () 
	{
		damageButton = GameObject.Find("UpgradeDamageButton").GetComponent<Button>();
		UpdateButtonText(damageButton, "Damage", CalculatePrice(damagePriceIncrease, StateManager.DamageUpgrades));
		damageButton.onClick.AddListener(UpgradeDamage);

		fireRateButton = GameObject.Find("UpgradeFireRateButton").GetComponent<Button>();
		UpdateButtonText(fireRateButton, "Firerate", CalculatePrice(fireRatePriceIncrease, StateManager.FireRateUpgrades));
		fireRateButton.onClick.AddListener(UpgradeFireRate);

		magazineSizeButton = GameObject.Find("UpgradeMagazineSizeButton").GetComponent<Button>();
		UpdateButtonText(magazineSizeButton, "Magazine", CalculatePrice(magazinePriceIncrease, StateManager.MagazineSizeUpgrades));
		magazineSizeButton.onClick.AddListener(UpgradeMagazine);

		healthButton = GameObject.Find("UpgradeHealthButton").GetComponent<Button>();
		UpdateButtonText(healthButton, "Health", CalculatePrice(healthPriceIncrease, StateManager.HealthUpgrades));
		healthButton.onClick.AddListener(UpgradeHealth);

		reloadSpeedButton = GameObject.Find("UpgradeReloadSpeedButton").GetComponent<Button>();
		UpdateButtonText(reloadSpeedButton, "Reload Speed", CalculatePrice(reloadSpeedPriceIncrease, StateManager.ReloadSpeedUpgrades));
		reloadSpeedButton.onClick.AddListener(UpgradeReload);

		moveSpeedButton = GameObject.Find("UpgradeMoveSpeedButton").GetComponent<Button>();
		UpdateButtonText(moveSpeedButton, "Movement Speed", CalculatePrice(moveSpeedPriceIncrease, StateManager.MoveSpeedUpgrades));
		moveSpeedButton.onClick.AddListener(UpgradeMoveSpeed);

		greedButton = GameObject.Find("UpgradeGreedButton").GetComponent<Button>();
		UpdateButtonText(greedButton, "Greed", CalculatePrice(greedPriceIncrease, StateManager.GreedUpgrades));
		greedButton.onClick.AddListener(UpgradeGreed);
		
		regenButton = GameObject.Find("UpgradeRegenButton").GetComponent<Button>();
		UpdateButtonText(regenButton, "Regen", CalculatePrice(regenPriceIncrease, StateManager.RegenUpgrades));
		regenButton.onClick.AddListener(UpgradeRegen);
		
		critChanceButton = GameObject.Find("UpgradeCritChanceButton").GetComponent<Button>();
		UpdateButtonText(critChanceButton, "Critical Chance", CalculatePrice(critChancePriceIncrease, StateManager.CritChanceUpgrades));
		critChanceButton.onClick.AddListener(UpgradeCritChance);
		
		critMultiplierButton = GameObject.Find("UpgradeCritMultiplierButton").GetComponent<Button>();
		UpdateButtonText(critMultiplierButton, "Critical Multiplier", CalculatePrice(critMultiplierPriceIncrease, StateManager.CritMultiplierUpgrades));
		critMultiplierButton.onClick.AddListener(UpgradeCritMultiplier);
	}

    void Awake()
    {
	}

	public void Exit()
	{
		StateManager.Save();
		SceneManager.LoadScene("Main Menu");
	}

	public void StartGame()
	{
		StateManager.Save();
		SceneManager.LoadScene("Level 01");
	}

    public static int CalculatePrice(int priceIncrease, int upgradeCount)
    {
        return priceIncrease * upgradeCount + priceIncrease;
    }

	static void UpdateButtonText(Button button, string text, int price)
	{
		button.GetComponentInChildren<Text> ().text = string.Format("Upgrade \n {0} \n\n Price: \n {1}", text, price);
	}

	void UpgradeDamage()
	{
		int damagePrice = CalculatePrice(damagePriceIncrease, StateManager.DamageUpgrades);
		 
		if (StateManager.CurrentScore >= damagePrice) {
			StateManager.CurrentScore -= damagePrice;
			StateManager.DamageUpgrades++;
			damagePrice += damagePriceIncrease;
			UpdateButtonText(damageButton, "Damage", damagePrice);
		}
	}

	void UpgradeFireRate()
	{
		int fireRatePrice = CalculatePrice(fireRatePriceIncrease, StateManager.FireRateUpgrades);
		 
		if (StateManager.CurrentScore >= fireRatePrice) {
			StateManager.CurrentScore -= fireRatePrice;
			StateManager.FireRateUpgrades++;
			fireRatePrice += fireRatePriceIncrease;
			UpdateButtonText(fireRateButton, "Firerate", fireRatePrice);
		}
	}

	void UpgradeMagazine()
	{
		int magazinePrice = CalculatePrice(magazinePriceIncrease, StateManager.MagazineSizeUpgrades);
		 
		if (StateManager.CurrentScore >= magazinePrice) {
			StateManager.CurrentScore -= magazinePrice;
			StateManager.MagazineSizeUpgrades++;
			magazinePrice += magazinePriceIncrease;
			UpdateButtonText(magazineSizeButton, "Magazine", magazinePrice);
		}
	}

	void UpgradeReload()
	{
		int reloadPrice = CalculatePrice(reloadSpeedPriceIncrease, StateManager.ReloadSpeedUpgrades);
		 
		if (StateManager.CurrentScore >= reloadPrice) {
			StateManager.CurrentScore -= reloadPrice;
			StateManager.ReloadSpeedUpgrades++;
			reloadPrice += reloadSpeedPriceIncrease;
			UpdateButtonText(reloadSpeedButton, "Reload Speed", reloadPrice);
		}
	}

	void UpgradeHealth()
	{
		int healthPrice = CalculatePrice(healthPriceIncrease, StateManager.HealthUpgrades);
		 
		if (StateManager.CurrentScore >= healthPrice) {
			StateManager.CurrentScore -= healthPrice;
			StateManager.HealthUpgrades++;
			healthPrice += healthPriceIncrease;
			UpdateButtonText(healthButton, "Health", healthPrice);
		}
	}

	void UpgradeMoveSpeed()
	{
		int moveSpeedPrice = CalculatePrice(moveSpeedPriceIncrease, StateManager.MoveSpeedUpgrades);
		 
		if (StateManager.CurrentScore >= moveSpeedPrice) {
			StateManager.CurrentScore -= moveSpeedPrice;
			StateManager.MoveSpeedUpgrades++;
			moveSpeedPrice += moveSpeedPriceIncrease;
			UpdateButtonText(moveSpeedButton, "Movement Speed", moveSpeedPrice);
		}
	}

	void UpgradeGreed()
	{
		int greedPrice = CalculatePrice(greedPriceIncrease, StateManager.GreedUpgrades);
		 
		if (StateManager.CurrentScore >= greedPrice) {
			StateManager.CurrentScore -= greedPrice;
			StateManager.GreedUpgrades++;
			greedPrice += greedPriceIncrease;
			UpdateButtonText(greedButton, "Greed", greedPrice);
		}
	}

	void UpgradeRegen()
	{
		int regenPrice = CalculatePrice(regenPriceIncrease, StateManager.RegenUpgrades);
		 
		if (StateManager.CurrentScore >= regenPrice) {
			StateManager.CurrentScore -= regenPrice;
			StateManager.RegenUpgrades++;
			regenPrice += regenPriceIncrease;
			UpdateButtonText(regenButton, "Regen", regenPrice);
		}
	}

	void UpgradeCritChance()
	{
		int critChancePrice = CalculatePrice(critChancePriceIncrease, StateManager.CritChanceUpgrades);
		 
		if (StateManager.CurrentScore >= critChancePrice) {
			StateManager.CurrentScore -= critChancePrice;
			StateManager.CritChanceUpgrades++;
			critChancePrice += critChancePriceIncrease;
			UpdateButtonText(critChanceButton, "Critical Chance", critChancePrice);
		}
	}

	void UpgradeCritMultiplier()
	{
		int critMultiplierPrice = CalculatePrice(critMultiplierPriceIncrease, StateManager.CritMultiplierUpgrades);
		 
		if (StateManager.CurrentScore >= critMultiplierPrice) {
			StateManager.CurrentScore -= critMultiplierPrice;
			StateManager.CritMultiplierUpgrades++;
			critMultiplierPrice += critMultiplierPriceIncrease;
			UpdateButtonText(critMultiplierButton, "Critical Multiplier", critMultiplierPrice);
		}
	}
}
