﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MetaShopController : MonoBehaviour 
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
	bool shopOpen = false;

	// Use this for initialization
	void Start () 
	{
		// Scene level1 = SceneManager.GetSceneByName("Level 01");
		// Debug.Log(level1.GetRootGameObjects().Length);
		// player = GameObject.FindGameObjectWithTag("Player");
		// playerHealth = player.GetComponent<PlayerHealth> ();
		// playerShooting = player.GetComponentInChildren<PlayerShooting> ();
		// upgradeDamageButton.GetComponentInChildren<Text> ().text = "Upgrade \n Damage \n\n Price: \n" + damagePrice;
		// upgradeFireRateButton.GetComponentInChildren<Text> ().text = "Upgrade \n FireRate \n\n Price: \n" + fireRatePrice;
		// upgradeMagazineSizeButton.GetComponentInChildren<Text>().text = "Upgrade \n Magazine Size \n\n Price: \n" + magazineSizePrice;
		// upgradeHealthButton.GetComponentInChildren<Text> ().text = "Upgrade \n Health \n\n Price: \n" + healthPrice;
		// restoreHealthButton.GetComponentInChildren<Text> ().text = "Restore \n" + restoreHealthPrice + " Health \n\n Price: \n" + restoreHealthPrice;
	}

    void Awake()
    {}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void FixedUpdate()
	{
	}

	public void DamageUpgrade()
	{
		if (ScoreManager.score >= damagePrice) {
			ScoreManager.score -= damagePrice;
			damagePrice += damagePriceIncrease;
			upgradeDamageButton.GetComponentInChildren<Text> ().text = "Upgrade \n Damage \n\n Price: \n" + damagePrice;
			playerShooting.damagePerShot += damageIncrease;
		}
	}

	public void FireRateUpgrade()
	{
		if (ScoreManager.score >= fireRatePrice) {
			ScoreManager.score -= fireRatePrice;
			fireRatePrice += fireRatePriceIncrease;
			upgradeFireRateButton.GetComponentInChildren<Text> ().text = "Upgrade \n FireRate \n\n Price: \n" + fireRatePrice;
			playerShooting.timeBetweenBullets /= fireRateIncrease;
		}
	}

	public void MagazineSizeUpgrade()
	{
		if (ScoreManager.score < magazineSizePrice) {
			return;
		}

		ScoreManager.score -= magazineSizePrice;
		magazineSizePrice += magazineSizePriceIncrease;
		upgradeMagazineSizeButton.GetComponentInChildren<Text>().text = "Upgrade \n Magazine Size \n\n Price: \n" + magazineSizePrice;
		playerShooting.magazineSize += magazineSizeIncrease;
	}

	public void HealthUpgrade()
	{
		if (ScoreManager.score >= healthPrice) {
			ScoreManager.score -= healthPrice;
			healthPrice += healthPriceIncrease;
			upgradeHealthButton.GetComponentInChildren<Text> ().text = "Upgrade \n Health \n\n Price: \n" + healthPrice;
			playerHealth.startingHealth += healthIncrease;
			playerHealth.currentHealth += healthIncrease;
		}
	}

	public void RestoreHealth()
	{
		if (ScoreManager.score >= restoreHealthPrice) {
			if(playerHealth.currentHealth < playerHealth.startingHealth)
			{
				ScoreManager.score -= restoreHealthPrice;
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