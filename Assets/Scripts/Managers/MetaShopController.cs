using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MetaShopController : MonoBehaviour 
{
	public int damagePriceIncrease = 50;
	public int fireRatePriceIncrease = 50;
	public int magazinePriceIncrease = 50;
	public int reloadSpeedPriceIncrease = 50;
	public int healthPriceIncrease = 50;
	public int moveSpeedPriceIncrease = 50;

	Button damageButton;
	Button fireRateButton;
	Button magazineSizeButton;
	Button healthButton;
	Button reloadSpeedButton;
	Button moveSpeedButton;
	
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
		reloadSpeedButton = GameObject.Find("ReloadSpeedButton").GetComponent<Button>();
		UpdateButtonText(reloadSpeedButton, "Reload Speed", CalculatePrice(reloadSpeedPriceIncrease, StateManager.ReloadSpeedUpgrades));
		reloadSpeedButton.onClick.AddListener(UpgradeReload);
		moveSpeedButton = GameObject.Find("UpgradeMoveSpeedButton").GetComponent<Button>();
		UpdateButtonText(moveSpeedButton, "Movement Speed", CalculatePrice(moveSpeedPriceIncrease, StateManager.MoveSpeedUpgrades));
		moveSpeedButton.onClick.AddListener(UpgradeMoveSpeed);
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
			damageButton.GetComponentInChildren<Text> ().text = "Upgrade \n Damage \n\n Price: \n" + damagePrice;
		}
	}

	void UpgradeFireRate()
	{
		int fireRatePrice = CalculatePrice(fireRatePriceIncrease, StateManager.FireRateUpgrades);
		 
		if (StateManager.CurrentScore >= fireRatePrice) {
			StateManager.CurrentScore -= fireRatePrice;
			StateManager.FireRateUpgrades++;
			fireRatePrice += fireRatePriceIncrease;
			fireRateButton.GetComponentInChildren<Text> ().text = "Upgrade \n Damage \n\n Price: \n" + fireRatePrice;
		}
	}

	void UpgradeMagazine()
	{
		int magazinePrice = CalculatePrice(magazinePriceIncrease, StateManager.MagazineSizeUpgrades);
		 
		if (StateManager.CurrentScore >= magazinePrice) {
			StateManager.CurrentScore -= magazinePrice;
			StateManager.MagazineSizeUpgrades++;
			magazinePrice += magazinePriceIncrease;
			magazineSizeButton.GetComponentInChildren<Text> ().text = "Upgrade \n Damage \n\n Price: \n" + magazinePrice;
		}
	}

	void UpgradeReload()
	{
		int reloadPrice = CalculatePrice(reloadSpeedPriceIncrease, StateManager.ReloadSpeedUpgrades);
		 
		if (StateManager.CurrentScore >= reloadPrice) {
			StateManager.CurrentScore -= reloadPrice;
			StateManager.ReloadSpeedUpgrades++;
			reloadPrice += reloadSpeedPriceIncrease;
			reloadSpeedButton.GetComponentInChildren<Text> ().text = "Upgrade \n Damage \n\n Price: \n" + reloadPrice;
		}
	}

	void UpgradeHealth()
	{
		int healthPrice = CalculatePrice(healthPriceIncrease, StateManager.HealthUpgrades);
		 
		if (StateManager.CurrentScore >= healthPrice) {
			StateManager.CurrentScore -= healthPrice;
			StateManager.HealthUpgrades++;
			healthPrice += healthPriceIncrease;
			healthButton.GetComponentInChildren<Text> ().text = "Upgrade \n Damage \n\n Price: \n" + healthPrice;
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
}
