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
	int critChancePriceIncrease = 1000;
	int critMultiplierPriceIncrease = 1000;
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
		healthButton = GameObject.Find("UpgradeHealthButton").GetComponent<Button>();
		healthButton.gameObject.SetActive(false);
		fireRateButton = GameObject.Find("UpgradeFireRateButton").GetComponent<Button>();
		fireRateButton.gameObject.SetActive(false);
		moveSpeedButton = GameObject.Find("UpgradeMoveSpeedButton").GetComponent<Button>();
		moveSpeedButton.gameObject.SetActive(false);
		magazineSizeButton = GameObject.Find("UpgradeMagazineSizeButton").GetComponent<Button>();
		magazineSizeButton.gameObject.SetActive(false);
		damageButton = GameObject.Find("UpgradeDamageButton").GetComponent<Button>();
		damageButton.gameObject.SetActive(false);
		reloadSpeedButton = GameObject.Find("UpgradeReloadSpeedButton").GetComponent<Button>();
		reloadSpeedButton.gameObject.SetActive(false);
		greedButton = GameObject.Find("UpgradeGreedButton").GetComponent<Button>();
		greedButton.gameObject.SetActive(false);
		regenButton = GameObject.Find("UpgradeRegenButton").GetComponent<Button>();
		regenButton.gameObject.SetActive(false);
		critChanceButton = GameObject.Find("UpgradeCritChanceButton").GetComponent<Button>();
		critChanceButton.gameObject.SetActive(false);
		critMultiplierButton = GameObject.Find("UpgradeCritMultiplierButton").GetComponent<Button>();
		critMultiplierButton.gameObject.SetActive(false);

		UpdateButtonText(healthButton, "Health", CalculatePrice(healthPriceIncrease, StateManager.HealthUpgrades));
		healthButton.gameObject.SetActive(true);
		healthButton.onClick.AddListener(UpgradeHealth);

		UpdateButtonText(fireRateButton, "Firerate", CalculatePrice(fireRatePriceIncrease, StateManager.FireRateUpgrades));
		fireRateButton.gameObject.SetActive(true);
		fireRateButton.onClick.AddListener(UpgradeFireRate);

		if (StateManager.WavesCleared < 1) {
			return;
		}

		UpdateButtonText(moveSpeedButton, "Movement Speed", CalculatePrice(moveSpeedPriceIncrease, StateManager.MoveSpeedUpgrades));
		moveSpeedButton.gameObject.SetActive(true);
		moveSpeedButton.onClick.AddListener(UpgradeMoveSpeed);

		UpdateButtonText(magazineSizeButton, "Magazine", CalculatePrice(magazinePriceIncrease, StateManager.MagazineSizeUpgrades));
		magazineSizeButton.gameObject.SetActive(true);
		magazineSizeButton.onClick.AddListener(UpgradeMagazine);

		if (StateManager.WavesCleared < 2) {
			return;
		}

		UpdateButtonText(damageButton, "Damage", CalculatePrice(damagePriceIncrease, StateManager.DamageUpgrades));
		damageButton.gameObject.SetActive(true);
		damageButton.onClick.AddListener(UpgradeDamage);

		UpdateButtonText(reloadSpeedButton, "Reload Speed", CalculatePrice(reloadSpeedPriceIncrease, StateManager.ReloadSpeedUpgrades));
		reloadSpeedButton.gameObject.SetActive(true);
		reloadSpeedButton.onClick.AddListener(UpgradeReload);

		if (StateManager.WavesCleared < 3) {
			return;
		}

		UpdateButtonText(greedButton, "Greed", CalculatePrice(greedPriceIncrease, StateManager.GreedUpgrades));
		greedButton.gameObject.SetActive(true);
		greedButton.onClick.AddListener(UpgradeGreed);

		if (StateManager.WavesCleared < 4) {
			return;
		}

		UpdateButtonText(regenButton, "Regen", CalculatePrice(regenPriceIncrease, StateManager.RegenUpgrades));
		regenButton.gameObject.SetActive(true);
		regenButton.onClick.AddListener(UpgradeRegen);

		if (StateManager.WavesCleared < 5) {
			return;
		}

		UpdateButtonText(critChanceButton, "Critical Chance", CalculatePrice(critChancePriceIncrease, StateManager.CritChanceUpgrades));
		critChanceButton.gameObject.SetActive(true);
		critChanceButton.onClick.AddListener(UpgradeCritChance);
		
		UpdateButtonText(critMultiplierButton, "Critical Multiplier", CalculatePrice(critMultiplierPriceIncrease, StateManager.CritMultiplierUpgrades));
		critMultiplierButton.gameObject.SetActive(true);
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
