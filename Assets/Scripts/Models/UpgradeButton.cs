using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton
{

    int levelRequirement;
    int priceIncrease;
    int price;
    int upgradeCount;
    string text;
    Button upgradeButton;
    
    public UpgradeButton(int levelRequirement, int upgradeCount, int priceIncrease, string text, string upgradeButtonName)
    {
        this.levelRequirement = levelRequirement;
        this.upgradeCount = upgradeCount;
        this.priceIncrease = priceIncrease;
        this.text = text;
        this.upgradeButton = GameObject.Find(upgradeButtonName).GetComponent<Button>();
        SetupUpgradeButton();
    }

    void SetupUpgradeButton()
    {
		upgradeButton.gameObject.SetActive(false);
        price = CalculatePrice(priceIncrease, upgradeCount);

        if (StateManager.WavesCleared < levelRequirement) {
            return;
        }

        UpdateButtonText();
        upgradeButton.gameObject.SetActive(true);
        upgradeButton.onClick.AddListener(Upgrade);
    }

    public static int CalculatePrice(int priceIncrease, int upgradeCount)
    {
        return priceIncrease * upgradeCount + priceIncrease;
    }

    void Upgrade()
    {
		if (StateManager.CurrentScore >= price) {
			StateManager.CurrentScore -= price;
			upgradeCount++;
			price += priceIncrease;
			UpdateButtonText();
		}
    }

    void UpdateButtonText()
    {
        upgradeButton.GetComponentInChildren<Text> ().text = string.Format("Upgrade \n {0} \n\n Price: \n {1}", text, price);
    }
}
