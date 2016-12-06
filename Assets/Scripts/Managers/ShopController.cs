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

	public int healthPrice = 100;
	public int healthPriceIncrease = 100;
	public int healthIncrease = 10;

	public int restoreHealthPrice = 50;
	public int restoreHealthAmount = 20;

	public Button upgradeDamageButton;
	public Button upgradeFireRateButton;
	public Button upgradeHealthButton;


	GameObject player;
	PlayerHealth playerHealth;
	PlayerShooting playerShooting;
	bool playerInRange;
	bool playerFound;

	// Use this for initialization
	void Start () 
	{
        playerFound = false;
	}

    void Awake()
    {}
	
	// Update is called once per frame
	void Update () 
	{
		if (!playerFound) 
		{
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach(GameObject p in players)
            {
                if(p.networkView.isMine)
                {
                    player = p;
                    playerHealth = player.GetComponent<PlayerHealth> ();
                    playerShooting = player.GetComponentInChildren<PlayerShooting> ();
                    playerInRange = false;
                    playerFound = true;
                }
            }

		}
		if (playerHealth.currentHealth <= 0) {
			playerInRange = false;
		}
		if (playerInRange) {
			if(Input.GetKeyDown(KeyCode.E))
			{
				StartCoroutine("ShowShop");
			}
		}

	}

	void FixedUpdate()
	{

	}

	void OnTriggerEnter(Collider other)
	{
        //Burde måske ændre dette til at kigge på afstanden til spiller i stedet for
		if (other.gameObject == player) 
		{
			playerInRange = true;
			enterShopText.gameObject.SetActive(true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == player) 
		{
			playerInRange = false;
			enterShopText.gameObject.SetActive(false);
		}
	}

	IEnumerator ShowShop()
	{
		while (playerInRange) {
			shop.gameObject.SetActive(true);
			yield return null;
			if(Input.GetKeyDown(KeyCode.E))
			{
				playerInRange = false;
			}
		}
		shop.gameObject.SetActive (false);
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
