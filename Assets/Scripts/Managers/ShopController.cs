using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopController : MonoBehaviour 
{
	public Text enterShopText;
	public Image shop;

	public int damagePrice = 50;
	public int damagePriceIncrease;
	public int damageIncrease;

	public int fireRatePrice;
	public int fireRatePriceIncrease;
	public float fireRateIncrease;

	public int magazineSizePrice;
	public int magazineSizePriceIncrease;
	public int magazineSizeIncrease;

	public int healthPrice = 100;
	public int healthPriceIncrease = 100;
	public int healthIncrease = 10;

	public int restoreHealthPrice = 50;
	public int restoreHealthAmount = 20;

	public Button upgradeDamageButton;
	public Button upgradeFireRateButton;
	public Button upgradeHealthButton;
	public Button upgradeMagazineSizeButton;
	public Button restoreHealthButton;

	GameObject player;
	PlayerHealth playerHealth;
	PlayerShooting playerShooting;
	bool playerInRange;
	public bool shopOpen = false;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
		playerShooting = player.GetComponentInChildren<PlayerShooting> ();
		// upgradeDamageButton.GetComponentInChildren<Text> ().text = "Upgrade \n Damage \n\n Price: \n" + damagePrice;
		// upgradeFireRateButton.GetComponentInChildren<Text> ().text = "Upgrade \n FireRate \n\n Price: \n" + fireRatePrice;
		// upgradeMagazineSizeButton.GetComponentInChildren<Text>().text = "Upgrade \n Magazine Size \n\n Price: \n" + magazineSizePrice;
		// upgradeHealthButton.GetComponentInChildren<Text> ().text = "Upgrade \n Health \n\n Price: \n" + healthPrice;
		restoreHealthButton.GetComponentInChildren<Text> ().text = "Restore \n" + restoreHealthPrice + " Health \n\n Price: \n" + restoreHealthPrice;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (playerHealth.currentHealth <= 0) {
			playerInRange = false;
		}

		if (playerInRange) {
			if(Input.GetKeyDown(KeyCode.E) && !shopOpen) {
				shopOpen = true;
				StartCoroutine("ShowShop");
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player) {
			playerInRange = true;
			enterShopText.gameObject.SetActive(true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == player) {
			playerInRange = false;
			shopOpen = false;
			enterShopText.gameObject.SetActive(false);
		}
	}

	IEnumerator ShowShop()
	{
		while (playerInRange) {
			shop.gameObject.SetActive(true);
			yield return null;

			if(Input.GetKeyDown(KeyCode.E))	{
				shopOpen = false;

				break;
			}
		}

		shop.gameObject.SetActive (false);
	}

	// public void DamageUpgrade()
	// {
	// 	if (StateManager.CurrentScore >= damagePrice) {
	// 		StateManager.CurrentScore -= damagePrice;
	// 		damagePrice += damagePriceIncrease;
	// 		upgradeDamageButton.GetComponentInChildren<Text> ().text = "Upgrade \n Damage \n\n Price: \n" + damagePrice;
	// 		playerShooting.damagePerShot += damageIncrease;
	// 	}
	// }

	// public void FireRateUpgrade()
	// {
	// 	if (StateManager.CurrentScore >= fireRatePrice) {
	// 		StateManager.CurrentScore -= fireRatePrice;
	// 		fireRatePrice += fireRatePriceIncrease;
	// 		upgradeFireRateButton.GetComponentInChildren<Text> ().text = "Upgrade \n FireRate \n\n Price: \n" + fireRatePrice;
	// 		playerShooting.fireRate /= fireRateIncrease;
	// 	}
	// }

	// public void MagazineSizeUpgrade()
	// {
	// 	if (StateManager.CurrentScore < magazineSizePrice) {
	// 		return;
	// 	}

	// 	StateManager.CurrentScore -= magazineSizePrice;
	// 	magazineSizePrice += magazineSizePriceIncrease;
	// 	upgradeMagazineSizeButton.GetComponentInChildren<Text>().text = "Upgrade \n Magazine Size \n\n Price: \n" + magazineSizePrice;
	// 	playerShooting.magazineSize += magazineSizeIncrease;
	// 	playerShooting.UpdateAmmoCounter();
	// }

	// public void HealthUpgrade()
	// {
	// 	if (StateManager.CurrentScore >= healthPrice) {
	// 		StateManager.CurrentScore -= healthPrice;
	// 		healthPrice += healthPriceIncrease;
	// 		upgradeHealthButton.GetComponentInChildren<Text> ().text = "Upgrade \n Health \n\n Price: \n" + healthPrice;
	// 		playerHealth.startingHealth += healthIncrease;
	// 		playerHealth.currentHealth += healthIncrease;
	// 	}
	// }

	public void RestoreHealth()
	{
		if (StateManager.CurrentScore >= restoreHealthPrice) {
			if(playerHealth.currentHealth < playerHealth.startingHealth)
			{
				StateManager.CurrentScore -= restoreHealthPrice;
				if(playerHealth.currentHealth <= playerHealth.startingHealth - restoreHealthAmount)
				{
					playerHealth.currentHealth += restoreHealthAmount;
				}
				else
				{
					playerHealth.currentHealth = playerHealth.startingHealth;
				}
			}
		}
	}
}
